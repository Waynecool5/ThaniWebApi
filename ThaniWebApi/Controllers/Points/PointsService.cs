using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using ThaniWebApi.Controllers.Points;

namespace ThaniWebApi.Controllers.Points
{
    public class PointsService
    {
        PointsDataAccess objPoints = new PointsDataAccess();


        //return Json from SQL2016
        //[{ "ID":1,"Comp_ID":1,"InternalRef":"BB01-001-00000001","CompList":[{"Comp_ID":1,"CompName":"UBRITE","VAT":"6806200090"}]}]
        // using the Special Paste "Paste json to C# class" to create entity object

        public class Point
        {
            public int ID { get; set; }
            public int Comp_ID { get; set; }
            public string InternalRef { get; set; }
            public IList<CompList> CompList { get; set; }
        }

        public class CompList
        {
            public int Comp_ID { get; set; }
            public string CompName { get; set; }
            public string VAT { get; set; }
        }


        public ICollection<Point> Points { get; set; }

        public dynamic GetClientPointsAsync()
        {

           // return objPoints.GetSomeJson().ToString();
            var jsonPoints = objPoints.GetSomeJson().ToString();


            //dynamic resultsPoints = JsonConvert.DeserializeObject(jsonPoints);
            //return resultsPoints;

            ////  Points jsPoints = Json. <Points>(resultsPoints);
            //  var resultsPoints = JsonConvert.DeserializeObject<Point>(jsonPoints);

            Points = JsonConvert.DeserializeObject<ICollection<Point>>(jsonPoints);
            return Points;
            // var resultsPoints = JsonConvert.DeserializeObject<Point>(jsonPoints, new ExpandoObjectConverter());
            //dynamic resultsPoints = JsonConvert.DeserializeObject<Point>(jsonPoints);
            //return jsonPoints;

        }

    }


}
