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


namespace ERA_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        IProductRepository productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        // GET: api/Products1
        [HttpGet]
        [Route("")]
        public ActionResult<List<Product>> GetProducts()
        {
            List<Product> products = productRepository.GetAllProducts();
            if (products.Count == 0)
                return NotFound();
            else
                return Ok(products);
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public ActionResult<Product> GetProduct(int id)
        {
            var product = productRepository.GetByID(id);

            if (product == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(product);
            }
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public IActionResult PutProduct(int id, Product product)
        {
            if (id != product.ProductID)
            {
                return BadRequest();
            }
            else
            {
                productRepository.UpdateProduct(id, product);
                return Ok();
            }

        }


        // POST: api/Products
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public ActionResult<Product> CreateProduct(Product product)
        {
            productRepository.CreateProduct(product);
                 
            return CreatedAtAction("GetProduct", new { id = product.ProductID }, product);
        }

        // DELETE: api/Products1/5
        [HttpDelete("{id}")]
        public ActionResult<Product> DeleteProduct(int id)
        {
            productRepository.DeleteProduct(id);
            return Ok();
        }

        [HttpGet("{name:alpha}")]
        [Route("{name}")]
        public ActionResult<List<Product>> GetProductByName(string name)
        {

            var products = productRepository.GetByName(name);
            if (products.Count == 0)
                return NotFound();
            else
                return Ok(products);

        }

        [HttpGet]
        [Route("[action]/{category}")]
        public ActionResult<List<Product>> Category(Category category)
        {

            var c = productRepository.GetByCategory(category);
            if (c.Count == 0)
                return NotFound();
            else

                return Ok(c);

        }

       /* [HttpPost("{id}")]
        [Route("[action]/{id}")]
        public ActionResult<Product> Discount([FromBody] decimal discount, int id)
        {
            var product=productRepository.AddDiscount(discount, id);
            if (product == null)
                return NotFound();
            else
                return Ok(product);
        }*/
        
        [HttpPut("{id}")]
        [Route("[action]/{id}")]
        public ActionResult<Product> Images(ProductImage image,int id)
        {
            var product = productRepository.AddImage(image,id);
            if (product == null)
                return NotFound();
            else
                return Ok(product);
        }

    }
}
