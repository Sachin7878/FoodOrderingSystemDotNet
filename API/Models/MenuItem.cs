using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

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

        [Required]
        public Hotel Hotel { get; set; }    
    }
}
