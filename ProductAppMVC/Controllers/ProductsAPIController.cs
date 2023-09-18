using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductAppMVC.Dto;
using ProductAppMVC.Models;

namespace ProductAppMVC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsAPIController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductsAPIController(AppDbContext dbContext)
        {
            this._context = dbContext;
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            var products = _context.Products.Include(p => p.Inventory).ToList();

            var productDtos = products.Select(p => new GetProductsDto
            {
                ProductId = p.ProductId,
                ProductName = p.ProductName,
                ProductDescription = p.ProductDescription,
                StockLevel = p.Inventory.StockLevel
            }).ToList();

            return Ok(productDtos);
        }

        [HttpGet("{productId}")]
        public IActionResult GetProduct(int productId)
        {
            var product = _context.Products.FirstOrDefault(p => p.ProductId == productId);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }
        [HttpPost]
        public IActionResult AddProduct([FromBody] ProductDto productDto)
        {
            var product = new Product
            {
                ProductName = productDto.ProductName,
                ProductDescription = productDto.ProductDescription,
                Inventory = new Inventory
                {
                    StockLevel = 0,
                }
            };
            _context.Add(product);
            _context.SaveChanges();

            return StatusCode(201, product);
        }
        [HttpPut("{productId}")]
        public IActionResult UpdateProduct(int productId, [FromBody] ProductDto productDto)
        {
            var existingProduct = _context.Products.AsNoTracking().FirstOrDefault(p => p.ProductId == productId);

            if (existingProduct == null)
            {
                return NotFound();
            }

            var updatedProduct = new Product
            {
                ProductId = productId,
                ProductName = productDto.ProductName ?? existingProduct.ProductName,
                ProductDescription = productDto.ProductDescription ?? existingProduct.ProductDescription
            };

            _context.Update(updatedProduct);
            _context.SaveChanges();

            return Ok(updatedProduct);
        }

        [HttpPatch("{productId}")]
        public IActionResult UpdateStockLevel(int productId, [FromBody] StockLevelDto stockLevelDto)
        {
            var product = _context.Products.Include(p => p.Inventory).FirstOrDefault(p => p.ProductId == productId);
            if (product == null)
            {
                return NotFound();
            }


            product.Inventory.StockLevel = stockLevelDto.StockLevel;

            _context.Update(product);
            _context.SaveChanges();
            return Ok(product);
        }

    }
}
