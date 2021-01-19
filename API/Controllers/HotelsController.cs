using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Models;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [Route("hotels")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private readonly AuthenticationContext _context;

        public HotelsController(AuthenticationContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Hotel>>> GetHotels()
        {
            var hotels = await _context.Hotels.ToListAsync();
            return Ok(new {Content = hotels});
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Hotel>> GetHotel(long id)
        {
            var hotel = await _context.Hotels.FindAsync(id);

            if (hotel == null)
            {
                return NotFound();
            }

            return hotel;
        }
        
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutHotel(long id, Hotel hotel)
        {
            if (id != hotel.Id)
            {
                return BadRequest();
            }

            _context.Entry(hotel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HotelExists(id))
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }
        
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Hotel>> PostHotel(Hotel hotel)
        {
            await _context.Hotels.AddAsync(hotel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHotel", new { id = hotel.Id }, hotel);
        }
        
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Hotel>> DeleteHotel(long id)
        {
            var hotel = await _context.Hotels.FindAsync(id);
            if (hotel == null)
            {
                return NotFound();
            }

            _context.Hotels.Remove(hotel);
            await _context.SaveChangesAsync();

            return Ok();
        }

        //Menu Items CRUD

        [HttpGet("{id}/menu/")]
        public OkObjectResult GetMenuItems(long id)
        {
            var menuItems = _context.MenuItems.Where(i => i.Hotel.Id == id);
            return Ok(new { Content = menuItems });
        }

        [HttpPost("{id}/menu/")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Hotel>> PostMenuItem(long id, MenuItem item)
        {
            await _context.MenuItems.AddAsync(item);
            await _context.SaveChangesAsync();

            return Ok(item);
        }

        [HttpDelete("{hotelId}/menu/{menuId}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Hotel>> DeleteHotel(long hotelId, long menuId)
        {
            var menuItem = _context.MenuItems.Where(i => i.Hotel.Id == hotelId).First(m => m.Id == menuId);
            if (menuItem == null)
            {
                return NotFound();
            }
            _context.MenuItems.Remove(menuItem);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{hotelId}/menu/{menuId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutMenuItem(long hotelId, long menuId, MenuItem item)
        {
            if (menuId != item.Id)
            {
                return BadRequest();
            }

            _context.Entry(item).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HotelExists(hotelId) && !MenuItemExists(menuId))
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }

        //helper methods
        private bool HotelExists(long id)
        {
            return _context.Hotels.Any(e => e.Id == id);
        }
        private bool MenuItemExists(long id)
        {
            return _context.MenuItems.Any(e => e.Id == id);
        }
    }
}
