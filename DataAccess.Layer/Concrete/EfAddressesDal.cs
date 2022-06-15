using ETicaret.DataAccessLayer.Abstrack;
using ETicaret.DataAccessLayer.CodeFirst;
using ETicaret.EntityLayer;

namespace ETicaret.DataAccessLayer.Concrete
{
    public class EfAddressesDal : EfRepository<Adresses, DataContext>, IAdressesDal
    {
     
    }
}
