using ERA_WebAPI.ERA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERA_WebAPI.Repository
{
    public interface IOrderRepository
    {
        IQueryable<Order> GetAll();
        Order GetOrder(int orderId);
        bool SubmitOrder(int orderId);
        bool AcceptOrder(int orderId);
        bool RejectOrder(int orderId);
        bool CancelOrder(int orderId);
        bool CreateUserCart(string userId);
        Order GetUserCart(string userId);
        IQueryable<Order> GetOrdersByUserId(string userId);
        bool AddToCart(OrderDetails item);
        bool RemoveFromCart(OrderDetails item);
        bool EditItemQuantity(OrderDetails item);
        void CalculateTotalPrice(Order order);
        bool UpdateUnitsInStock(Order order);
    }
}
