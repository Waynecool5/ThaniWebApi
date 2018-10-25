using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ThaniWebApi.Controllers.Points;
using Newtonsoft.Json;
using System.Net.Http;
using Microsoft.Extensions.Hosting.Internal;
using System.IO;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using NaturalSort.Extension;

namespace ThaniWebApi.Controllers.Massy
{
    [Route("api/[controller]")]
    [ApiController]
    public class MassyController : ControllerBase
    {
        public IEnumerable<MassyResponse> Points { get; set; }
        //private IPointsRepository PointsRepository;

        //public MassyController(IPointsRepository PointsRepository)
        //{
        //    this.PointsRepository = PointsRepository;
        //}

        //----------------------------------------------

        [HttpPost]
        [Route("InsertMassyApiPoints")]
        public static async Task<MassyResponse> InsertMassyApiPoints(ICollection<MassyPoints> mPts)
        {
            //
            return await addMassyApiPoints(mPts);

        }
//
        private static async Task<MassyResponse> addMassyApiPoints(ICollection<MassyPoints> mPts)
        {
            //dynamic model;
            HttpClient _clientMassy = new HttpClient();

            try
            {
                _clientMassy.BaseAddress = new Uri(ClsGlobal.MassyAPIver134);
                _clientMassy.DefaultRequestHeaders.Accept.Clear();
                _clientMassy.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                //----------------------------------------------------
                // -- Make Certificate for qsa value
                //------------------------------------------------
              
                //Convert to json string
                string jsonString = JsonConvert.SerializeObject(mPts,Formatting.None,new JsonSerializerSettings()
                    { ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore});

                //Points jsonX = JsonConvert.DeserializeObject<Points>(jsonString);
                string[] sd = new String[8];
                int i = 0;

                JArray jsonX= JArray.Parse(jsonString);

                var list = from t in jsonX[0]
                           select t;

                foreach (string s in list)
                {
                    sd[i] = s;
                    i++;
                }

                //card,units,unitType,mlid,ts,pin
                var sequence = new[] { sd[0], sd[1], sd[2], sd[3], sd[4], sd[5], sd[6] };
                //Convert to Array for sorting
                // Points[] arr = JObject.Parse(jsonString)["subjects"].ToObject<Points[]>();
               // var xx = jsonX[0].Values();
                

                //sd[0] = jsonX.Values()

                ////https://github.com/tompazourek/NaturalSort.Extension
                ////PM: Install-Package NaturalSort.Extension
                var ordered = sequence.OrderBy(x => x, StringComparer.OrdinalIgnoreCase.WithNaturalSort());

                ////place :: to separate values
                //var queryString = ordered.GetQueryString("");

                ////Create hash certificate
                string qsa = ordered.GetHmacSHA256( ordered,sd[7].ToString());

                //-----------------------------------

                ////Get date Massy API "/api/catalog/list"
                ////submit to massyapi
                ////HTTP GET Request sent to the below URL for massy points
                ////http://beta.massycard.com/loyalty/massy/api/rest2/earn?
                ////card =CARD&units=UNITVALUE&unitType=UNITTYPE&mlid=LOCATIONID&ts=UNIXTIMESTAMP&pin= PIN&qsa=GENERATEDHASH


                //string[] cmp

                var queryString  = (from o in mPts
                                select o.card + "::" + o.units + "::" + o.unitType + "::" + o.mlid + "::" + o.ts + "::" + o.pin).ToString();

                var strPath = ClsGlobal.MassyAPIver134 + "earn?" + queryString.ToString(); //+ "::" + qsa.ToString();
                

                var response = await _clientMassy.GetAsync(strPath);

                response.EnsureSuccessStatusCode();

                var stringResponse = await response.Content.ReadAsStringAsync();

                //model = JsonConvert.DeserializeObject<dynamic>(stringResponse);

                //return model;


                string json = @"{ ""response"" : { ""balance"" : { ""p"" : ""60"", ""d"" : ""6.00"" }, ""expiry"" : { ""pts"" : ""2"", ""dat"" : ""10/10/2019""}, ""footer"": [""Footer Line 1 Text"",""Footer Line 2 Text""] } }";

                MassyResponse ResponseData = JsonConvert.DeserializeObject<MassyResponse>(json);

                //IEnumerable<MassyResponse> ResponseData = new MassyResponse 

                return ResponseData;


                ////ReturnsFirst10CatalogItems
                //// Assert.Equal(10, model.CatalogItems.Count());
            }
            catch (Exception ex)
            {
               Console.WriteLine( ex.Message);
                //return null;
                //{"response":{"balance":{"p": "POINTS","d": "DOLLARS"},"expiry": {"pts": "POINTS","dat": "EXPIRYDATE"},"footer":["Footer Line 1 Text","Footer Line 2 Text"]}}
                string json = @"{ ""response"" : { ""balance"" : { ""p"" : ""60"", ""d"" : ""6.00"" }, ""expiry"" : { ""pts"" : ""2"", ""dat"" : ""10/10/2019""}, ""footer"": [""Footer Line 1 Text"",""Footer Line 2 Text""] } }";

                MassyResponse ResponseData = JsonConvert.DeserializeObject<MassyResponse>(json);

                return ResponseData;

            }


        }
        
        //private static string makeHash(ICollection<MassyPoints> mPts)

    }

    internal class Points
    {
        [JsonProperty("Points_card")]
        public string card { get; set; } //Card number
        [JsonProperty("Points_units")]
        public double units { get; set; } //(decimal) Points or Dollar value
        [JsonProperty("Points_unitType")]
        public string unitType { get; set; } // (P or D) – P for points, D for dollars
        [JsonProperty("Points_mlid")]
        public int mlid { get; set; } // (integer) Massy Merchant Location ID
        [JsonProperty("Points_ts")]
        public int ts { get; set; } // (integer) Unix timestamp
        [JsonProperty("Points_pin")]
        public string pin { get; set; } // (integer)00000 5-digit user pin
        [JsonProperty("Points_qsa")]
        public string qsa { get; set; }
        [JsonProperty("Points_Secret")]
        public string secret { get; set; } // (string) The Secret for the selected store

    }
}




//var tmp = mPts.SelectMany(element => element.ToString());


//IEnumerable<string> tmps = from mPt in mPts
//                           where mPt.ts > 0
//                           select new { card = mPt.card, units = mPt.units }; //, mPt.unitType, mPt.mlid, mPt.ts, mPt.pin };




//string[] cmp = (from o in mPts
//                select o.card + "::" + o.units + "::" + o.unitType + "::" + o.mlid + "::" + o.ts + "::" + o.pin).ToArray();


//string[] array = new string[mPts.Count];
//array.CopyTo(array, 0);
//                    IList<string> s = ordered as List<string>;
////Gernerate Hash

//string qsa = ordered.GetHmacSHA256(s.ToString());

//var vQueryString = (JsonConvert.SerializeObject(mPts));
//String v1 = vQueryString.Replace("[", "");
//String v2 = v1.Replace("]", "");
//var json = JsonConvert.DeserializeObject(v2);

//var jObj = (JObject)JsonConvert.DeserializeObject(v2);