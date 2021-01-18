using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class MenuItem
    {
        public long Id { get; set; }
        public string ItemName { get; set; }
        public double ItemPrice { get; set; }
        public bool Available { get; set; }
        public Hotel Hotel { get; set; }    
    }
}
