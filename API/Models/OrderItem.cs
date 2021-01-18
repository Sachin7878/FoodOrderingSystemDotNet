using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class OrderItem
    {
        public long Id { get; set; }
        public MenuItem Item { get; set; }
        public int Quantity { get; set; }
        public double Amount { get; set; }
    }
}
