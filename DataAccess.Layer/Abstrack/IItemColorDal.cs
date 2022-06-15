using ETicaret.EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ETicaret.DataAccessLayer.Abstrack
{
    public interface IItemColorDal : IRepository<ItemColors>
    {
        List<ItemColors> GetColorEmptyAndProductWithProductid(int productid, Expression<Func<ItemColors, bool>> filter = null);
        List<ItemColors> GetItemColorWithProductid(int productId,Expression<Func<ItemColors,bool>>filter=null);
        List<ItemColors> GetEmptyItemColors(Expression<Func<ItemColors, bool>> filter = null);
    }
}
