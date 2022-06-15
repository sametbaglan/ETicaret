using ETicaret.DataAccessLayer.Abstrack;
using ETicaret.DataAccessLayer.CodeFirst;
using ETicaret.EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ETicaret.DataAccessLayer.Concrete
{
    public class EfItemColorDal : EfRepository<ItemColors, DataContext>, IItemColorDal
    {
        public List<ItemColors> GetColorEmptyAndProductWithProductid(int productid, Expression<Func<ItemColors, bool>> filter = null)
        {
            using (var db = new DataContext())
            {
                return db.ItemColor.Where(x => x.productid == 0 || x.productid == productid).ToList();
            }
        }
        public List<ItemColors> GetEmptyItemColors(Expression<Func<ItemColors, bool>> filter = null)
        {
            using (var db = new DataContext())
            {
                return db.ItemColor.Where(x => x.productid == 0&&x.IsActive==true&&x.IsDeleted==false).ToList();
            }
        }
        public List<ItemColors> GetItemColorWithProductid(int productId, Expression<Func<ItemColors, bool>> filter = null)
        {
            using (var db = new DataContext())
            {
                return db.ItemColor.Where(x => x.productid == productId).ToList();
            }
        }
    }
}
