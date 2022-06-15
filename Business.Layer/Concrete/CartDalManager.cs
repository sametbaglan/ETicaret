using ETicaret.BusinessLayer.Abstrack;
using ETicaret.DataAccessLayer.Abstrack;
using ETicaret.EntityLayer;
using System.Collections.Generic;

namespace ETicaret.BusinessLayer.Concrete
{
    public class CartDalManager : ICartDalService
    {
        private readonly ICartDal _cartDal;
        public CartDalManager(ICartDal cartDal)
        {
            _cartDal = cartDal; 
        }

        public void Add(Cart entity)
        {
          _cartDal.Create(entity);
        }

        public void Create(Cart cart)
        {
            _cartDal.Create(cart);
        }

        public void Delete(Cart entity)
        {
            _cartDal.Delete(entity);
        }

        public List<Cart> GetAll()
        {
           return _cartDal.GetAll();
        }

        public Cart GetById(int id)
        {
           return _cartDal.GetByID(id); 
        }
        public Cart GetCartByUserId(int userId)
        {
            return _cartDal.GetByUserId(userId);
        }

        public void InıtıliazerCart(int userid)
        {
            _cartDal.Create(new Cart() { UserId=userid });
        }

        public void Update(Cart entity)
        {
            _cartDal.Update(entity);
        }

        public void UpdateCart(Cart entity)
        {
            _cartDal.UpdateCart(entity);
        }
    }
}
