using ETicaret.Entity.Featur;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ETicaret.EntityLayer
{
    public class ItemColors:Featurs
    {
        [Key]
        public int QuickID { get; set; }
        public string Color { get; set; }
        public int userid { get; set; }
        public int productid { get; set; }
        public List<BodySizes> BodySizes { get; set; }
    }
}