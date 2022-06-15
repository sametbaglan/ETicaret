using DataAccess.Layer.Abstrack;
using ETicaret.DataAccessLayer.CodeFirst;
using ETicaret.DataAccessLayer.Concrete;
using ETicaret.EntityLayer;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Layer.Concrete
{
    public class EfShipmenDal : EfRepository<Shipmens, DataContext>, IShipmenDal
    {
        public List<Shipmens> GetOrdersWithUserid(int userid,string statüs)
        {
            using (var db=new DataContext())
            {
                return db.shipmens.Where(x => x.UserId == userid.ToString()&&x.OrderState==statüs).ToList();
            }
        }
    }
}
