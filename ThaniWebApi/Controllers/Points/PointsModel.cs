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
    //N'{"Points_id":null,"Document_id":null,"PtsCustomerNo":"7678976890222","PtsFirstName":"NewCustome","PtsLastName":"LastCustomer","PtsUnitType":"P","PtsMode":"P","PtsTotal":60,"PtsValue":600.00,"PtsValueRate":0.10,"PtsDiscount":6.00,"PtsDiscountRate":0.10,"PtsLocation":"SS","PtsCashier":"Wayne"}'
    // Copy above string, then using the Special Paste from menu "Paste json to C# class" to create entity object
    public class Point
    {
        public int Points_id { get; set; }
        public int Document_id { get; set; }
        public string PtsCustomerNo { get; set; }//Card number
        public string PtsFirstName { get; set; } //Card holder First Name
        public string PtsLastName { get; set; } //Card holder Last Name
        public string PtsUnitType { get; set; } // (P or D) – P for points, D for dollars
        public string PtsMode { get; set; }  // (P or R) Purchase or Redeem
        public double PtsTotal { get; set; } //(decimal) Points 
        public double PtsValue { get; set; } //(decimal) Dollar value
        public double PtsValueRate { get; set; } //10 cents(decimal) Dollar value
        public double PtsDiscount { get; set; } //(decimal) Dollar value
        public double PtsDiscountRate { get; set; } //10 cents (decimal) Dollar value
        public string PtsLocation { get; set; } // (integer) Massy Merchant Location ID
        public string PtsCashier { get; set; }
        public int PtsMlid { get; set; } // (integer) Massy Merchant Location ID
        public int PtsUnix { get; set; } // (integer) Unix timestamp
        public int PtsPin { get; set; } // (integer)00000 5-digit user pin
        public string PtsQsa { get; set; } // (string) The generated hash
    }

    public class MassyPoints
    {
        public string Card { get; set; } //Card number
        public double Units { get; set; } //(decimal) Points or Dollar value
        public string UnitType { get; set; } // (P or D) – P for points, D for dollars
        public int Mlid { get; set; } // (integer) Massy Merchant Location ID
        public int Ts { get; set; } // (integer) Unix timestamp
        public int Pin { get; set; } // (integer)00000 5-digit user pin
        public string Qsa { get; set; } // (string) The generated hash
    }



}
