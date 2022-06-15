
using ETicaret.BusinessLayer.Abstrack;
using ETicaret.DataAccessLayer.Abstrack;
using ETicaret.EntityLayer;
using System.Collections.Generic;

namespace ETicaret.BusinessLayer.Concrete
{
    public class CategoriaManager : ICategoriaService
    {
        private ICategoriaDal _categoriaDal;
        public CategoriaManager(ICategoriaDal categoriaDal)
        {
            _categoriaDal = categoriaDal;
        }
        public void Create(Categoria entity)
        {
            _categoriaDal.Create(entity);
        }

        public void Delete(Categoria entity)
        {
            _categoriaDal.Delete(entity);
        }

        public List<Categoria> GetAll()
        {
            return _categoriaDal.GetAll(x=>x.IsActive==true&&x.IsDeleted==false);
        }

        public Categoria GetById(int id)
        {
            return _categoriaDal.GetByID(id);
        }

        public void Update(Categoria entity)
        {
            _categoriaDal.Update(entity);
        }
    }
}
