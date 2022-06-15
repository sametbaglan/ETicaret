using System.ComponentModel.DataAnnotations;

namespace ETicaret.EntityLayer
{
    public class Images
    {

        [Key]
        public int ImagesID { get; set; }
        public string Image { get; set; }

        public int ProductID { get; set; }
        public Product Product { get; set; }
    }
}