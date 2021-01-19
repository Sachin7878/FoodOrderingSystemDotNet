using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("vendor")]
    [ApiController]
    public class VendorController : ControllerBase
    {

        private readonly AuthenticationContext _context;

        public VendorController(AuthenticationContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<Hotel>> VendorHotelDetails()
        {
            var userId = User.Claims.First(c => c.Type == "UserID").Value;
            var hotel = await _context.Hotels.FirstOrDefaultAsync(h => h.Vendor.Id.Equals(userId));
            if (hotel == null)
            {
                return NotFound();
            }
            return Ok(hotel);
        }

        [HttpGet("hotel/menu")]
        public async Task<ActionResult<MenuItem>> GetMenuItemsAsync()
        {
            var userId = User.Claims.First(c => c.Type == "UserID").Value;
            var hotel = await _context.Hotels.FirstOrDefaultAsync(h => h.Vendor.Id.Equals(userId));
            var menuItems = _context.MenuItems.Where(i => i.Hotel.Id == hotel.Id).ToList();
            return Ok(menuItems);
        }
    }
}
