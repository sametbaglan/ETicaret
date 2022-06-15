using ETicaret.EntityLayer;
using System.Collections.Generic;

namespace Business.Layer.Abstrack
{
    public interface IShipmenService
    {
        void Create(Shipmens order);
        List<Shipmens> GetOrdersWithUserid(int userid,string statüs);
        List<Shipmens> GetAll();
    }
}
