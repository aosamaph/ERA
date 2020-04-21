using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERA_WebAPI.ERA.Models
{
    public class Order
    {

        public Order(string userId)
        {
            
            UserId = userId;
            Status = OrderStatus.inCart;
            OrderDetails = new HashSet<OrderDetails>();
            TotalPrice = 0;
        }

        public int OrderId { get; set; }
        public DateTime? Date { get; set; }
        public OrderStatus? Status { get; set; }
        public decimal? TotalPrice 
        { get; set; }
        //{
        //    get
        //    {
        //        decimal result = 0;
        //        foreach (var item in OrderDetails)
        //        {
        //            result += item.Product.UnitPrice * (1 - item.Product.Discount);
        //        }
        //        return result;
        //    }
        //    set { }
        //}

        public string UserId { get; set; }
        public virtual AppUser User { get; set; }

        public virtual ICollection<OrderDetails> OrderDetails { get; private set; }
    }
}
