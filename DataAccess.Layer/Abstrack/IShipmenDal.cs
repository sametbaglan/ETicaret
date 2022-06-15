
using ETicaret.DataAccessLayer.Abstrack;
using ETicaret.EntityLayer;
using System.Collections.Generic;

namespace DataAccess.Layer.Abstrack
{
    public interface IShipmenDal:IRepository<Shipmens>
    {
        List<Shipmens> GetOrdersWithUserid(int userid,string statüs);
    }
}
