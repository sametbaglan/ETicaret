using ETicaret.EntityLayer;
using System.Collections.Generic;

namespace ETicaret.BusinessLayer.Abstrack
{
    public interface IAddressesService
    {
        List<Adresses> GetAll();
        Adresses GetById(int id);
        void Create(Adresses entity);
        void Update(Adresses entity);
        void Delete(Adresses entity);
    }
}
