using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace API.Controllers
{
    [Route("cart")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly AuthenticationContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CartsController(AuthenticationContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: /Cart
        [HttpGet]
        [Authorize(Roles = "User")]
        public async Task<Cart> GetCustomerCart()
        {
            var userId = User.Claims.First(c => c.Type == "UserID").Value;
            var userCart = await _context.Carts.FirstOrDefaultAsync(c => c.Customer.Id == userId);
            if (userCart == null)
            {
                userCart = new Cart {Customer = await _userManager.FindByIdAsync(userId)};
            }
            await _context.Carts.AddAsync(userCart);
            await _context.SaveChangesAsync();
            return userCart;
        }

        // POST: cart
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<List<CartItem>> AddCartItem(CartItem cartItem)
        {
            var userId = User.Claims.First(c => c.Type == "UserID").Value;
            var userCart = await _context.Carts.FirstOrDefaultAsync(c => c.Customer.Id == userId);
            var cartList = userCart.CartItems;
            cartList.Add(cartItem);
            await _context.SaveChangesAsync();

            return cartList;
        }

        // DELETE: cart/5
        [HttpDelete("{cartItemId}")]
        [Authorize(Roles = "User")]
        public async Task<List<CartItem>> RemoveCartItem(long cartItemId)
        {
            var userId = User.Claims.First(c => c.Type == "UserID").Value;
            var userCart = await _context.Carts.FirstOrDefaultAsync(c => c.Customer.Id == userId);
            var cartItem = userCart.CartItems.Find(c => c.Id == cartItemId);
            userCart.CartItems.Remove(cartItem);
            await _context.SaveChangesAsync();
            return userCart.CartItems;
        }

        // DELETE: cart/
        [HttpDelete]
        [Authorize(Roles = "User")]
        public async Task<OkResult> ClearCart()
        {
            var userId = User.Claims.First(c => c.Type == "UserID").Value;
            var userCart = await _context.Carts.FirstOrDefaultAsync(c => c.Customer.Id == userId);
            _context.Carts.Remove(userCart);
            await _context.SaveChangesAsync();
            return Ok();
        }

        // PUT: /Cart/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("updateQty")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> UpdateQuantity(CartItem cartItem)
        {
            var cartItemToUpdate = await _context.CartItems.FindAsync(cartItem.Id);
            cartItemToUpdate.Quantity = cartItem.Quantity;
            await _context.SaveChangesAsync();
            return Ok(cartItemToUpdate);
        }
    }
}
