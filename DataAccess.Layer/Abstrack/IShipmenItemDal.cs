using ETicaret.DataAccessLayer.Abstrack;
using ETicaret.EntityLayer;
using System.Collections.Generic;

namespace DataAccess.Layer.Abstrack
{
    public interface IShipmenItemDal:IRepository<ShipmenItem>
    {
        List<ShipmenItem> GetShipmenWith(int ordernumber);
        List<ShipmenItem> GetNewOrders(int statenumber);
    }
}
