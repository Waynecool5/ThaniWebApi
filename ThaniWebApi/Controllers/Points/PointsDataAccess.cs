using System.Data.SqlClient;
using System.Data;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Converters;
using System.IO;
using System.Net.Http;
using Insight.Database;
using Insight.Database.Reliable;
using Insight.Database.Providers;
using System.Collections;
using System.Net.Http.Headers;
using ThaniWebApi.Controllers.Massy;
using System.Security.Cryptography;
using NaturalSort.Extension;
using System.Reflection;

namespace ThaniWebApi.Controllers.Points
{

    //public class PointsDataAccess : IPointsDataProvider
    public class PointsDataAccess : IPointsRepository
    {
        public ICollection<Point> Points { get; set; }
        public ICollection<Point> ResultPts { get; set; }
        public ICollection<Comp> CompData { get; set; }
        public ICollection<MassyPoints> mPts { get; set; }

        public ICollection<MassyProfile> mProfile { get; set; }
     

        private HttpClient _client;

        private readonly string conn = "Data Source=" + ClsGlobal.SqlSource2 + "; Initial Catalog=" + ClsGlobal.SqlCatalog + "; Persist Security Info=True;" +
                          "User ID=" + ClsGlobal.SqlUser + ";Password=" + ClsGlobal.SqlPassword + "";

        //private readonly string conn2 = "Data Source = OFFICE\\SQL2017;Initial Catalog = WebAsync; Persist Security Info=True;User ID = sa;Password=dedan!0987o;";

        public PointsDataAccess()
        {
            _client = new HttpClient();
        }


        public async Task<IEnumerable<Point>> GetPointsAsync()
        {
            
            ////For async SQL connections
            //SqlConnectionStringBuilder database = new SqlConnectionStringBuilder(conn);
            //using (var SQLconn = database.Open())

            //For Cloud connections like AZure
            //using (var Sqlconn = new ReliableConnection<SqlConnection>(conn)) 

            using (var Sqlconn = new SqlConnection(conn))
            {
                await Sqlconn.OpenAsync();
                   
                Parm parm = new Parm { ID = -1, XMode = 0 };
                //Execute Storeprocedure for all Points

                Points = Sqlconn.Query<Point>("GetCustomerPoints",  parm); //Parameters.Empty);//,

            }


            return Points;
        }


        public async Task<IEnumerable<Comp>> GetSomeJsonAsync()
        {
            using (var Sqlconn = new SqlConnection(conn))
            {
                await Sqlconn.OpenAsync();

                Parm parm = new Parm { Mode = "select" };

                //var CompData = Sqlconn.Query("GetList", parm); //,
                //var CompData = Sqlconn.Query<Comp>("GetList", new { mode = "select" }); //,

                //Return data and place into 2 objects that are link by IList<>
                CompData = Sqlconn.Query("GetList", parm,
                                Query.Returns(Some<Comp>.Records)
                                 .ThenChildren(Some<CompList>.Records)); //, thrird object
                                                                         //id: Comp => Comp.ID,
                                                                         //into: (Comp, CompList) => beer.Glasses = CompList);

            }

            return CompData;

        }


