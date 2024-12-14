using BookStore.Core.Entities.Books;
using BookStore.Core.Entities.Orders;
using BookStore.Core.IUnitOfWork;
using BookStore.Core.Repository.Contract;
using BookStore.Core.Send_Mail;
using BookStore.Core.Service.Contract;
using BookStore.Core.Specifications.BookSpecifications;
using BookStore.Core.Specifications.OrderSpecifications;

namespace BookStore.Service.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBooksService _booksService;

        public OrderService(IBasketRepository basketRepository,IUnitOfWork unitOfWork,IBooksService booksService)
        {
            _basketRepository = basketRepository;
            _unitOfWork = unitOfWork;
            _booksService = booksService;
        }
        public async Task<Order?> CreateOrderAsync(string basketId, string buyerEmail, OrderAddress orderAddress, int deliveryMethodId)
        {
            var basket  = await _basketRepository.GetBasketById(basketId);
            if(basket == null) return null;

            var orderItems = new List<OrderItems>();
            foreach (var item in basket.Items)
            {
                var orderItem = new OrderItems(item.Id, item.Name, item.PictureUrl, item.Price, item.Quantity);
                orderItems.Add(orderItem);
            }
            var deliveryMethod =await  _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(deliveryMethodId);
            if(deliveryMethod == null) return null;

            var order = new Order(buyerEmail, orderAddress, deliveryMethod, orderItems, basket.TotalAmount); 

            _unitOfWork.Repository<Order>().Add(order);

            var result =await  _unitOfWork.CompleteAsync();

            if(result <=0) return null;
            return order;

        }

        public async Task<Order?> UpdateOrderSatus(int orderId)
        {
            var spec =new OrderWithDeliveryMethodSpecifications(orderId);
           var order =await  _unitOfWork.Repository<Order>().GetByIdWithSpecAsync(spec);
           if (order == null || order.OrderStatus==OrderStatus.PaymentRecieved) return null;
           order.OrderStatus = OrderStatus.PaymentRecieved;
           _unitOfWork.Repository<Order>().Update(order);
           await  _unitOfWork.CompleteAsync();
           return order;
        }

        public async Task<IReadOnlyList<Order?>> GetUserOrders(string email)
        {
            var spec = new OrderSpecifications(email);
            var orders = await _unitOfWork.Repository<Order>().GetAllWithSpecAsync(spec);
            if (orders.Count() <=0) return null;
            return orders;
        }
        public async Task<Order?> GetUserOrder(string email,int orderId)
        {
            var spec = new OrderSpecifications(email,orderId);
            var order = await _unitOfWork.Repository<Order>().GetByIdWithSpecAsync(spec);
            if (order == null) return null;
            return order;
        }
        public async Task<bool> DeleteOrder(string email, int orderId)
        {
            var spec = new OrderSpecifications(email, orderId);
            var order = await _unitOfWork.Repository<Order>().GetByIdWithSpecAsync(spec);
            if (order == null) return false;
            _unitOfWork.Repository<Order>().Delete(order);
            var result = await _unitOfWork.CompleteAsync();
            if (result <= 0) return false;
            return true;
            
        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliverymethodsAsync()
        {
            var deliveryMethods= await _unitOfWork.Repository<DeliveryMethod>().GetAllAsync();
            return deliveryMethods;
        }

        public async Task<Order?> UpdateItemsQuantityAfterPayment(int orderId)
        {
            var order = await _unitOfWork.Repository<Order>().GetByIdAsync(orderId);
            foreach (var item in order.OrderItems)
            {
                var bookSpec = new BookWithCategorySpecifications(item.ProductId);
                var book = await _unitOfWork.Repository<Book>().GetByIdWithSpecAsync(bookSpec);
                book.InStock = book.InStock-item.Quantity;
                _unitOfWork.Repository<Book>().Update(book);
            }
            var result = await _unitOfWork.CompleteAsync();
            if (result <= 0) return null;
            return order;
        }
    }
}
