using ETicaret.EntityLayer;
using System.Collections.Generic;

namespace ETicaret.BusinessLayer.Abstrack
{
    public interface ICartItemService
    {
        void CartItemUpdate(CartItem cartItem);
        List<CartItem> GetAll();
        CartItem GetByProductId(int productid);
        CartItem GetById(int id);
        void Create(CartItem entity);
        void Update(CartItem entity);
        void Delete(CartItem entity);
        void Add(CartItem entity);
        CartItem DeleteCartItemByProductid(int userid, int productid);
        List<CartItem> GetCartListWithCartid(int cartid);
    }
}
