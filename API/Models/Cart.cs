using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class Cart
    {
        public long Id { get; set; }
        public ApplicationUser Customer { get; set; }
        public List<CartItem> CartItems { get; set; }
    }
}
