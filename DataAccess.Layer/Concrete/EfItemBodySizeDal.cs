using ETicaret.DataAccessLayer.Abstrack;
using ETicaret.DataAccessLayer.CodeFirst;
using ETicaret.EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ETicaret.DataAccessLayer.Concrete
{
    public class EfItemBodySizeDal : EfRepository<BodySizes, DataContext>, IItemBodySizeDal
    {
        public List<BodySizes> GetBodySizesWithItemColorid(int itemcolorid, Expression<Func<BodySizes, bool>> filter = null)
        {
            using (var db = new DataContext())
            {
                return db.BodySizes.Where(x => x.ItemColorid == itemcolorid && x.IsDeleted == false && x.IsActive == true).ToList();
            }
        }
        public List<BodySizes> GetBodySizeWithProductid(int productid, Expression<Func<BodySizes, bool>> filter = null)
        {
            using (var db = new DataContext())
            {
                return db.BodySizes.Where(x => x.productid == productid&&x.IsDeleted==false&&x.IsActive==true&&x.BodyCount>0).ToList();
            }
        }
        public List<BodySizes> GetEmptyBodySize(Expression<Func<BodySizes, bool>> filter = null)
        {
            using (var db = new DataContext())
            {
                var ds = db.BodySizes.ToList();
                return ds;
            }
        }
    }
}
