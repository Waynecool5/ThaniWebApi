using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;


namespace ThaniWebApi.Controllers.Security
{

    //public interface IUserService
    //{
    //    UserModel Authenticate(string username, string password);

    //    IEnumerable<UserModel> GetAll();
    //}


    //-------------------------------------------------------


    public class UserService : IUserRepository
    {
               
        // users hardcoded for simplicity, store in a db with hashed passwords in production applications
        private List<UserModel> _users = new List<UserModel>
        {
            new UserModel { Id = 1, FirstName = "Test1", LastName = "User1", Username = "test1", Password = "test1" },
            new UserModel { Id = 2, FirstName = "Test2", LastName = "User2", Username = "test2", Password = "test2" },
            new UserModel { Id = 3, FirstName = "Test3", LastName = "User3", Username = "test3", Password = "test3" },
            new UserModel { Id = 4, FirstName = "Test4", LastName = "User4", Username = "test4", Password = "test4" },
            new UserModel { Id = 5, FirstName = "Test5", LastName = "User5", Username = "test5", Password = "test5" },
            new UserModel { Id = 6, FirstName = "Test6", LastName = "User6", Username = "test6", Password = "test6" },
            new UserModel { Id = 7, FirstName = "Test7", LastName = "User7", Username = "test7", Password = "test7" },
            new UserModel { Id = 8, FirstName = "Test8", LastName = "User8", Username = "test8", Password = "test8" }
        };

        private readonly string _Secert;

        public UserService() //IOptions<AppSettings> appSettings)
        {
            _Secert = ClsGlobal.Secret;
        }

        public UserModel Authenticate(string username, string password)
        {
            var user = _users.SingleOrDefault(x => x.Username == username && x.Password == password);

            // return null if user not found
            if (user == null)
                return null;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_Secert);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            // remove password before returning
            user.Password = null;

            return user;
        }

        public IEnumerable<UserModel> GetAll()
        {
            // return users without passwords
            return _users.Select(x => {
                x.Password = null;
                return x;
            });
        }
    }
}