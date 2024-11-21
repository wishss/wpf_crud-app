namespace WPF_CRUD.Models
{
    class ProductModel
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Price => Quantity * UnitPrice;
    }
}
