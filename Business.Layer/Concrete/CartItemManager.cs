
using ETicaret.BusinessLayer.Abstrack;
using ETicaret.DataAccessLayer.Abstrack;
using ETicaret.EntityLayer;
using System.Collections.Generic;

namespace ETicaret.BusinessLayer.Concrete
{
    public class CartItemManager : ICartItemService
    {
        private ICartItemDal _cartItemservice;
        public CartItemManager(ICartItemDal cartItemservice)
        {
            _cartItemservice= cartItemservice;
        }

        public void Add(CartItem entity)
        {
            _cartItemservice.Create(entity);
        }

        public void CartItemUpdate(CartItem cartItem)
        {
           _cartItemservice.CartItemUpdate(cartItem);
        }

        public void Create(CartItem entity)
        {
            _cartItemservice.Create(entity);
        }

        public void Delete(CartItem entity)
        {
            _cartItemservice.Delete(entity);
        }

        public CartItem DeleteCartItemByProductid(int userid, int productid)
        {
           return _cartItemservice.DeleteCartItemByProductid(userid, productid);
        }

        public List<CartItem> GetAll()
        {
           return _cartItemservice.GetAll();
        }

        public CartItem GetById(int id)
        {
           return _cartItemservice.GetByID(id); 
        }

        public CartItem GetByProductId(int productid)
        {
            return _cartItemservice.GetByProductId(productid); 
        }

        public List<CartItem> GetCartListWithCartid(int cartid)
        {
            return _cartItemservice.GetCartListWithCartid(cartid);
        }

        public void Update(CartItem entity)
        {
            _cartItemservice.Update(entity);
        }
    }
}
