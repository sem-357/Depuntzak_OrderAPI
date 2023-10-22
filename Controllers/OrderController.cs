using Microsoft.AspNetCore.Mvc;

using Depuntzak_V2.Services;
using Depuntzak_V2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Depuntzak_V2.Data;
using DePuntzak_OrderAPI.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DePuntzak_OrderAPI.Controllers

{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase

    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ShoppingCartService _cartService;

        public OrderController(ApplicationDbContext context, UserManager<IdentityUser> userManager, ShoppingCartService cartService)
        {
            _context = context;
            _userManager = userManager; 
            _cartService = cartService;
        }

        public OrderController(ShoppingCartService cartService)
        {
            _cartService = cartService;
        }
        // GET: api/<OrderController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<OrderController>/5
        [HttpGet("items")]
        public IActionResult GetCartItems()
        {
            
            var cartItems = _cartService.ViewCart();
            return Ok(cartItems);
        }

        [HttpPost]
        [Authorize]
        public IActionResult PlaceOrder()
        {
            var user = _userManager.GetUserAsync(User).Result;
            var cartItems = _cartService.ViewCart();

            
            var order = new Order
            {
                CustomerId = user.Id,
                OrderItems = new List<OrderItem>(),
                
            };

            foreach (var cartItem in cartItems)
            {
                var orderItem = new OrderItem
                {
                    ProductId = cartItem.Product.Id,
                    
                };

                order.OrderItems.Add(orderItem);
            }



            






            return Ok(order);
        }


        // PUT api/<OrderController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<OrderController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
