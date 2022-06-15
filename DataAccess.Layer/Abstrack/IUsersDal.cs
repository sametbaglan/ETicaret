

using ETicaret.EntityLayer;

namespace ETicaret.DataAccessLayer.Abstrack
{
    public interface IUsersDal:IRepository<Users>
    {
        Users GetByEmailUsers(string email);        
   
    }
}
