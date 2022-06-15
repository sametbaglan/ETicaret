using ETicaret.BusinessLayer.Abstrack;
using ETicaret.DataAccessLayer.Abstrack;
using ETicaret.EntityLayer;
using System.Collections.Generic;
namespace ETicaret.BusinessLayer.Concrete
{
    public class ItemBodySizeManager : IItemBodySizeService
    {
        private IItemBodySizeDal _ıtemBodySizeDal;
        public ItemBodySizeManager(IItemBodySizeDal ıtemBodySizeDal)
        {
            _ıtemBodySizeDal = ıtemBodySizeDal;
        }
        public void Create(BodySizes entity)
        {
            _ıtemBodySizeDal.Create(entity);
        }

        public void Delete(BodySizes entity)
        {
            _ıtemBodySizeDal.Delete(entity);
        }

        public List<BodySizes> GetAll()
        {
            return _ıtemBodySizeDal.GetAll(x=>x.productid==0&&x.IsActive==true&&x.IsDeleted==false);
        }

        public List<BodySizes> GetBodySizesWithItemColorid(int itemcolorid)
        {
            return _ıtemBodySizeDal.GetBodySizesWithItemColorid(itemcolorid);
        }

        public List<BodySizes> GetBodyWithProductid(int productid)
        {
            return _ıtemBodySizeDal.GetBodySizeWithProductid(productid);
        }
     

        public BodySizes GetById(int id)
        {
            return _ıtemBodySizeDal.GetByID(id);
        }

     
        public List<BodySizes> GetEmptyBodySize()
        {
            return _ıtemBodySizeDal.GetEmptyBodySize();
        }

        public void Update(BodySizes entity)
        {
            _ıtemBodySizeDal.Update(entity);
        }
    }
}
