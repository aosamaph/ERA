using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERA_WebAPI.ERA.Models
{
    public class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetails>();
        }

        public int OrderId { get; set; }
        public DateTime? Date { get; set; }
        public OrderStatus? Status { get; set; }
        public decimal? TotalPrice { get; set; }
        public string UserId { get; set; }
        public virtual AppUser User { get; set; }
        public virtual ICollection<OrderDetails> OrderDetails { get; set; }
    }
}
