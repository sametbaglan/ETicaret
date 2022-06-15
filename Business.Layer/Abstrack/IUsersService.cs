using ETicaret.EntityLayer;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ETicaret.BusinessLayer.Abstrack
{
    public interface IUsersService
    {
        Task<string> Authenticate(string email, string password);
        Users GetByEmailUsers(string email);
        List<Users> GetAll();
        Users GetById(int id);
        void Create(Users entity);
        void Update(Users entity);
        void Delete(Users entity);
    }
}
