
using ETicaret.Entity.Featur;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace ETicaret.EntityLayer
{
    public  class Product: Featurs
    {
        [Key]
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public decimal? ReducedPrice { get; set; }
        public string Descriptions { get; set; }
        public string ImageUrl { get; set; }
        public string Barcode { get; set; }
        public int CategoryID { get; set; }
        public Categoria Category { get; set; }
        public List<Images> Images { get; set; }
        public List<ShipmenItem> ShipmenItem { get; set; }
    }
}
