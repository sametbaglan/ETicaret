using ETicaret.BusinessLayer.Abstrack;
using ETicaret.DataAccessLayer.Abstrack;
using ETicaret.EntityLayer;
using System.Collections.Generic;

namespace ETicaret.BusinessLayer.Concrete
{
    public class AddressesManager : IAddressesService
    {
        private IAdressesDal _adressesDal;
        public AddressesManager(IAdressesDal adressesDal)
        {
            _adressesDal= adressesDal;
        }
        public void Create(Adresses entity)
        {
            _adressesDal.Create(entity);
        }

        public void Delete(Adresses entity)
        {
            _adressesDal.Delete(entity);
        }

        public List<Adresses> GetAll()
        {
           return _adressesDal.GetAll();    
        }

        public Adresses GetById(int id)
        {
            return _adressesDal.GetByID(id);    
        }

        public void Update(Adresses entity)
        {
            _adressesDal.Update(entity);    
        }
    }
}
