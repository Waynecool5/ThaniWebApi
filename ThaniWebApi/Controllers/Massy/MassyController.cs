﻿using System;
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
using System.Text;
using System.Security.Principal;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Net.Http.Headers;

namespace ThaniWebApi.Controllers.Massy
{
    [Authorize]
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

        //[BasicAuthorize("http://beta.massycard.com")]
        [HttpPost]
        [Route("InsertMassyApiPoints")]
        public static async Task<MassyResponse> InsertMassyApiPoints(ICollection<MassyPoints> mPts, string apiType)
        {
            //
            return await doMassyApiPoints(mPts, apiType);

        }




        private static async Task<MassyResponse> doMassyApiPoints(ICollection<MassyPoints> mPts,string apiType)
        {
             HttpClient _clientMassy = new HttpClient(); // handler);
            string strPath = "";
            string json = "";

            try
            {   

                _clientMassy.BaseAddress = new Uri(ClsGlobal.MassyAPIver134);
                _clientMassy.DefaultRequestHeaders.Accept.Clear();
                _clientMassy.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                switch (apiType)
                {   case "earn":
                        //earn?card=CARD&units=UNITVALUE&unitType=UNITTYPE&mlid=LOCATIONID&ts=UNIXTIMESTAMP&pin= PIN&qsa=GENERATEDHASH
                        strPath = MakeQueryString(mPts,"earn", 7);
                        break;
                    case "redeem":
                        //redeem?card=CARD&units=UNITVALUE&unitType=UNITTYPE&mlid=LOCATIONID&ts=UNIXTIMESTAMP&pin=PIN&qsa=GENERATEDHASH
                        strPath = MakeQueryString(mPts, "redeem", 7);
                        break;
                    case "customerProfile":
                        //customerProfile?card=LOYALTY&mlid=LOCATIONID&ts=UNIXTIMESTAMP&qsa=GENERATEDHASH
                        strPath = MakeQueryString(mPts, "customerProfile", 4);
                        break;
                    case "pinverify":
                        //pinverify?mlid=LOCATIONID&ts=UNIXTIMESTAMP&pin= PIN&fcn=FCN&qsa=GENERATEDHASH
                        strPath = MakeQueryString(mPts, "pinverify", 4);
                        break;
                    case "history":
                        //history?card=CARD&mlid=LOCATIONID&limit=NUMBEROFRECORDSTORETURN&qsa=GENERATEDHASH
                        strPath = MakeQueryString(mPts, "history", 4);
                        break;
                    case "void":
                        //void?invoice=INVOICE#&mlid=LOCATIONID&ts=UNIXTIMESTAMP&pin= PIN&qsa=GENERATEDHASH
                        strPath = MakeQueryString(mPts, "void", 4);
                        break;
                    case "refund":
                        //refund?card=CARD&units=UNITVALUE&unitType=UNITTYPE&mlid=LOCATIONID&ts=UNIXTIMESTAMP&pin= PIN&qsa=GENERATEDHASH
                        strPath = MakeQueryString(mPts, "refund", 7);
                        break;
                    case "balance":
                        //balance?card=CARD&mlid=LOCATIONID&ts=UNIXTIMESTAMP&qsa=GENERATEDHASH
                        strPath = MakeQueryString(mPts, "balance", 4);
                        break;
                }



                if (strPath == "")
                {
                    switch (apiType)
                    {
                        case "earn":
                            //Default error message from Massy
                            json = @"{""response"":{ ""invoice"":""000000"",""points"":""0"",""userid"":""TERMINAL"",
                                     ""balance"":{""p"":""0"",""d"":""0.00""},""footer"":[""Earnings Footer Text""],""expiry"":{""pts"":""0"",""dat"":""1900-01-31""}},""code"":""FAIL"",""HttpStatusCode"":""900""}";
                            break;
                    }

                    MassyResponse ResponseData = JsonConvert.DeserializeObject<MassyResponse>(json);

                    return ResponseData;
                }
                else
                {

                    //_clientMassy.DefaultRequestHeaders.Add(HeaderNames.Authorization, AuthorizationHeaderHelper.GetBasic());

                    //Massy using a querystring from MakeQueryString() to collect data.
                    var response = await _clientMassy.GetAsync(strPath);

                    response.EnsureSuccessStatusCode();

                    var stringResponse = await response.Content.ReadAsStringAsync();
                    
                    MassyResponse ResponseData = JsonConvert.DeserializeObject<MassyResponse>(stringResponse);

                    //Return Json results to Form1
                    return ResponseData;
                }

