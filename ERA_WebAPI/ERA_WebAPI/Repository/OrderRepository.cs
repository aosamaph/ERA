using ERA_WebAPI.Data;
using ERA_WebAPI.ERA.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERA_WebAPI.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ERAContext context;
        public OrderRepository(ERAContext context)
        {
            this.context = context;
        }
        public bool AcceptOrder(int orderId)
        {
            var order = GetOrder(orderId);
            if (order == null)
                return false;
            if (order.Status != OrderStatus.pending)
                return false;
            order.Status = OrderStatus.accepted;
            context.Entry(order).State = EntityState.Modified;
            context.SaveChanges();
            return true;
        }

        public bool AddToCart(OrderDetails item)
        {
            var cart = GetOrder(item.OrderId);
            if (cart == null)
                return false;
            if (cart.Status != OrderStatus.inCart)
                return false;
            if (cart.OrderDetails.Any(od => od.OrderId == item.OrderId && od.ProductId == item.ProductId))
                return false;
            try
            {
                cart.OrderDetails.Add(item);
                context.Entry(cart).State = EntityState.Modified;
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool RemoveFromCart(OrderDetails item)
        {
            var cart = GetOrder(item.OrderId);
            if (cart == null)
                return false;
            if (cart.Status != OrderStatus.inCart)
                return false;
            var trackedItem = cart.OrderDetails.SingleOrDefault(od => od.OrderId == item.OrderId && od.ProductId == item.ProductId);
            if (trackedItem == null)
                return false;
            try
            {
                cart.OrderDetails.Remove(trackedItem);
                context.Entry(cart).State = EntityState.Modified;
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool CancelOrder(int orderId)
        {
            var order = GetOrder(orderId);
            if (order == null)
                return false;
            if (order.Status != OrderStatus.pending)
                return false;
            order.Status = OrderStatus.cancelled;
            //context.Entry(order).State = EntityState.Modified;
            context.SaveChanges();
            return true;
        }

        public bool CreateUserCart(string userId)
        {
            //check if there is already a cart
            try
            {
                context.Orders.Add(new Order(userId));
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool EditItemQuantity(OrderDetails item)
        {
            var cart = GetOrder(item.OrderId);
            if (cart == null)
                return false;
            if (cart.Status != OrderStatus.inCart)
                return false;
            var trackedItem = cart.OrderDetails.SingleOrDefault(od => od.OrderId == item.OrderId && od.ProductId == item.ProductId);
            if (trackedItem == null)
                return false;
            if (item.NumberOfItems > trackedItem.Product.UnitsInStock)
                return false;
            try
            {
                //context.Entry(item).State = EntityState.Modified;
                trackedItem.NumberOfItems = item.NumberOfItems;
                context.Entry(trackedItem).State = EntityState.Modified;
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IQueryable<Order> GetAll()
        {
            return context.Orders.Where(o => o.Status != OrderStatus.cancelled && o.Status != OrderStatus.inCart)
                .Include(o => o.User)
                .Include(o => o.OrderDetails)
                .ThenInclude(o => o.Product);
        }

        public Order GetOrder(int orderId)
        {
            return context.Orders.Include(o => o.OrderDetails)
                .ThenInclude(od => od.Product)
                .ThenInclude(p => p.Image)
                .SingleOrDefault(o => o.OrderId == orderId);
        }

        public Order GetUserCart(string userId)
        {
            return context.Orders.Include(o => o.OrderDetails)
                .ThenInclude(od => od.Product)
                .ThenInclude(p => p.Image)
                .SingleOrDefault(o => o.UserId == userId && o.Status == OrderStatus.inCart);
        }

        public IQueryable<Order> GetOrdersByUserId(string userId)
        {
            return context.Orders.Where(o => o.UserId == userId && o.Status != OrderStatus.cancelled && o.Status != OrderStatus.inCart)
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Product)
                .ThenInclude(p => p.Image);
        }

        public bool RejectOrder(int orderId)
        {
            var order = GetOrder(orderId);
            if (order == null)
                return false;
            if (order.Status != OrderStatus.pending)
                return false;
            order.Status = OrderStatus.rejected;
            context.Entry(order).State = EntityState.Modified;
            context.SaveChanges();
            return true;
        }

        public bool SubmitOrder(int orderId)
        {
            var order = GetOrder(orderId);
            if (order == null)
                return false;
            if (order.Status != OrderStatus.inCart)
                return false;
            if (order.OrderDetails.Count == 0)
                return false;
            order.Status = OrderStatus.pending;
            order.Date = DateTime.Now;
            try
            {
                context.Entry(order).State = EntityState.Modified;
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void CalculateTotalPrice(Order order)
        {
            decimal total = 0;
            foreach (var item in order.OrderDetails)
            {
                total += item.NumberOfItems * item.Product.UnitPrice * (1 - item.Product.Discount);
            }
            order.TotalPrice = total;
            context.Entry(order).State = EntityState.Modified;
            context.SaveChanges();
            return;
        }

        public bool UpdateUnitsInStock(Order order)
        {
            foreach (var item in order.OrderDetails)
            {
                if (item.NumberOfItems > item.Product.UnitsInStock)
                    return false;
            }
            foreach (var item in order.OrderDetails)
            {
                item.Product.UnitsInStock -= item.NumberOfItems;
            }
            return true;
        }
        public bool ReturnUnitsInStock(Order order)
        {
            foreach (var item in order.OrderDetails)
            {
                item.Product.UnitsInStock += item.NumberOfItems;
            }
            return true;
        }
    }
}
