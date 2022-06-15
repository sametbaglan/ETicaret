using ETicaret.EntityLayer;
using System.Collections.Generic;

namespace ETicaret.BusinessLayer.Abstrack
{
    public   interface ICategoriaService
    {
        List<Categoria> GetAll();
        Categoria GetById(int id);
        void Create(Categoria entity);
        void Update(Categoria entity);
        void Delete(Categoria entity);
    }
}
