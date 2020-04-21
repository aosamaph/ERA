using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERA_WebAPI.ERA.Models
{
    public class OrderDetails
    {
        public OrderDetails(int orderId, int productId)
        {
            ProductId = productId;
            OrderId = orderId;
            NumberOfItems = 1;
        }
        public int ProductId { get; set; }
        public int OrderId { get; set; }
        public int NumberOfItems { get; set; }

        //public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }
}
