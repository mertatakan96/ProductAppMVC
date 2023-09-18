namespace ProductAppMVC.Dto
{
    public class GetProductsDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public int StockLevel { get; set; }
    }

}
