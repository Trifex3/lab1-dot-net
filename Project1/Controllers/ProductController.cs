using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project1.Data;
using Project1.Models;
using Project1.ViewModels;

namespace Project1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("filter/{minPrice}")]
        public ActionResult<IEnumerable<Product>> FilterProducts(int minPrice)
        {
            var query = _context.Products.Where(p => p.Price >= minPrice);//pregateste doar LINQ
            query.ToQueryString();
            return query.ToList();//se afiseaza doar cand fac ceva cu TO
        }

        // GET: api/Product
        [HttpGet]//get ALL
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts(int? minPrice)
        {   if (minPrice == null)
            {
                return await _context.Products.ToListAsync();
            }

            return await _context.Products.Where(p => p.Price >=minPrice).ToListAsync();
        }

        // GET: api/Product/5
        [HttpGet("{id}")]//find by id->not found 404
        public async Task<ActionResult<ProductViewModel>> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);// sa deduca tipul aici product; PRODUCTS e de fapt repository-u;

            //look at automapper

            var productViewModel = new ProductViewModel
            {
                Description = product.Description,
                Id = product.Id,
                Name = product.Name,
                Price = product.Price
            };

            
            if (product == null)
            {
                return NotFound();
            }

            return productViewModel;
        }

        // PUT: api/Product/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Product
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }

        // DELETE: api/Product/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
