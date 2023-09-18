using System.ComponentModel.DataAnnotations;

namespace ProductAppMVC.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [Required]
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public Inventory Inventory { get; set; }
    }
}
