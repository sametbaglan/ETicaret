using Business.Layer.Abstrack;
using DataAccess.Layer.Abstrack;
using ETicaret.EntityLayer;
using System.Collections.Generic;

namespace Business.Layer.Concrete
{
    public class ShipmenManager : IShipmenService
    {
        private IShipmenDal _orderDal;
        public ShipmenManager(IShipmenDal orderDal)
        {
            _orderDal = orderDal;
        }
        public void Create(Shipmens order)
        {
            _orderDal.Create(order);
        }

        public List<Shipmens> GetAll()
        {
            return _orderDal.GetAll();
        }

        public List<Shipmens> GetOrdersWithUserid(int userid,string statüs)
        {
            return _orderDal.GetOrdersWithUserid(userid,statüs);
        }
    }
}
