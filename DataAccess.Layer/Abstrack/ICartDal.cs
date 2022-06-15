
using ETicaret.EntityLayer;

namespace ETicaret.DataAccessLayer.Abstrack
{
    public interface ICartDal:IRepository<Cart>
    {
        void UpdateCart(Cart cart);
        Cart GetByUserId(int userid);
    }
}
