using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class Hotel
    {
        public long Id { get; set; }
        public string HotelName { get; set; }
        public string MobileNo { get; set; }
        public Address Address { get; set; }
        public ApplicationUser Vendor { get; set; }
        public byte[] Image { get; set; }
        public string ImageContentType { get; set; }
    }
}
