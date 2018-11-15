using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

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

        private UserModel user { get; set; }


        // users hardcoded for simplicity, store in a db with hashed passwords in production applications
        private List<UserModel> _users = new List<UserModel>
        {
            new UserModel { Id = 1, FirstName = "Test1", LastName = "User1", Username = "test1", Password = "test1", APPId ="4d53bce03ec34c0a911182d4c228ee6c" ,APIKey="A93reRTUJHsCuQSHR+L3GxqOJyDmQpCgps102ciuabc="},
            new UserModel { Id = 2, FirstName = "Test2", LastName = "User2", Username = "test2", Password = "test2", APPId ="" ,APIKey=""},
            new UserModel { Id = 3, FirstName = "Test3", LastName = "User3", Username = "test3", Password = "test3", APPId ="" ,APIKey=""},
            new UserModel { Id = 4, FirstName = "Test4", LastName = "User4", Username = "test4", Password = "test4", APPId ="" ,APIKey=""},
            new UserModel { Id = 5, FirstName = "Test5", LastName = "User5", Username = "test5", Password = "test5", APPId ="" ,APIKey=""},
            new UserModel { Id = 6, FirstName = "Test6", LastName = "User6", Username = "test6", Password = "test6", APPId ="" ,APIKey=""},
            new UserModel { Id = 7, FirstName = "Test7", LastName = "User7", Username = "test7", Password = "test7", APPId ="" ,APIKey=""},
            new UserModel { Id = 8, FirstName = "Test8", LastName = "User8", Username = "test8", Password = "test8", APPId ="" ,APIKey=""},
        };

        private readonly string _Secert;

        public UserService() //IOptions<AppSettings> appSettings)
        {
            _Secert = ClsGlobal.Secret;
        }


        private bool decrpytHash(string incomingBase64Signature,string appid, string secretid, string requestTimeStamp)
        {
            string Data = String.Format("{0}{1}", appid, requestTimeStamp);

            var secretKeyBytes = Convert.FromBase64String(secretid);

            byte[] signature = Encoding.UTF8.GetBytes(Data);

            using (HMACSHA256 hmac = new HMACSHA256(secretKeyBytes))
            {
                byte[] signatureBytes = hmac.ComputeHash(signature);

                return (incomingBase64Signature.Equals(Convert.ToBase64String(signatureBytes), StringComparison.Ordinal));
            }
        }

        private bool validateUser(string username, string password, string appid, string apidata, string apiTime)
        {
            try
            {
                //check for vaild user
                user = _users.SingleOrDefault(x => x.Username == username && x.Password == password && x.APPId == appid); // && x.APIKey == apidata);
                if (user == null)
                    return false;

                //can use hmac-hash here from appid and apikey check
                bool result = decrpytHash(apidata, user.APPId, user.APIKey, apiTime);
                if (result == false)
                    return false;

                return true;
                
            }
            catch (Exception)
            {
                return false;
                //throw;
            }
        }


        public UserModel Authenticate(string username, string password, string appid, string apidata, string apiTime  )
        {

            //Check for valid user with secretID
            bool isvalid = validateUser(username, password, appid, apidata, apiTime);
            if (isvalid == false)
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