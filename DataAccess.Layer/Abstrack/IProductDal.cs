

using ETicaret.EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ETicaret.DataAccessLayer.Abstrack
{
  public  interface IProductDal : IRepository<Product>
    {
        List<Product> GetByWithCategoriByProduct(int categoria ,Expression<Func<Product,bool>>expression);
        Product GetByProductDetail(int productid);
        Product GetByFindProductName(string name);
    }
}
