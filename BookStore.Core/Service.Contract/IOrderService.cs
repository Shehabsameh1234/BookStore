﻿using BookStore.Core.Entities.Orders;


namespace BookStore.Core.Service.Contract
{
    public interface IOrderService
    {
        Task<Order?> CreateOrderAsync(string basketId,string buyerEmail , OrderAddress orderAddress,int deliveryMethodId);
        Task<Order?> UpdateOrderSatus(int orderId);
        Task<IReadOnlyList<Order?>> GetUserOrders(string email);
        Task<IReadOnlyList<DeliveryMethod>> GetDeliverymethodsAsync();
        Task<Order?> GetUserOrder(string email, int orderId);
        Task<bool> DeleteOrder(string email, int orderId);
        Task<Order?> UpdateItemsQuantityAfterPayment(int orderId);
    }
}
