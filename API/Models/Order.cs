using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public enum OrderStatus
    {
        Pending, Dispatched, Delivered, Cancelled
    }
    public class Order
    {
        public long Id { get; set; }
        public ApplicationUser Customer { get; set; }
        public Hotel Hotel { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public double GrandTotal { get; set; }
        public OrderStatus Status { get; set; }
    }
}
