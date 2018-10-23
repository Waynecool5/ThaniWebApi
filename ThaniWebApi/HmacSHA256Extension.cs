using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ThaniWebApi
{
    public static class HmacSHA256Extension
    {
        public static string GetHmacSHA256(this object data, String key)
        {
            string a3 = "";
            try
            {
                using (HMACSHA256 hmac = new HMACSHA256(Encoding.ASCII.GetBytes(key)))
                {
                    byte[] a2 = hmac.ComputeHash(Encoding.ASCII.GetBytes(data.ToString()));
                    a3 = Convert.ToBase64String(a2).ToString().Replace("+", "-").Replace("/", "_").Replace("=", "");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "";
            }
            return a3;
        }

    }
}
