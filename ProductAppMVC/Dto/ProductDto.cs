using ProductAppMVC.Models;

namespace ProductAppMVC.Dto
{
    public class ProductDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public Inventory Inventory { get; set; }
    }
}
