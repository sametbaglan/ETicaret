
using ETicaret.EntityLayer;
using System.Collections.Generic;

namespace ETicaret.UILayer.Models
{
    public class BodySizeCreateModel
    {
        public List<Product> Products { get; set; }
        public int BodyID { get; set; }
        public int? XS { get; set; }
        public int? S { get; set; }
        public int? M { get; set; }
        public int? L { get; set; }
        public int? XL { get; set; }
        public int? XXL { get; set; }


        public int ProductID { get; set; }
        public Product Product { get; set; }
    }
}
