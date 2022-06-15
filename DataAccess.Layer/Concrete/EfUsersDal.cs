
using ETicaret.DataAccessLayer.Abstrack;
using ETicaret.DataAccessLayer.CodeFirst;
using ETicaret.EntityLayer;
using System.Linq;

namespace ETicaret.DataAccessLayer.Concrete
{
    public class EfUsersDal : EfRepository<Users, DataContext>, IUsersDal
    {
        public Users GetByEmailUsers(string email)
        {
           using (var db=new DataContext())
            {
                return db.Users.Where(x=>x.Email==email).FirstOrDefault();  
            }
        }
    }
}
