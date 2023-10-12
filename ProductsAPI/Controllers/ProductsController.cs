using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductsAPI.Models;

namespace ProductsAPI.Controllers
{
    // localhost:5000/api/products
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController:ControllerBase
    {
        private readonly ProductsContext _context;

        public ProductsController(ProductsContext context)
        {
            _context = context;
        }

        // localhost:5000/api/products => GET
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products =  await _context.Products.ToListAsync();
            return Ok(products);
        }

        // localhost:5000/api/products/5 => GET
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int? id)
        {   
            if(id == null)
            {
                return NotFound();
            }

            var p = await _context.Products.FirstOrDefaultAsync(i => i.ProductId == id);

            if(p == null)
            {
                return NotFound();
            }

            return Ok(p);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(Product entity)
        {
            _context.Products.Add(entity);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProduct),new { id = entity.ProductId }, entity);
        }
    }
}