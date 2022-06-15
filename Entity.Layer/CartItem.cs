using System.ComponentModel.DataAnnotations;

namespace ETicaret.EntityLayer
{
    public class CartItem
    {

        [Key]
        public int ID { get; set; }
        public Product Product { get; set; }
        public int ProductID { get; set; }
        public Cart Cart { get; set; }
        public int CartID { get; set; }
        public int Quantity { get; set; }
        public string beden { get; set; }
        public string color { get; set; }
        public decimal totalprice { get; set; }
        public decimal price { get; set; }
        public decimal reducedprice { get; set; }
    }
}