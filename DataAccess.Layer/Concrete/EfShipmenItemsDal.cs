using DataAccess.Layer.Abstrack;
using ETicaret.DataAccessLayer.CodeFirst;
using ETicaret.DataAccessLayer.Concrete;
using ETicaret.EntityLayer;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Layer.Concrete
{
    public class EfShipmenItemsDal : EfRepository<ShipmenItem, DataContext>, IShipmenItemDal
    {
        public List<ShipmenItem> GetNewOrders(int statenumber)
        {
            using (var db=new DataContext())
            {
                return db.shipmenItems.Where(x => x.State == statenumber).ToList();
            }
        }

        public List<ShipmenItem> GetShipmenWith(int ordernumber)
        {
            using (var db=new DataContext())
            {
                return db.shipmenItems.Include(x => x.Order).ThenInclude(x => x.OrderItems).Where(x => x.OrderId == ordernumber).ToList();
            }
        }
    }
}
