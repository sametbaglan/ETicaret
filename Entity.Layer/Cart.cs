using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace ETicaret.EntityLayer
{
    public  class Cart
    {
        [Key]
        public int CartID { get; set; }
        public int UserId { get; set; }
        public List<CartItem> CartItems { get; set; }
    }
}
