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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace ThaniWebApi.Controllers.Points
{
    
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class PointsController : ControllerBase
    {

        //private IPointsDataProvider PointsDataProvider;
        private IPointsRepository _PointsRepository;

        //public PointsController(IPointsDataProvider PointsDataProvider)
        //{
        //    this.PointsDataProvider = PointsDataProvider;
        //}

        public PointsController(IPointsRepository PointsRepository)
        {
            _PointsRepository = PointsRepository;
        }

        //----------------------------------------------
        [AllowAnonymous]
        [HttpGet]
        [Route("GetSomeJsonAsync")]
        public async Task<IEnumerable<Comp>> GetSomeJsonAsync()
        {
            return await _PointsRepository.GetSomeJsonAsync();
            //return await this.PointsDataProvider.GetSomeJsonAsync();
        }


        [HttpGet]
        [Route("GetPointsAsync")]
        public async Task<IEnumerable<Point>> GetPointsAsync()
        {

            return await _PointsRepository.GetPointsAsync();
        }

        //----------------------------------------------

        [HttpPost]
        [Route("GetCustProfile")]
        public async Task<MassyRespProfile> GetCustProfile(string apiType,[FromBody] Point Points)
        {
            return await _PointsRepository.GetCustProfile(apiType, Points);
        }

        //----------------------------------------------

        [HttpPost]
        [Route("GetRedeemProfile")]
        public async Task<MassyRespEarn> GetRedeemProfile(string apiType, [FromBody] Point Points)
        {
            return await _PointsRepository.GetRedeem(apiType, Points);
        }

        //----------------------------------------------

        [HttpPost]
        [Route("DoPointsAsync")]
        public async Task<MassyRespEarn> DoPointsAsync( string apiType,[FromBody] Point Points)
        {

            return await _PointsRepository.DoPointsAsync(apiType, Points);
            
         }

        

        //[HttpPost]
        //[Route("InsertMassyApiPoints")]
        //public async Task<dynamic> InsertMassyApiPoints(MassyPoints mPts)
        //{
        //    return await this.InsertMassyApiPoints(mPts);

        //}


        //[HttpGet]
        //public HttpResponseMessage StreamContent(int id)
        //{  //Sending image from a file via streamimg chunk
        //    var response = Request.CreateResponse();

        //    response.Content = new StreamContent(
        //        File.OpenRead(
        //            HostingEnvironment.MapPath(GetPathFor(id))));

        //    response.Content.Headers.ContentType =
        //        new MediaTypeHeaderValue("image/jpeg");

        //    return response;
        //}

        //[HttpGet]
        //public HttpResponseMessage PushStreamContent()
        //{
        //    var response = Request.CreateResponse();

        //    response.Content = new PushStreamContent((stream, content, context) =>
        //        {
        //            foreach (var staffMember in _staffMembers)
        //            {
        //                var serializer = new JsonSerializer();
        //                using (var writer = new StreamWriter(stream))
        //                {
        //                    serializer.Serialize(writer, staffMember);
        //                    stream.Flush();
        //                }
        //            }
        //        });

        //    return response;
        //}

        //[HttpPost]
        //public async Task<HttpResponseMessage> ReadStream(int id)
        //{
        //    using (var stream = await Request.Content.ReadAsStreamAsync())
        //    {
        //        var fileStream = File.OpenWrite(
        //            HostingEnvironment.MapPath(GetPathFor(id)));

        //        stream.CopyTo(fileStream);

        //        fileStream.Close();
        //    }

        //    return Request.CreateResponse(HttpStatusCode.OK);
        //}

        //[HttpPost]
        //public async Task Post([FromBody]User user)
        //{
        //    await this.userDataProvider.AddUser(user);
        //}

        //[HttpPut("{id}")]
        //public async Task Put(int UserId, [FromBody]User user)
        //{
        //    await this.userDataProvider.UpdateUser(user);
        //}

        //[HttpDelete("{id}")]
        //public async Task Delete(int UserId)
        //{
        //    await this.userDataProvider.DeleteUser(UserId);
        //}


        //---------------------------------------------
        //PointsService svr_Points = new PointsService();

        //[Route("api/GetPoints")]
        //public ActionResult GetClientPoints()
        //{
        //    var result = svr_Points.GetClientPointsAsync();
        //    if (result == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(result);

        //    //var Point =svr_Points.GetClientPointsAsync();

        //    //return Point;

        //    // return new string[] { "value1", "value2" };
        //    // return await svr_Points.GetClientPointsAsync();

        //    //return ok(jsonPoints);
        //}

        //// GET api/values/5
        //[HttpGet("{id}")]
        //public ActionResult<string> Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/values
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/values/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/values/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}



    }

}