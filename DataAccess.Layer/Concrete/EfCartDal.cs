using ETicaret.DataAccessLayer.Abstrack;
using ETicaret.DataAccessLayer.CodeFirst;
using ETicaret.EntityLayer;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ETicaret.DataAccessLayer.Concrete
{
    public class EfCartDal : EfRepository<Cart, DataContext>, ICartDal
    {
        public override void Update(Cart entity)
        {
           using (var context = new DataContext())
            {
                context.Carts.Update(entity);
                context.SaveChanges();
            }
        }
        public Cart GetByUserId(int userid)
        {
            using (var db=new DataContext())
            {
                return db.Carts.Include(x=>x.CartItems)
                    .ThenInclude(x=>x.Product)
                    .Where(x=>x.UserId == userid).FirstOrDefault();
            }
        }

        public void UpdateCart(Cart cart)
        {
           using(var db=new DataContext())
            {
                db.Entry(cart).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
    }
}
