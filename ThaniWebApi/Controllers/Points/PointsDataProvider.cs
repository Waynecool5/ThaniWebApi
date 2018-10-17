using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ThaniWebApi.Controllers.Points;

namespace ThaniWebApi.Controllers.Points
{

    public interface IPointsDataProvider
    {
        //Interface defines a contract between our application and
        //other objects.This indicates what sort of methods, properties
        //and events are exposed by our user object.

        Task<IEnumerable<Point>> GetSomeJsonAsync();

        //Task<Stream> GetSomeJsonAsync();

        Task<Stream> GetDataPoints(string name);

        Task StreamTextToServer();
    }
}
