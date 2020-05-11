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

        // GET: api/Products
        [HttpGet]
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
        [Authorize(Roles = "admin, user")]
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
        [Authorize(Roles = "admin")]
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
        [Authorize(Roles = "admin")]
        public ActionResult<Product> CreateProduct(Product product)
        {
            productRepository.CreateProduct(product);

            return CreatedAtAction("GetProduct", new { id = product.ProductID }, product);
        }

        // DELETE: api/Products1/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public ActionResult<Product> DeleteProduct(int id)
        {
            productRepository.DeleteProduct(id);
            return Ok();
        }

        [HttpGet("{name:alpha}")]
        [Authorize(Roles = "admin, user")]
        public ActionResult<List<Product>> GetProductByName(string name)
        {

            var products = productRepository.GetByName(name);
            //if (products.Count == 0)
            //    return NotFound();
            //else
                return Ok(products);

        }

        [HttpGet]
        [Route("[action]/{category}")]
        [Authorize(Roles = "admin, user")]
        public ActionResult<List<Product>> Category(Category category)
        {

            var c = productRepository.GetByCategory(category);
            if (c.Count == 0)
                return NotFound();
            else

                return Ok(c);

        }

        [HttpPut("discount/{id}")]
        [Authorize(Roles = "admin")]
        public ActionResult<Product> Discount(int id, [FromBody] decimal discount)
        {
            var product = productRepository.AddDiscount(discount, id);
            if (product == null)
                return NotFound();
            else
                return Ok(product);
        }

        [HttpPut("image")]
        [Authorize(Roles = "admin")]
        public ActionResult<Product> Images(ProductImage image)
        {
            var product = productRepository.AddImage(image);
            if (product == null)
                return NotFound();
            else
                return Ok(image);
        }

        [HttpPost("image")]
        [Authorize(Roles = "admin")]
        public ActionResult<Product> DeleteImages(ProductImage image)
        {
            var product = productRepository.DeleteImage(image);
            if (product == null)
                return NotFound();
            else
                return Ok(image);
        }

    }
}
