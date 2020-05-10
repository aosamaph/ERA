using ERA_WebAPI.Data;
using ERA_WebAPI.ERA.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERA_WebAPI.Repository
{
    public class ProductRepository:IProductRepository
    {
        ERAContext DB;
        public ProductRepository(ERAContext DB)
        {
            this.DB = DB;
        }

        public Product CreateProduct(Product product)
        {
            DB.Products.Add(product);
            DB.SaveChanges();
            return product;
        }

        public Product UpdateProduct(int id,Product product)
        {
            DB.Entry(product).State = EntityState.Modified;
            DB.SaveChanges();
            return product;
        }
         
        public Product DeleteProduct(int id)
        {
            var product = GetByID(id);
            if (product != null)
            {
                product.IsDeleted = true;
                DB.SaveChanges();
            }
            return product;

        }
        public List<Product> GetAllProducts()
        {
            return DB.Products.Where(p=>p.IsDeleted != true).Include("Image").ToList();
        }

        public Product GetByID(int ID)
        {
            var product = DB.Products.Include("Image").FirstOrDefault(p => p.ProductID == ID && p.IsDeleted != true); 
            return product;
        }

        //[Route("api/controller/{name:alpha}")] fel controller
        public List<Product> GetByName(string name)
        {
            var product = DB.Products.Include("Image").Where(p => p.ProductName.StartsWith(name) && p.IsDeleted != true); 
           
            return product.ToList();
        }

        public List<Product> GetByCategory(Category category)
        {
            var product = DB.Products.Include("Image").Where(p => p.Category==(category)&&p.IsDeleted != true);
            return product.ToList();
        }

        public Product AddImage(ProductImage image)
        {
            var p = GetByID(image.ProductID);
            p.Image.Add(image);
            DB.SaveChanges();
            return p;
        }
        public Product DeleteImage(ProductImage image)
        {
            var p = GetByID(image.ProductID);
            var trackedItem = p.Image.SingleOrDefault
                (pp => pp.ProductID == image.ProductID && 
                pp.ImagePath == image.ImagePath);
            if (trackedItem == null)
                return null;
            p.Image.Remove(trackedItem);
            DB.SaveChanges();
            return p;
        }
        public Product AddDiscount(decimal discount,int id)
        {
            var product = GetByID(id);
            product.Discount = discount;
            DB.SaveChanges();
            return product;
        }
        


    }
}
