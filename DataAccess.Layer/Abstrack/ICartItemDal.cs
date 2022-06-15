using ETicaret.EntityLayer;
using System.Collections.Generic;

namespace ETicaret.DataAccessLayer.Abstrack
{
    public interface ICartItemDal:IRepository<CartItem>
    {
        CartItem DeleteCartItemByProductid(int userid,int productid);
        CartItem GetByProductId(int productid);

        List<CartItem> GetCartListWithCartid(int cartid);
        void CartItemUpdate(CartItem cartItem);
    }
}