        public async Task<MassyRespProfile> GetCustProfile(string apiType, Point Points)
        {
            string json = "";

            try
            {
                using (var Sqlconn = new SqlConnection(conn))
                {
                    await Sqlconn.OpenAsync();

                    var structure = new OneToOne<MassyProfile>(
                                     // if you don't override a column, the default rules are used 
                                     new ColumnOverride<MassyProfile>("ptsCustomerNo", "card"),
                                     new ColumnOverride<MassyProfile>("ptsMlid", "mlid"),
                                     new ColumnOverride<MassyProfile>("ptsUnix", "ts"),
                                     new ColumnOverride<MassyProfile>("ptsPin", "pin"),
                                     new ColumnOverride<MassyProfile>("ptsSecret", "secret")
                                );

                    Parm parm = new Parm { Loc_ID = Points.ptsLocation, Card = Points.ptsCustomerNo };

                    //return point object for submission to MassyAPI
                    //ResultPts = Sqlconn.Query<Point>("InsertDocuments", parm);

                    mProfile = Sqlconn.Query("GetPointsProfile", parm, Query.Returns(structure));

                    if (mProfile.Count > 0)
                    {
                        //Call MassyAPI System

                        //customerProfile?card=LOYALTY&mlid=LOCATIONID&ts=UNIXTIMESTAMP&qsa=GENERATEDHASH
                        MassyRespProfile mResp = await MassyController.GetMassyApiProfile(mProfile, apiType);

                        return mResp;
                    }
                    else
                    {

                        //Default error message from Massy
                        json = @"{""response"":{ ""invoice"":""000000"",""points"":""0"",""userid"":""TERMINAL"",
                                        ""balance"":{""p"":""0"",""d"":""0.00""},""footer"":[""Earnings Footer Text""],""expiry"":{""pts"":""0"",""dat"":""1900-01-31""}},""code"":""FAIL"",""HttpStatusCode"":""900""}";

                        MassyRespProfile mResp = JsonConvert.DeserializeObject<MassyRespProfile>(json);

                        return mResp;

                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                //Default error message from Massy
                    json = @"{""response"":{ ""invoice"":""000000"",""points"":""0"",""userid"":""TERMINAL"",
                                        ""balance"":{""p"":""0"",""d"":""0.00""},""footer"":[""Earnings Footer Text""],""expiry"":{""pts"":""0"",""dat"":""1900-01-31""}},""code"":""FAIL"",""HttpStatusCode"":""900""}";

                    MassyRespProfile mResp = JsonConvert.DeserializeObject<MassyRespProfile>(json);

