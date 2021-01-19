using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class Hotel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        public string HotelName { get; set; }

        [Required]
        public string MobileNo { get; set; }
        
        [Required]
        public Address Address { get; set; }

        public ApplicationUser Vendor { get; set; }

        public byte[] Image { get; set; }

        public string ImageContentType { get; set; }
    }
}
