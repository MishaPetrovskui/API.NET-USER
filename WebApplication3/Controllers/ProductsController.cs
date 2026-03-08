using Microsoft.AspNetCore.Mvc;
using ShopAPI.Models;
using ShopAPI.Services;

namespace ShopAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductsController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public ActionResult<List<Product>> GetProducts()
        {
            return Ok(_productService.GetAll());
        }

        [HttpPost]
        public ActionResult AddProduct([FromBody] ProductDto dto)
        {
            var product = new Product { Name = dto.Name, Price = dto.Price, Stock = dto.Stock };
            _productService.Add(product);
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }
        [HttpGet("{id}")]
        public ActionResult<Product> GetProduct(int id)
        {
            var product = _productService.GetById(id);
            if (product == null) return NotFound();
            return Ok(product);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateProduct(int id, [FromBody] ProductDto dto)
        {
            var product = _productService.GetById(id);
            if (product == null) return NotFound();
            product.Name = dto.Name;
            product.Price = dto.Price;
            product.Stock = dto.Stock;
            _productService.Update();
            return Ok(product);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteProduct(int id)
        {
            var product = _productService.GetById(id);
            if (product == null) return NotFound();
            _productService.Delete(product);
            return Ok();
        }
    }
}
