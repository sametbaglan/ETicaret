using ETicaret.EntityLayer;
using System.Collections.Generic;
namespace ETicaret.BusinessLayer.Abstrack
{
    public interface ICartDalService
    {
        List<Cart> GetAll();
        Cart GetById(int id);
        void Create(Cart entity);
        void Update(Cart entity);
        void UpdateCart(Cart entity);
        void Delete(Cart entity);
        void Add(Cart entity);
        Cart GetCartByUserId(int userId);
        void InıtıliazerCart(int userid);
    }
}
