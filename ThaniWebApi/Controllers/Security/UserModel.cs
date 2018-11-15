using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThaniWebApi.Controllers.Security
{
    public class UserModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string APPId { get; set; }
        public string APIData { get; set; }
        public string APIKey { get; set; }
        public string APITimeStamp { get; set; }
        public string Token { get; set; }
    }
}
