using FakeStoreApp.Models;
using FakeStoreApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FakeStoreApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IFakeStoreService _fakeStoreService;

        public ProductController(IFakeStoreService fakeStoreService)
        {
            _fakeStoreService = fakeStoreService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetProducts([FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] string category = null, [FromQuery] decimal? minPrice = null, [FromQuery] decimal? maxPrice = null, [FromQuery] string name = null)
        {
            var products = await _fakeStoreService.GetProducts();

            var filteredProducts = products
                .Where(p => (category == null || p.Category == category) &&
                            (minPrice == null || p.Price >= minPrice) &&
                            (maxPrice == null || p.Price <= maxPrice) &&
                            (name == null || p.Title.Contains(name, System.StringComparison.InvariantCultureIgnoreCase)))
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return Ok(filteredProducts);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _fakeStoreService.GetProductById(id);
            return Ok(product);
        }
    }
}
