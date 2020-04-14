using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERA_WebAPI.ERA.Models
{
    public class ProductImage
    {
        [ForeignKey("Product")]
        public int ProductID { get; set; }
        public string ImagePath { get; set; }

        public virtual Product Product { get; set; }
    }
}
