using ETicaret.EntityLayer;
using System.Collections.Generic;

namespace ETicaret.UILayer.Models
{
    public class ProductCreateModel
    {
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
        public List<Categoria> SelectedCategoria { get; set; }
        public List<ItemColors> ItemColors { get; set; }
        public List<BodySizes> BodySizes { get; set; }
       
        
    }
}