                return mResp;

            }
        }

        public async Task<MassyRespEarn> DoPointsAsync(string apiType,Point Points)
        {
            try
            {
                ICollection<MassyPoints> mPts = await this.InsertPointsAsync(apiType, Points);

                switch (apiType)
                {
                    case "earn":
                        //earn?card=CARD&units=UNITVALUE&unitType=UNITTYPE&mlid=LOCATIONID&ts=UNIXTIMESTAMP&pin= PIN&qsa=GENERATEDHASH
                        //MassyPoints mPts = await InsertPointsAsync(Points);
                        return await MassyController.InsertMassyApiPoints(mPts, "earn");
                    case "redeem":
                        //redeem?card=CARD&units=UNITVALUE&unitType=UNITTYPE&mlid=LOCATIONID&ts=UNIXTIMESTAMP&pin=PIN&qsa=GENERATEDHASH
                        return await MassyController.InsertMassyApiPoints(mPts, "redeem");
                    //case "customerProfile":
                    //    //customerProfile?card=LOYALTY&mlid=LOCATIONID&ts=UNIXTIMESTAMP&qsa=GENERATEDHASH
                    //    return await MassyController.InsertMassyApiPoints(mPts, "customerProfile");
                    case "pinverify":
                        //pinverify?mlid=LOCATIONID&ts=UNIXTIMESTAMP&pin= PIN&fcn=FCN&qsa=GENERATEDHASH
                        return await MassyController.InsertMassyApiPoints(mPts, "pinverify");
                    case "history":
                        //history?card=CARD&mlid=LOCATIONID&limit=NUMBEROFRECORDSTORETURN&qsa=GENERATEDHASH
                        return await MassyController.InsertMassyApiPoints(mPts, "history");
                    case "void":
                        //void?invoice=INVOICE#&mlid=LOCATIONID&ts=UNIXTIMESTAMP&pin= PIN&qsa=GENERATEDHASH
                        return await MassyController.InsertMassyApiPoints(mPts, "void");
                    case "refund":
                        //refund?card=CARD&units=UNITVALUE&unitType=UNITTYPE&mlid=LOCATIONID&ts=UNIXTIMESTAMP&pin= PIN&qsa=GENERATEDHASH
                        return await MassyController.InsertMassyApiPoints(mPts, "refund");
                    case "balance":
                        //balance?card=CARD&mlid=LOCATIONID&ts=UNIXTIMESTAMP&qsa=GENERATEDHASH
                        return await MassyController.InsertMassyApiPoints(mPts, "balance");
                    default:
                        return new MassyRespEarn();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }


        public async Task<ICollection<MassyPoints>> InsertPointsAsync(string apiType, Point Points)
        {
            var strVal = new StringBuilder();
            //int i = 0;
            string [] vs= new string[] {""};

            strVal.Append(JsonConvert.SerializeObject(Points));

            using (var Sqlconn = new SqlConnection(conn))
            {
                await Sqlconn.OpenAsync();

                // the structure object is for remapping results from SQL to Massypoints Class Object
               // var structure = getStucture(apiType);

                var structure = new OneToOne<MassyPoints>(
                     // if you don't override a column, the default rules are used 
                     new ColumnOverride<MassyPoints>("ptsCustomerNo", "card"),
                     new ColumnOverride<MassyPoints>("ptsTotal", "units"),
                     new ColumnOverride<MassyPoints>("ptsUnitType", "unitType"),
                     new ColumnOverride<MassyPoints>("ptsMlid", "mlid"),
                     new ColumnOverride<MassyPoints>("ptsUnix", "ts"),
                     new ColumnOverride<MassyPoints>("ptsPin", "pin"),
                     // new ColumnOverride<MassyPoints>("ptsQsa", "qsa"),
                     new ColumnOverride<MassyPoints>("ptsSecret", "secret"),
                     new ColumnOverride<MassyPoints>("ptsInvoice", "invoice"),
                     new ColumnOverride<MassyPoints>("ptsLimit", "limit"),
                     new ColumnOverride<MassyPoints>("ptsfcn", "fcn")
                );

                Parm parm = new Parm { Document = strVal.ToString() };

                //return point object for submission to MassyAPI
                //ResultPts = Sqlconn.Query<Point>("InsertDocuments", parm);

                 mPts = Sqlconn.Query("InsertDocuments", parm, Query.Returns(structure));

                if (mPts.Count > 0)
                {
                    //card,units,unitType,mlid,ts,pin
  
                    return mPts;
                    

                    // //ICollection<MassyPoints> mp = IMapper.Map(ResultPts);
                    //dynamic mp = ResultPts.Select(x => new MassyPoints()
                    // {
                    //     card = x.ptsCustomerNo,
                    //     units = x.ptsTotal,
                    //     unitType = x.ptsUnitType,
                    //     mlid = x.ptsMlid,
                    //     ts = x.ptsUnix,
                    //     pin = x.ptsPin,
                    //     qsa = x.ptsQsa
                    // });


                    ////convert to string
                    //String json = JsonConvert.SerializeObject(mp, Formatting.None);
                    // String jsonStr =  json.Replace("\",\"","");


                    //convert to MassyPoints Object
                    //mPts = JsonConvert.DeserializeObject<ICollection<MassyPoints>>(jsonStr); // (JsonConvert.SerializeObject(mPts, Formatting.None));

                    //return mPts;
                    //return JsonConvert.DeserializeObject<MassyPoints>(str.ToString()); // Convert.ToBoolean(results);

                    //Send to MassyApi
                    //this.InsertMassyApiPoints(mPts);
                }
                else
                {
                    return null;
                }
            }



        }



        //public async Task<IEnumerable<Point>> GetSomeJsonAsync()
        //{
        //    //var obj = new clsSqlJson();
        //    var jsonResult = new StringBuilder();

        //    SqlDataReader reader = null;

        //    // SQL string conn
        //    //string conn = this.ConnSqlString();

        //    using (var SQLconn = new SqlConnection(conn))
        //    {
        //        await SQLconn.OpenAsync();

        //        // using (SqlCommand cmd = new SqlCommand("dbo.GetList", SQLconn)) //"SELECT FROM... FOR JSON PATH", SQLconn)
        //        SqlCommand cmd = SQLconn.CreateCommand();

        //        cmd.CommandText = "GetList";
        //        cmd.CommandType = CommandType.StoredProcedure;

        //        cmd.Parameters.Clear();

        //        SqlParameter pmMode = new SqlParameter("@mode", SqlDbType.VarChar, 10);
        //        pmMode.Direction = ParameterDirection.Input;
        //        //paramResults.IsNullable = true;
        //        pmMode.Value = "select";
        //        cmd.Parameters.Add(pmMode);

        //        //SqlParameter pmResults = new SqlParameter("@results", SqlDbType.NVarChar, -1);
        //        //pmResults.Direction = ParameterDirection.Output;
        //        ////pmResults.IsNullable = true;
        //        //pmResults.Value = "";
        //        //cmd.Parameters.Add(pmResults);

        //        //----------------------------------------------
        //        //var sqlReader = cmd.ExecuteReader(System.Data.CommandBehavior.SequentialAccess);


        //        //SQLconn.Open();

        //        using (reader = await cmd.ExecuteReaderAsync())
        //        {
        //            while (reader.Read())
        //            {
        //                if (!reader.HasRows)
        //                {
        //                    jsonResult.Append("[]");
        //                }
        //                else
        //                {
        //                    jsonResult.Append(reader.GetValue(0).ToString());
        //                }
        //            }
        //        }

        //        SQLconn.Close();

        //    }

        //    Points = JsonConvert.DeserializeObject<ICollection<Point>>(jsonResult.ToString());
        //    return Points;

        //    //var Points1 = JsonConvert.DeserializeObject<ICollection<Point>>(jsonResult.ToString());
        //    // return new System.IO.MemoryStream(System.Text.Encoding.UTF8.GetBytes(Points1.ToString()));

        //    // var stream = new MemoryStream();
        //    //Points1.s.SaveAs(stream);

        //    // return await _client.GetStreamAsync(Points1.ToString());

        //    // throw new NotImplementedException();
        //}

        //public async Task Report()
        //{
        //    await Sql.Stream(@"SELECT color AS x, AVG(price) AS y FROM product GROUP BY color FOR JSON PATH",
        //  Response.Body, '[]');
        //}


        // Application transferring a large Text File to SQL Server in .Net 4.5  

        //public async Task StreamTextToServer()
        //{
        //    using (SqlConnection SQLconn = new SqlConnection(conn))
        //    {
        //        await SQLconn.OpenAsync();

        //        using (SqlCommand cmd = new SqlCommand("INSERT INTO [TextStreams] (textdata) VALUES (@textdata)", SQLconn))
        //        {
        //            using (StreamReader file = File.OpenText("textdata.txt"))
        //            {

        //                // Add a parameter which uses the StreamReader we just opened  
        //                // Size is set to -1 to indicate "MAX"  
        //                cmd.Parameters.Add("@textdata", SqlDbType.NVarChar, -1).Value = file;

        //                // Send the data to the server asynchronously  
        //                await cmd.ExecuteNonQueryAsync();
        //            }
        //        }
        //    }
        //}


        //public async Task<Stream> GetDataPoints(string name)
        //{ //Return Stream Data
        //    var urlBlob = string.Empty;
        //    switch (name)
        //    {
        //        case "earth":
        //            urlBlob = "https://anthonygiretti.blob.core.windows.net/videos/earth.mp4";
        //            break;
        //        case "nature1":
        //            urlBlob = "https://anthonygiretti.blob.core.windows.net/videos/nature1.mp4";
        //            break;
        //        case "nature2":
        //        default:
        //            urlBlob = "https://anthonygiretti.blob.core.windows.net/videos/nature2.mp4";
        //            break;
        //    }
        //    return await _client.GetStreamAsync(urlBlob);
        //}

        // Application transferring a large Text File from SQL Server in .NET 4.5  
        //public async Task<Stream> PrintTextValues()
        //{
        //    using (SqlConnection connection = new SqlConnection(conn))
        //    {
        //        await connection.OpenAsync();
        //        using (SqlCommand command = new SqlCommand("SELECT [id], [textdata] FROM [Streams]", connection))
        //        {

        //            // The reader needs to be executed with the SequentialAccess behavior to enable network streaming  
        //            // Otherwise ReadAsync will buffer the entire text document into memory which can cause scalability issues or even OutOfMemoryExceptions  
        //            using (SqlDataReader reader = await command.ExecuteReaderAsync(CommandBehavior.SequentialAccess))
        //            {
        //                while (await reader.ReadAsync())
        //                {
        //                    Console.Write("{0}: ", reader.GetInt32(0));

        //                    if (await reader.IsDBNullAsync(1))
        //                    {
        //                        Console.Write("(NULL)");
        //                    }
        //                    else
        //                    {
        //                        char[] buffer = new char[4096];
        //                        int charsRead = 0;
        //                        using (TextReader data = reader.GetTextReader(1))
        //                        {
        //                            do
        //                            {
        //                                // Grab each chunk of text and write it to the console  
        //                                // If you are writing to a TextWriter you should use WriteAsync or WriteLineAsync  
        //                                charsRead = await data.ReadAsync(buffer, 0, buffer.Length);
        //                               // return new MemoryStream(buffer, 0, charsRead);
        //                                //Console.Write(buffer, 0, charsRead);
        //                            } while (charsRead > 0);
        //                        }
        //                    }

        //                   // Console.WriteLine();
        //                }
        //            }
        //        }
        //    }
        //}

        public StringBuilder GetSomeJson()
        {
            //var obj = new clsSqlJson();
            var jsonResult = new StringBuilder();

            SqlDataReader reader = null;

            // SQL string conn
            // string conn = ConnSqlString();

            using (var SQLconn = new SqlConnection(conn))
            {

                // using (SqlCommand cmd = new SqlCommand("dbo.GetList", SQLconn)) //"SELECT FROM... FOR JSON PATH", SQLconn)
                SqlCommand cmd = SQLconn.CreateCommand();

                cmd.CommandText = "GetList";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Clear();

                SqlParameter pmMode = new SqlParameter("@mode", SqlDbType.VarChar, 10);
                pmMode.Direction = ParameterDirection.Input;
                //paramResults.IsNullable = true;
                pmMode.Value = "select";
                cmd.Parameters.Add(pmMode);

                //SqlParameter pmResults = new SqlParameter("@results", SqlDbType.NVarChar, -1);
                //pmResults.Direction = ParameterDirection.Output;
                ////pmResults.IsNullable = true;
                //pmResults.Value = "";
                //cmd.Parameters.Add(pmResults);

                //----------------------------------------------
                //var sqlReader = cmd.ExecuteReader(System.Data.CommandBehavior.SequentialAccess);


                SQLconn.Open();

                using (reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (!reader.HasRows)
                        {
                            jsonResult.Append("[]");
                        }
                        else
                        {
                            jsonResult.Append(reader.GetValue(0).ToString());
                        }
                    }
                }

                SQLconn.Close();

            }


            //return new ObjectResult(obj);

            return jsonResult;
        }

    }

    internal class Parm
    {
        
        public string Mode { get; set; }
        public string Document { get; set; }
        public int ID { get; set; }
        public int XMode { get; set; }
        public string Loc_ID { get; set; }
        public string Card { get; set; }
    }

    internal class MassyCard
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


