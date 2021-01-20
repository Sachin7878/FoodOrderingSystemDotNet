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
        
        [HttpGet]
        [Authorize(Roles = "User")]
        public async Task<List<CartItem>> GetCustomerCart()
        {
            var userId = User.Claims.First(c => c.Type == "UserID").Value;
            var user = await _userManager.FindByIdAsync(userId);
            var userCart = await _context.Carts.Include(i => i.CartItems)
                .ThenInclude(c => c.Item)
                .SingleOrDefaultAsync(c => c.Customer.Id == userId);
            if (userCart == null)
            {
                userCart = new Cart {Customer = user, CartItems = new List<CartItem>()};
                await _context.Carts.AddAsync(userCart);
                await _context.SaveChangesAsync();
            }
            return userCart.CartItems;
        }
        
        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<List<CartItem>> AddCartItem(CartItem cartItem)
        {
            var userId = User.Claims.First(c => c.Type == "UserID").Value;
            var userCart = await _context.Carts.FirstOrDefaultAsync(c => c.Customer.Id == userId);
            var cartList = userCart.CartItems;
            if (cartList == null)
            {
                cartList = new List<CartItem>();
            }
            cartList.Add(cartItem);
            userCart.CartItems = cartList;
            await _context.SaveChangesAsync();

            return cartList;
        }
        
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
        
        [HttpDelete]
        [Authorize(Roles = "User")]
        public async Task<OkResult> ClearCart()
        {
            var userId = User.Claims.First(c => c.Type == "UserID").Value;
            var userCart = await _context.Carts.Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.Customer.Id == userId);

            if (userCart?.CartItems != null)
            {
                userCart.CartItems.ForEach(i =>
                {
                    _context.CartItems.Remove(i);
                });
                await _context.SaveChangesAsync();
            }
            return Ok();
        }
        
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
