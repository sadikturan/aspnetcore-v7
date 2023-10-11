using Microsoft.AspNetCore.Mvc;
using ProductsAPI.Models;

namespace ProductsAPI.Controllers
{
    // localhost:5000/api/products
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController:ControllerBase
    {
        private static List<Product>? _products;

        public ProductsController()
        {
            _products = new List<Product>
            {
                new() { ProductId = 1, ProductName = "IPhone 14", Price = 60000, IsActive = true },
                new() { ProductId = 2, ProductName = "IPhone 15", Price = 70000, IsActive = true },
                new() { ProductId = 3, ProductName = "IPhone 16", Price = 80000, IsActive = true },
                new() { ProductId = 4, ProductName = "IPhone 17", Price = 90000, IsActive = true }
            };
        }

        // localhost:5000/api/products => GET
        [HttpGet]
        public List<Product> GetProducts()
        {
            return _products ?? new List<Product>();
        }

        // localhost:5000/api/products/1 => GET
        [HttpGet("{id}")]
        public Product GetProduct(int id)
        {   
            return _products?.FirstOrDefault(i => i.ProductId == id) ?? new Product();
        }
    }
}