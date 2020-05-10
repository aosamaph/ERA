using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ERA_WebAPI.Data;
using ERA_WebAPI.ERA.Models;
using ERA_WebAPI.Repository;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace ERA_WebAPI.Controllers
{
    [Route("api")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository orderRepository;

        public OrdersController(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        // GET: api/Orders
        // Admin can view all orders(pending, accepted, rejected)
        // View all orders (username,date,total price,product titles only)
        [HttpGet("[controller]")]
        [Authorize(Roles = "admin")]
        public ActionResult<IEnumerable<Order>> GetOrders()
        {
            //var LoggedInUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            return orderRepository.GetAll().ToList();
        }

        // GET: api/Cart/123
        // User gets his cart to add, remove products
        // or to change quantities
        // or to submit order
        [HttpGet("Cart/{user}")]
        [Authorize(Roles = "user")]
        public ActionResult<Order> GetUserCart(string user)
        {
            var order = orderRepository.GetUserCart(user);

            if (order == null)
                return NotFound();

            return order;
        }

        // GET: api/Orders/123
        // User can view all his orders (pending, accepted, rejected)
        [HttpGet("[controller]/{user}")]
        [Authorize(Roles = "user")]
        public ActionResult<IEnumerable<Order>> GetUserOrders(string user)
        {
            var orders = orderRepository.GetOrdersByUserId(user).ToList();

            if (orders == null)
                return NotFound();

            return orders;
        }
        // PUT: api/Orders/accept/1
        // Admin can Accept the order
        [HttpPut("[Controller]/accept/{id}")]
        [Authorize(Roles = "admin")]
        public ActionResult<Order> AcceptOrder(int id)
        {
            var order = orderRepository.GetOrder(id);
            if (order == null)
                return BadRequest();
            var success = orderRepository.AcceptOrder(id);
            if (!success)
                return BadRequest();
            return order;
        }
        // PUT: api/Orders/reject/1
        // Admin can Reject the order
        [HttpPut("[Controller]/reject/{id}")]
        [Authorize(Roles = "admin")]
        public ActionResult<Order> RejectOrder(int id)
        {
            var order = orderRepository.GetOrder(id);
            if (order == null)
                return BadRequest();
            var success = orderRepository.ReturnUnitsInStock(order);
            if (!success)
                return BadRequest();
            success = orderRepository.RejectOrder(id);
            if (!success)
                return BadRequest();
            return order;
        }
        // PUT: api/Orders/cancel/1
        // User can Cancel the order
        [HttpPut("[Controller]/cancel/{id}")]
        [Authorize(Roles = "user")]
        public ActionResult<Order> CancelOrder(int id)
        {
            var order = orderRepository.GetOrder(id);
            if (order == null)
                return BadRequest();
            var success = orderRepository.ReturnUnitsInStock(order);
            if (!success)
                return BadRequest();
            success = orderRepository.CancelOrder(id);
            if (!success)
                return BadRequest();
            return order;
        }
        // PUT: api/Orders/submit/1
        // User can Submit the order
        [HttpPut("[Controller]/submit/{id}")]
        [Authorize(Roles = "user")]
        public ActionResult<Order> SubmitOrder(int id)
        {
            var order = orderRepository.GetOrder(id);
            if (order == null)
                return BadRequest();
            var success = orderRepository.UpdateUnitsInStock(order);
            if (!success)
                return BadRequest("No enough units in stock");
            success = orderRepository.SubmitOrder(id);
            if (!success)
                return BadRequest();
            orderRepository.CreateUserCart(order.UserId);
            return order;
        }
        // PUT: api/Cart/123
        // User can Add product to his cart
        [HttpPost("Cart")]
        [Authorize(Roles = "user")]
        public ActionResult<Order> AddToCart([FromBody] OrderDetails item)
        {
            var success = orderRepository.AddToCart(item);
            if (!success)
                return BadRequest("Not Added");
            var order = orderRepository.GetOrder(item.OrderId);
            orderRepository.CalculateTotalPrice(order);
            return order;
        }
        // PUT: api/Cart/123
        // User can Remove product from his cart
        [HttpPut("Cart/remove")]
        [Authorize(Roles = "user")]
        public ActionResult<Order> RemoveFromCart([FromBody] OrderDetails item)
        {
            var success = orderRepository.RemoveFromCart(item);
            if (!success)
                return BadRequest("Not Removed");
            var order = orderRepository.GetOrder(item.OrderId);
            orderRepository.CalculateTotalPrice(order);
            return order;
        }
        // PUT: api/Cart/123
        // User can Remove product from his cart
        [HttpPut("Cart")]
        [Authorize(Roles = "user")]
        public ActionResult<Order> ChangeQuantity([FromBody] OrderDetails item)
        {
            var success = orderRepository.EditItemQuantity(item);
            if (!success)
                return BadRequest("No enough units in stock");
            var order = orderRepository.GetOrder(item.OrderId);
            orderRepository.CalculateTotalPrice(order);
            return order;
        }
    }
}
