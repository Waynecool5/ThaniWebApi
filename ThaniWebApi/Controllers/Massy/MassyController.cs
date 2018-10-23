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

                var queryString =  mPts.GetQueryString("");

                ////Get date Massy API "/api/catalog/list"
                ////submit to massyapi
                ////HTTP GET Request sent to the below URL for massy points
                ////http://beta.massycard.com/loyalty/massy/api/rest2/earn?
                ////card =CARD&units=UNITVALUE&unitType=UNITTYPE&mlid=LOCATIONID&ts=UNIXTIMESTAMP&pin= PIN&qsa=GENERATEDHASH

                var strPath = ClsGlobal.MassyAPIver134 + "earn?" + queryString.ToString();



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

}