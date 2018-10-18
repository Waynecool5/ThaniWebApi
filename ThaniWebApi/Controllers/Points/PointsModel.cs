using Insight.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ThaniWebApi.Controllers.Points
{
    //return Json from SQL2016
    //[{ "ID":1,"Comp_ID":1,"InternalRef":"BB01-001-00000001","CompList":[{"Comp_ID":1,"CompName":"UBRITE","VAT":"6806200090"}]}]
    // Copy above string, then using the Special Paste from menu "Paste json to C# class" to create entity object
    public class Comp
    {
        public int Comp_ID { get; set; }
        public string CompName { get; set; }
        public string VAT { get; set; }
        public IList<CompList> CompList { get; set; }  //  [Column(SerializationMode=SerializationMode.Json)] for Insight.Database

    }

    public class CompList
    {
        public int Comp_ID { get; set; }
        public string InternalRef { get; set; }
        public int ID { get; set; }
    }



    //Return Sql2016 json results
    //{"Points_id":null,"Document_id":null,"ptsCustomerNo":"7678976890222","ptsUnitType":"P","ptsMode":"P","ptsTotal":60,"ptsValue":600.00,"ptsDiscount":6.00,"ptsLocation":"SS","ptsCashier":"Wayne"}'
    // Copy above string, then using the Special Paste from menu "Paste json to C# class" to create entity object
    public class Point
    {
        public int Points_id { get; set; }
        public int Document_id { get; set; }
        public string ptsCustomerNo { get; set; }//Card number
        public string ptsFirstName { get; set; } //Card holder First Name
        public string ptsLastName { get; set; } //Card holder Last Name
        public string ptsUnitType { get; set; } // (P or D) – P for points, D for dollars
        public string ptsMode { get; set; }  // (P or R) Purchase or Redeem
        public double ptsTotal { get; set; } //(decimal) Points 
        public double ptsValue { get; set; } //(decimal) Dollar value
        public double ptsValueRate { get; set; } //10 cents(decimal) Dollar value
        public double ptsDiscount { get; set; } //(decimal) Dollar value
        public double ptsDiscountRate { get; set; } //10 cents (decimal) Dollar value
        public string ptsLocation { get; set; } // (integer) Massy Merchant Location ID
        public string ptsCashier { get; set; }
        public int ptsMlid { get; set; } // (integer) Massy Merchant Location ID
        public int ptsUnix { get; set; } // (integer) Unix timestamp
        public int ptsPin { get; set; } // (integer)00000 5-digit user pin
        public string ptsQsa { get; set; } // (string) The generated hash
    }

    public class MassyPoints
    {
        public string card { get; set; } //Card number
        public double units { get; set; } //(decimal) Points or Dollar value
        public string unitType { get; set; } // (P or D) – P for points, D for dollars
        public int mlid { get; set; } // (integer) Massy Merchant Location ID
        public int ts { get; set; } // (integer) Unix timestamp
        public int pin { get; set; } // (integer)00000 5-digit user pin
        public string qsa { get; set; } // (string) The generated hash
    }



}
