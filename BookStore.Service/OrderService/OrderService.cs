using BookStore.Core.Entities.Orders;
using BookStore.Core.IUnitOfWork;
using BookStore.Core.Repository.Contract;
using BookStore.Core.Service.Contract;
using BookStore.Core.Specifications.OrderSpecifications;

namespace BookStore.Service.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IBasketRepository basketRepository,IUnitOfWork unitOfWork)
        {
            _basketRepository = basketRepository;
            _unitOfWork = unitOfWork;
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
           if (order == null) return null;

           order.OrderStatus = OrderStatus.PaymentRecieved;

           _unitOfWork.Repository<Order>().Update(order);
           await  _unitOfWork.CompleteAsync();
           return order;
        }
    }
}
