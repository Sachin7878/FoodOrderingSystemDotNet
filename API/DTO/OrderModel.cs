using System.Collections.Generic;
using API.Models;

namespace API.DTO
{
    public class OrderModel
    {
        public long OrderId { get; set; }
        public double GrandTotal { get; set; }
        public HotelModel Hotel { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public Address CustomerAddress { get; set; }
        public OrderStatus Status { get; set; }

        public OrderModel(Order order)
        {
            OrderId = order.Id;
            GrandTotal = order.GrandTotal;
            Hotel = new HotelModel(order.Hotel);
            OrderItems = order.OrderItems;
            CustomerAddress = order.Customer.Address;
            Status = order.OrderStatus;
        }
    }
}
