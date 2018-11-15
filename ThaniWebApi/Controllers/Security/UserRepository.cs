using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThaniWebApi.Controllers.Security
{
    public interface IUserRepository
    {
        UserModel Authenticate(string username, string password, string appid, string apidata, string apiTime);

        IEnumerable<UserModel> GetAll();
    }
}
