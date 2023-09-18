﻿using Microsoft.AspNetCore.Http;
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
                    StockLevel = productDto.Inventory.StockLevel,
                }
            };
            _context.Add(product);
            _context.SaveChanges();

            return StatusCode(201);
        }
        [HttpPut("{productId}")]
        public IActionResult UpdateProduct(int productId, [FromBody] ProductDto productDto)
        {
            Product updateProduct = _context.Products.FirstOrDefault(p => p.ProductId == productId);

            if (updateProduct == null)
            {
                return NotFound();
            }

            var product = new Product
            { 
                ProductId = productId,
                ProductName = productDto.ProductName,
                ProductDescription = productDto.ProductDescription,
            };
            _context.Update(product);
            _context.SaveChanges();

            return Ok();
        }
        [HttpPatch("{productId}")]
        public IActionResult UpdateStockLevel(int productId, [FromBody] StockLevelDto stockLevelDto)
        {
        }
    }
}