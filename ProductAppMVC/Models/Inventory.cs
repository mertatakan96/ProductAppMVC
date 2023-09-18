using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProductAppMVC.Models
{
    public class Inventory
    {
        [Key]
        public int InventoryId { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public int StockLevel { get; set; }
        public Product Product { get; set; }
    }
}
