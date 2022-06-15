using Business.Layer.Abstrack;
using DataAccess.Layer.Abstrack;
using ETicaret.EntityLayer;
using System.Collections.Generic;

namespace Business.Layer.Concrete
{
    public class ShipmenItemsManager : IShipmenItemsService
    {
        private IShipmenItemDal _shipmenDal;
        public ShipmenItemsManager(IShipmenItemDal shipmenDal)
        {
            _shipmenDal = shipmenDal;
        }
        public void Create(ShipmenItem entity)
        {
            _shipmenDal.Create(entity);
        }

        public List<ShipmenItem> GetAll()
        {
            return _shipmenDal.GetAll();
        }

        public ShipmenItem GetById(int id)
        {
            return _shipmenDal.GetByID(id);
        }

        public List<ShipmenItem> GetNewOrders(int statenumber)
        {
            return _shipmenDal.GetNewOrders(statenumber);
        }

        public List<ShipmenItem> GetShipmenWith(int ordernumber)
        {
            return _shipmenDal.GetShipmenWith(ordernumber);
        }

        public void Update(ShipmenItem entity)
        {
            _shipmenDal.Update(entity);
        }
    }
}
