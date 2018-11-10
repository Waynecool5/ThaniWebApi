using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ThaniClient
{
    public class clsWinGlobal
    {
        public static decimal gsRate = 0.0M;
        public static decimal gsMassyRate = 0.10M;
        public static decimal gsValueRate = 0.10M;
        public static decimal gsBarpRate = 0.20M;
        public static decimal gsStaffRate = 0.20M;

        //--------------------------------------------------------------------
        //Convert C# object to json string object for web api transfer
        //--------------------------------------------------------------------
        public static StringContent GetStringContent(object obj)
            => new StringContent(JsonConvert.SerializeObject(obj), Encoding.Default, "application/json");


        public static StringContent GetStringContent_UTF8(object obj)
                => new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");


        public static StringContent GetStringContent_UTF32(object obj)
                => new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF32, "application/json");


        public static StringContent GetStringContent_ASCII(object obj)
                => new StringContent(JsonConvert.SerializeObject(obj), Encoding.ASCII, "application/json");


        public static StringContent GetStringContent_Uni(object obj)
                => new StringContent(JsonConvert.SerializeObject(obj), Encoding.Unicode, "application/json");


        // Encrypt a file.
        public static void AddEncryption(string FileName)
        {

            File.Encrypt(FileName);

        }

        // Decrypt a file.
        public static void RemoveEncryption(string FileName)
        {
            File.Decrypt(FileName);
        }
    }
}
