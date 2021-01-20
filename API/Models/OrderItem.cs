using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class OrderItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        public MenuItem Item { get; set; }

        [Required]
        public int Quantity { get; set; }
        public double Amount { get; set; }

        public OrderItem()
        {
        }
        public OrderItem(MenuItem item, int quantity)
        {
            Item = item;
            Quantity = quantity;
            Amount = item.ItemPrice * quantity;
        }
    }
}