                ////ReturnsFirst10CatalogItems
                //// Assert.Equal(10, model.CatalogItems.Count());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                //Default error message from Massy
                 json = @"{""response"":{ ""invoice"":""000000"",""points"":""0"",""userid"":""TERMINAL"",
                                     ""balance"":{""p"":""0"",""d"":""0.00""},""footer"":[""Earnings Footer Text""],""expiry"":{""pts"":""0"",""dat"":""1900-01-31""}},""code"":""FAIL"",""HttpStatusCode"":""900""}";

                MassyResponse ResponseData = JsonConvert.DeserializeObject<MassyResponse>(json);

                return ResponseData;

            }


        }


        private static String MakeQueryString(ICollection<MassyPoints> mPts,string ApiType, int arrlen)
        {
            //----------------------------------------------------
            // -- Make Certificate for qsa value
            //------------------------------------------------

            try
            {
                //Convert to json string
                string jsonString = JsonConvert.SerializeObject(mPts, Formatting.None, new JsonSerializerSettings()
                                    { ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore });

                //Points jsonX = JsonConvert.DeserializeObject<Points>(jsonString);
                string[] sd = new String[arrlen];
                int i = 0;

                JArray jsonX = JArray.Parse(jsonString);

                var list = from t in jsonX[0]
                           select t;

                foreach (string s in list)
                {
                    sd[i] = s;
                    i++;
                }

                //------------------------------------------------
                //card,units,unitType,mlid,ts,pin
                //------------------------------------------------
                var sequence = new[] { sd[0], sd[1], sd[2], sd[3], sd[4], sd[5] }; //, sd[6] };

                //Secret
                string key = sd[6].ToString();

                //https://github.com/tompazourek/NaturalSort.Extension
                //PM: Install-Package NaturalSort.Extension
                // Sort array to natsort standard
                var ordered = sequence.OrderBy(x => x, StringComparer.OrdinalIgnoreCase.WithNaturalSort());


                //------------------------------------------------
                //place :: to separate values
                //------------------------------------------------
                var HashString = string.Join("::", ordered);

                /*For test Massy Response
                    var tp = "1::Test";
                    string qsa2 = tp.GetHmacSHA256("e5cc16024314d0bdc48e755fffc5e563");
                    must return this value 
                    42792d6294eef612ba48419ba83b773fea72380be4fb11ff1dabfd6b5e983529
                */
                    
                    //------------------------------------------------
                //Create hash certificate
                //------------------------------------------------
                string qsa = HashString.GetHmacSHA256(key);


                //------------------------------------------------
                ////card =CARD&units=UNITVALUE&unitType=UNITTYPE&mlid=LOCATIONID&ts=UNIXTIMESTAMP&pin= PIN&qsa=GENERATEDHASH
                //Create string form submission
                //------------------------------------------------
                string queryString2 = String.Concat(mPts.Select(o => "card=" + o.card + "&units=" + o.units + "&unitType="
                                                  + o.unitType + "&mlid=" + o.mlid + "&ts=" + o.ts + "&pin=" + o.pin));


                var strPath = ClsGlobal.MassyAPIver134 + ApiType + "?" + queryString2 + "&qsa=" + qsa.ToString();



                return strPath;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "";

            }

        }


        //private getUserProfile()
        //{
        //    HttpClient _clientMassyProf = new HttpClient(); 

        //    try
        //    {

        //        _clientMassyProf.BaseAddress = new Uri(ClsGlobal.MassyAPIver134);
        //        _clientMassyProf.DefaultRequestHeaders.Accept.Clear();
        //        _clientMassyProf.DefaultRequestHeaders.Accept.Add(
        //            new MediaTypeWithQualityHeaderValue("application/json"));
        //        //http://beta.massycard.com/loyalty/massy/api/rest2/
        //        //customerProfile?card=LOYALTY&mlid=LOCATIONID&ts=UNIXTIMESTAMP&qsa=GENERATEDHASH

        //        string strPath = MakeQueryString(mPts,);

        //        return names;
        //    }
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