using ETicaret.EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ETicaret.DataAccessLayer.Abstrack
{
    public   interface IItemBodySizeDal: IRepository<BodySizes>
    {
        List<BodySizes> GetBodySizeWithProductid(int productid, Expression<Func<BodySizes, bool>> filter = null);
        List<BodySizes> GetEmptyBodySize(Expression<Func<BodySizes, bool>> filter = null);
        List<BodySizes> GetBodySizesWithItemColorid(int itemcolorid,Expression<Func<BodySizes,bool>>filter=null);
    }
}
