using FakeStoreApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FakeStoreApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly IFakeStoreService _fakeStoreService;

        public CategoryController(IFakeStoreService fakeStoreService)
        {
            _fakeStoreService = fakeStoreService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _fakeStoreService.GetCategories();
            return Ok(categories);
        }
    }
}
