using ETicaret.BusinessLayer.Abstrack;
using ETicaret.DataAccessLayer.Abstrack;
using ETicaret.EntityLayer;
using System.Collections.Generic;

namespace ETicaret.BusinessLayer.Concrete
{
    public class ItemColorManager : IItemColorService
    {
        private IItemColorDal _ItemColor;
        public ItemColorManager(IItemColorDal ItemColor)
        {
            _ItemColor = ItemColor;
        }
        public void Create(ItemColors entity)
        {
            _ItemColor.Create(entity);
        }
        public void Delete(ItemColors entity)
        {
            _ItemColor.Delete(entity);
        }
        public List<ItemColors> GetAll()
        {
            return _ItemColor.GetAll(x=>x.productid==0&&x.IsDeleted==false&&x.IsActive==true);
        }
        public ItemColors GetById(int id)
        {
            return _ItemColor.GetByID(id);
        }

        public List<ItemColors> GetColorEmptyAndProductWithProductid(int productid)
        {
            return _ItemColor.GetColorEmptyAndProductWithProductid(productid);
        }

        public List<ItemColors> GetEmptyItemColors()
        {
            return _ItemColor.GetEmptyItemColors();
        }

        public List<ItemColors> GetItemColorsWithProductid(int productid)
        {
            return _ItemColor.GetItemColorWithProductid(productid);
        }

        public void Update(ItemColors entity)
        {
            _ItemColor.Update(entity);
        }
    }
}
