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

        public OrderModel()
        {
            
        }
        public OrderModel(long orderId, double grandTotal, HotelModel hotel, List<OrderItem> orderItems, Address customerAddress, OrderStatus status)
        {
            OrderId = orderId;
            GrandTotal = grandTotal;
            Hotel = hotel;
            OrderItems = orderItems;
            CustomerAddress = customerAddress;
            Status = status;
        }
    }
}
