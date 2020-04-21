using ERA_WebAPI.ERA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERA_WebAPI.Repository
{
    public interface IProductRepository
    {
        public Product CreateProduct(Product product);
        public Product UpdateProduct(int id,Product product);
        public Product DeleteProduct(int id);
        public List<Product> GetAllProducts();
        public Product GetByID(int ID);
        public List<Product> GetByName(string name);
        public List<Product> GetByCategory(Category category);
        public Product AddDiscount(decimal discount, int id);
        public Product AddImage(ProductImage image);
        public Product DeleteImage(ProductImage image);


    }
}
