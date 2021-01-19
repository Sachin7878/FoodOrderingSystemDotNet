using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [Column(TypeName ="nvarchar(70)")]
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
