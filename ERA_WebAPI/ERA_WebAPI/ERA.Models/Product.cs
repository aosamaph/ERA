﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ERA_WebAPI.ERA.Models
{
    public class Product
    {

        public Product()
        {

        }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        
        public decimal UnitPrice { get; set; }
        public int UnitsInStock { get; set; }
        public decimal Discount { get; set; }
        public Category Category { get; set; }
        public string Description{ get; set; }
        public bool IsDeleted { get; set; }
        public  ICollection<ProductImage> Image { get; set; } = new HashSet<ProductImage>();
        public  ICollection<OrderDetails> OrderDetails { get; set; } = new HashSet<OrderDetails>();
    }
}
