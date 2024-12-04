using FakeStoreApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace FakeStoreApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CartController : ControllerBase
    {
        private static Cart _cart = new Cart();

        [HttpPost]
        [Authorize]
        public IActionResult AddToCart([FromBody] CartItem item)
        {
            var existingItem = _cart.Items.FirstOrDefault(i => i.ProductId == item.ProductId);
            if (existingItem != null)
            {
                existingItem.Quantity += item.Quantity;
            }
            else
            {
                _cart.Items.Add(item);
            }

            UpdateCartTotal();
            return Ok(_cart);
        }

        [HttpPut]
        [Authorize]
        public IActionResult UpdateCartItem([FromBody] CartItem item)
        {
            var existingItem = _cart.Items.FirstOrDefault(i => i.ProductId == item.ProductId);
            if (existingItem != null)
            {
                existingItem.Quantity = item.Quantity;
                UpdateCartTotal();
                return Ok(_cart);
            }

            return NotFound();
        }

        [HttpDelete("{productId}")]
        [Authorize]
        public IActionResult DeleteCartItem(int productId)
        {
            var item = _cart.Items.FirstOrDefault(i => i.ProductId == productId);
            if (item != null)
            {
                _cart.Items.Remove(item);
                UpdateCartTotal();
                return Ok(_cart);
            }

            return NotFound();
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetCart()
        {
            return Ok(_cart);
        }

        private void UpdateCartTotal()
        {
            _cart.TotalPrice = _cart.Items.Sum(i => i.Quantity * 10); // Example: assuming each product costs $10
        }
    }
}
