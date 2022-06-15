using ETicaret.EntityLayer;
using System.Collections.Generic;

namespace Business.Layer.Abstrack
{
    public interface IShipmenItemsService
    {
        void Create(ShipmenItem entity);
        List<ShipmenItem> GetShipmenWith(int ordernumber);
        List<ShipmenItem> GetNewOrders(int statenumber);
        List<ShipmenItem> GetAll();

        ShipmenItem GetById(int id);
        void Update(ShipmenItem entity);
    }
}
