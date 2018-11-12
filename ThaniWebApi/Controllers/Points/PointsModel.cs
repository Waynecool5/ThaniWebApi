//using Insight.Database;
//using Newtonsoft.Json;
//using Newtonsoft.Json.Serialization;
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
    //N'{"Points_id":null,"Document_id":null,"ptsCustomerNo":"7678976890222","ptsFirstName":"NewCustome","ptsLastName":"LastCustomer","ptsUnitType":"P","ptsMode":"P","ptsTotal":60,"ptsValue":600.00,"ptsValueRate":0.10,"ptsDiscount":6.00,"ptsDiscountRate":0.10,"ptsLocation":"SS","ptsCashier":"Wayne"}'
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
        public string ptsPin { get; set; } // (integer)00000 5-digit user pin
        public string ptsQsa { get; set; } // (string) The generated hash
        public string ptsSecret { get; set; } // (string) The Secret for the selected store
        public string ptsInvoice { get; set; } // (string) The invoice # of process
        public string ptsLimit { get; set; } // (string) The limit to retreive data
        public string ptsfcn { get; set; }
    }

    public class MassyPoints
    {
        public string card { get; set; } //Card number
        public double units { get; set; } //(decimal) Points or Dollar value
        public string unitType { get; set; } // (P or D) – P for points, D for dollars
        public int mlid { get; set; } // (integer) Massy Merchant Location ID
        public int ts { get; set; } // (integer) Unix timestamp
        public string pin { get; set; } // (integer)00000 5-digit user pin
        //public string qsa { get; set; } // (string) The generated hash
        public string secret { get; set; } // (string) The Secret for the selected store
        public string invoice { get; set; } // (string) The invoice # of process
        public string limit { get; set; } // (string) The limit to retreive data
        public string fcn { get; set; } // (string) The fcn
    }

    public class MassyProfile
    {
        //public string ptsLocation { get; set; }
        public string card { get; set; }
        public string mlid { get; set; }
        public string pin { get; set; }
        public string secret { get; set; }
        public string ts { get; set; }
    }

    public class MassyRedeem
    {
        //public string ptsLocation { get; set; }
        public string card { get; set; }
        public double units { get; set; } //(decimal) Points or Dollar value
        public string unitType { get; set; } // (P or D) – P for points, D for dollars
        public string mlid { get; set; }
        public string pin { get; set; }
        public string secret { get; set; }
        public string ts { get; set; }
    }

    //----------------------------------------------------------------
    // Massypoint response for display
    //{"response":{"invoice":"510162","points":6,"userid":"TERMINAL",
    // "balance":{"p":122,"d":"12.20"},
    //"footer":["Earnings Footer Text"],
    //"expiry":{"pts":107,"dat":"2018-10-31"}},"code":1,"HttpStatusCode":200}
    //------------------------------------------------------
    public class MassyRespEarn
    {
        public Response response { get; set; }
        public int code { get; set; }
        public int HttpStatusCode { get; set; }
    }

    public class Response
    {
        public string invoice { get; set; }
        public int points { get; set; }
        public string userid { get; set; }
        public Balance balance { get; set; }
        public string[] footer { get; set; }
        public Expiry expiry { get; set; }
    }

    public class Balance
    {
        public int p { get; set; }
        public string d { get; set; }
    }

    public class Expiry
    {
        public int pts { get; set; }
        public string dat { get; set; }
    }


    //-------------------------------------------------
    //[JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class MassyRespProfile
    {
        public RespNames response { get; set; }
        public int code { get; set; }
        public int HttpStatusCode { get; set; }
    }

    public class RespNames
    {
        public string firstname { get; set; }
        public string lastname { get; set; }
    }

}


