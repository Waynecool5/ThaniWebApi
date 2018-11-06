using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;


namespace ThaniClient
{
    class clsSecurity
    {
        static UserModel newToken = null;

        public async Task<UserModel> GetSecurityToken(string UrlAddress, UserModel userParam)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(UrlAddress); 
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //StringContent content = new StringContent(JsonConvert.SerializeObject(userParam), Encoding.UTF8, "application/json");

                // HTTP POST to get token
                HttpResponseMessage response1 = await client.PostAsync("api/User/authenticate", clsWinGlobal.GetStringContent_UTF8(userParam)); // content); //.Result();
                if (response1.IsSuccessStatusCode)
                {
                  newToken = await response1.Content.ReadAsAsync<UserModel>();
                }
            }

            return newToken;
        }
    }
}

public class UserModel
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Token { get; set; }
}