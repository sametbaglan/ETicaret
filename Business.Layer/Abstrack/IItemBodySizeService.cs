using ETicaret.EntityLayer;
using System.Collections.Generic;

namespace ETicaret.BusinessLayer.Abstrack
{
    public interface IItemBodySizeService
    {
        List<BodySizes> GetBodySizesWithItemColorid(int itemcolorid);
        List<BodySizes> GetBodyWithProductid(int productid);
        List<BodySizes> GetEmptyBodySize();
        List<BodySizes> GetAll();
        BodySizes GetById(int id);
        void Create(BodySizes entity);
        void Update(BodySizes entity);
        void Delete(BodySizes entity);
    }
}
