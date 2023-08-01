
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using modernapi.Data;
using modernapi.Models;

namespace modernapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly AplicationDbContext _aplicationDbContext;

        public ProductsController(AplicationDbContext aplicationDbContext)
        {
            _aplicationDbContext = aplicationDbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllProduct() 
        {
            return Ok( await _aplicationDbContext.products.ToListAsync());
        }
        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] Product product)
        {
            product.Id = Guid.NewGuid();
            await _aplicationDbContext.products.AddRangeAsync(product);
            await _aplicationDbContext.SaveChangesAsync();
            return Ok(product);
        }
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetProduct(Guid id)
        {
           var product = await _aplicationDbContext.products.FirstOrDefaultAsync(x => x.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateProduct([FromRoute] Guid id, Product updateProduct)
        {
            var product = await _aplicationDbContext.products.FindAsync(id);
            if(product ==null)
            {
                return NotFound();
            }
            product.Name = updateProduct.Name;
            product.Type = updateProduct.Type;
            product.Color = updateProduct.Color;
            product.Price =updateProduct.Price;
            await _aplicationDbContext.SaveChangesAsync();
            return Ok(product);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var product = await _aplicationDbContext.products.FindAsync(id);
            if(product == null)
            {
                return NotFound();
            }
            _aplicationDbContext.products.Remove(product);
            await _aplicationDbContext.SaveChangesAsync();
            return Ok(product);
        }
    }
}
