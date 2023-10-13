using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductsAPI.DTO;
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
            var products =  await _context.Products.Where(i => i.IsActive).Select(p => ProductToDTO(p)).ToListAsync();
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

            var p = await _context.Products.Select(p => ProductToDTO(p)).FirstOrDefaultAsync(i => i.ProductId == id);

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

        // localhost:5000/api/products/5 => PUT
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, Product entity)
        {
            if(id != entity.ProductId)
            {
                return BadRequest();
            }

            var product = await _context.Products.FirstOrDefaultAsync(i => i.ProductId == id);

            if(product == null)
            {
                return NotFound();
            }

            product.ProductName = entity.ProductName;
            product.Price = entity.Price;
            product.IsActive = entity.IsActive;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch(Exception)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int? id)
        {
            if(id == null)
            {
                return  NotFound();
            }

            var product = await _context.Products.FirstOrDefaultAsync(i => i.ProductId == id);

            if(product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch(Exception)
            {
                return NotFound();
            }
            return NoContent();
        }
    
        private static ProductDTO ProductToDTO(Product p)
        {
            return new ProductDTO
            {
                ProductId = p.ProductId,
                ProductName = p.ProductName,
                Price = p.Price
            };
        }
    }


}