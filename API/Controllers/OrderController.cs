using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using API.DTO;
using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly AuthenticationContext _context;

        public OrderController(AuthenticationContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Authorize]
        public List<OrderModel> GetOrders()
        {
            var userId = User.Claims.First(c => c.Type == "UserID").Value;
            var userOrders = _context.Orders.Include(h => h.Hotel)
                .Include(c => c.Customer).ThenInclude(a => a.Address)
                .Include(o => o.OrderItems).ThenInclude(o => o.Item)
                .Where(o => o.Customer.Id == userId);
            var list = new List<OrderModel>();
            foreach (var order in userOrders) list.Add(new OrderModel(order));
            return list;
        }

        [HttpGet("{hotelId}")]
        [Authorize]
        public List<OrderModel> GetOrder(long hotelId)
        {
            var userOrders = _context.Orders.Where(o => o.Hotel.Id == hotelId);
            var list = new List<OrderModel>();
            foreach (var order in userOrders) list.Add(new OrderModel(order));
            return list;
        }

        [HttpPost("place")]
        public async Task<OkResult> PlaceOrder()
        {
            var userId = User.Claims.First(c => c.Type == "UserID").Value;
            var user = await _context.ApplicationUsers.FindAsync(userId);
            var userCart = await _context.Carts.Include(c => c.CartItems)
                .ThenInclude(m => m.Item)
                .ThenInclude(h => h.Hotel).FirstOrDefaultAsync(c => c.Customer.Id == userId);
            var cartItems = userCart.CartItems;
            var itemHotel = cartItems.FirstOrDefault()?.Item.Hotel;
            var orderItems = new List<OrderItem>();

            cartItems.ForEach(c =>
            {
                var newItem = new OrderItem(c.Item, c.Quantity);
                orderItems.Add(newItem);
            });
            foreach (var orderItem in orderItems) await _context.OrderItems.AddAsync(orderItem);
            await _context.SaveChangesAsync();

            var newOrder = new Order
            {
                Customer = user, Hotel = itemHotel, OrderItems = orderItems, Status = OrderStatus.Pending
            };

            var calcAmt = newOrder.CalculateGrandTotal(orderItems);
            newOrder.GrandTotal = calcAmt;

            await _context.Orders.AddAsync(newOrder);
            await _context.SaveChangesAsync();

            cartItems.Clear();
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost("{orderId}")]
        public async Task<OkResult> UpdateStatus(long orderId, string status)
        {
            var myEnum = (OrderStatus) Enum.Parse(typeof(OrderStatus), status, true);
            var orderToUpdate = await _context.Orders.FindAsync(orderId);
            orderToUpdate.Status = myEnum;
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}