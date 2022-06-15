

using ETicaret.DataAccessLayer.Abstrack;
using ETicaret.DataAccessLayer.CodeFirst;
using ETicaret.EntityLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ETicaret.DataAccessLayer.Concrete
{
    public class EfProductDal : EfRepository<Product, DataContext>, IProductDal
    {
        public Product GetByFindProductName(string name)
        {
           using (var context = new DataContext())
            {
                return context.Products.OrderByDescending(x=>x.CreatedDate).Where(x=>x.ProductName==name).FirstOrDefault();
            }
        }

        public Product GetByProductDetail(int productid)
        {
          using(var db=new DataContext())
            {
                return db.Products.Where(x => x.ProductID == productid).FirstOrDefault();
            }
        }

        public List<Product> GetByWithCategoriByProduct(int categoriaid, Expression<Func<Product, bool>> expression)
        {
            using (var db=new DataContext())
            {
                return db.Products.Include(x => x.Category)
                    .ThenInclude(x => x.Products)
                    .Where(x => x.CategoryID == categoriaid&&x.IsActive==true&&x.IsDeleted==false).ToList();
            }
        }
    }
}
