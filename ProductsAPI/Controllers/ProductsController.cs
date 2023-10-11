using Microsoft.AspNetCore.Mvc;

namespace ProductsAPI.Controllers
{
    // localhost:5000/api/products
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController:ControllerBase
    {
        private static readonly string[] Products = {
            "IPhone 14","IPhone 15", "IPhone 16"
        };

        // localhost:5000/api/products => GET
        [HttpGet]
        public string[] GetProducts()
        {
            return Products;
        }

        // localhost:5000/api/products/1 => GET
        [HttpGet("{id}")]
        public string GetProduct(int id)
        {   
            return Products[id];
        }
    }
}