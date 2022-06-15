using ETicaret.DataAccessLayer.Abstrack;
using ETicaret.DataAccessLayer.CodeFirst;
using ETicaret.EntityLayer;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
namespace ETicaret.DataAccessLayer.Concrete
{
    public class EfCartItemDal : EfRepository<CartItem, DataContext>, ICartItemDal
    {
        public override void Update(CartItem entity)
        {
            using(var context = new DataContext())
            {
                context.CartItems.Update(entity);
                context.SaveChanges();
            }
        }
        public CartItem DeleteCartItemByProductid(int userid, int productid)
        {
            using (var db=new DataContext()) 
            {
             CartItem cartItem=db.CartItems.Where(x => x.ProductID == productid && x.Cart.UserId == userid).FirstOrDefault();
              return cartItem;  
            }
        }

        public CartItem GetByProductId(int productid)
        {
            using (var db = new DataContext())
            {
                return db.CartItems.Where(x => x.ProductID == productid).FirstOrDefault();
            } 
        }

        public void CartItemUpdate(CartItem cartItem)
        {
            using(var db=new DataContext())
            {
                db.Entry(cartItem).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
            }
        }
        public List<CartItem> GetCartListWithCartid(int cartid)
        {
           using(var db=new DataContext())
            {
                return db.CartItems.Include(x => x.Cart).ThenInclude(x => x.CartItems).Where(x => x.CartID == cartid).ToList();
            }
        }
    }
}
