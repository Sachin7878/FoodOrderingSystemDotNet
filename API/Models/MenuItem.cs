using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class MenuItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        public string ItemName { get; set; }

        [Required]
        public double ItemPrice { get; set; }

        [Required]
        public bool Available { get; set; }

        public Hotel Hotel { get; set; }    
    }
}
