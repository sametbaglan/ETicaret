using ETicaret.Entity.Featur;
using System.ComponentModel.DataAnnotations;

namespace ETicaret.EntityLayer
{
    public class BodySizes:Featurs
    {
        [Key]
        public int BodyID { get; set; }
        public string BodyName{ get; set; }
        public int BodyCount { get; set; }
        public int ItemColorid { get; set; }
        public ItemColors ItemColors { get; set; }
        public int Userid { get; set; }
        public int productid { get; set; }
    }
}