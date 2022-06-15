
using ETicaret.BusinessLayer.Abstrack;
using ETicaret.BusinessLayer.PasswordHashs;
using ETicaret.DataAccessLayer.Abstrack;
using ETicaret.EntityLayer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.BusinessLayer.Concrete
{
    public class UsersManager : IUsersService
    {
        private IConfiguration _configuration;
        private IUsersDal _usersDal;
        public UsersManager(IUsersDal usersDal, IConfiguration configuration)
        {
            _usersDal = usersDal;
            _configuration = configuration;
        }
        public async Task<string> Authenticate(string email, string password)
        {
            var user = _usersDal.GetByEmailUsers(email);
            if(user== null) return null;

            password = CommonMethods.ConvertToEncryp(password);
            if (user.Password == password)
            {
               
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenKey = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("TokenKey"));
                var tokenDescriptor = new SecurityTokenDescriptor()
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim("userId",user.UserId.ToString()),
                    new Claim(ClaimTypes.Email,user.Email)

                    }),
                    Expires = DateTime.UtcNow.AddYears(1),
                    SigningCredentials = new SigningCredentials(

                        new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
                };
                var stoken = tokenHandler.CreateToken(tokenDescriptor);

                var token = tokenHandler.WriteToken(stoken);
                return token;
            }
            else { return null; }



        }

        public void Create(Users entity)
        {
            _usersDal.Create(entity);
        }

        public void Delete(Users entity)
        {
            _usersDal.Delete(entity);
        }
        public List<Users> GetAll()
        {
            return _usersDal.GetAll();
        }
        public Users GetByEmailUsers(string email)
        {
            return _usersDal.GetByEmailUsers(email);
        }
        public Users GetById(int id)
        {
            return _usersDal.GetByID(id);
        }
        public void Update(Users entity)
        {
            _usersDal.Update(entity);
        }
    }
}
