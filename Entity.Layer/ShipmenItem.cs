using System.ComponentModel.DataAnnotations;
namespace ETicaret.EntityLayer
{
    public class ShipmenItem
    {
        [Key]
        public int Id { get; set; }
        public int OrderId { get; set; }
        public Shipmens Order { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public decimal Price { get; set; }
        public decimal? ReducedPrice { get; set; }
        public int Quantity { get; set; }
        public decimal? CargoPrice { get; set; }
        public string beden { get; set; }
        public string renk { get; set; }
        public int State { get; set; }
        public string CargoCode { get; set; }
    }
}
