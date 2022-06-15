

using ETicaret.DataAccessLayer.Abstrack;
using ETicaret.DataAccessLayer.CodeFirst;
using ETicaret.EntityLayer;


namespace ETicaret.DataAccessLayer.Concrete
{
    public  class EfCategoriaDal : EfRepository<Categoria, DataContext>, ICategoriaDal
    {
    }
}
