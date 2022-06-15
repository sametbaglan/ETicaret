using ETicaret.EntityLayer;
using System.Collections.Generic;
namespace ETicaret.BusinessLayer.Abstrack
{
    public interface IItemColorService
    {
        List<ItemColors> GetColorEmptyAndProductWithProductid(int productid);
        List<ItemColors> GetEmptyItemColors();
        List<ItemColors> GetItemColorsWithProductid(int productid);
        List<ItemColors> GetAll();
        ItemColors GetById(int id);
        void Create(ItemColors entity);
        void Update(ItemColors entity);
        void Delete(ItemColors entity);
    }
}
