using ETicaret.EntityLayer;

namespace ETicaret.UILayer.Models
{
    public class ColorCreateModel
    {
 
        public int QuickID { get; set; }
        public string Color1 { get; set; }
        public string Color2 { get; set; }
        public string Color3 { get; set; }
        public string Color4 { get; set; }
        public string Color5 { get; set; }
        public string Color6 { get; set; }

        public int? ProductID { get; set; }
        public Product Products { get; set; }
    }
}
