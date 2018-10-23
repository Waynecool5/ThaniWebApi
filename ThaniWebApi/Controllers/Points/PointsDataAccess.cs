using System.Data.SqlClient;
using System.Data;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
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
using Newtonsoft.Json.Linq;
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


        public async Task<MassyResponse> DoPointsAsync(Point Points)
        {
            try
            {
                ICollection<MassyPoints> mPts = await this.InsertPointsAsync(Points);

                //MassyPoints mPts = await InsertPointsAsync(Points);
                return await MassyController.InsertMassyApiPoints(mPts);
              }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }


        public async Task<ICollection<MassyPoints>> InsertPointsAsync(Point Points)
        {
            var strVal = new StringBuilder();
            int i = 0;
            string [] vs= new string[] {""};

            strVal.Append(JsonConvert.SerializeObject(Points));

            using (var Sqlconn = new SqlConnection(conn))
            {
                await Sqlconn.OpenAsync();

                // the structure object is for remapping results to Massypoints
                var structure = new OneToOne<MassyPoints>(
                    // if you don't override a column, the default rules are used 
                     new ColumnOverride<MassyPoints>("ptsCustomerNo", "card"),
                     new ColumnOverride<MassyPoints>("ptsTotal", "units"),
                     new ColumnOverride<MassyPoints>("ptsUnitType", "unitType"),
                     new ColumnOverride<MassyPoints>("ptsMlid", "mlid"),
                     new ColumnOverride<MassyPoints>("ptsUnix", "ts"),
                     new ColumnOverride<MassyPoints>("ptsPin", "pin"),
                     new ColumnOverride<MassyPoints>("ptsQsa", "qsa"),
                     new ColumnOverride<MassyPoints>("ptsSecret", "ptsSecret")
                );

                Parm parm = new Parm { Document = strVal.ToString() };

                //return point object for submission to MassyAPI
                //ResultPts = Sqlconn.Query<Point>("InsertDocuments", parm);

                 mPts = Sqlconn.Query("InsertDocuments", parm, Query.Returns(structure));

                if (mPts.Count > 0)
                {
                    //card,units,unitType,mlid,ts,pin
                   var tmp = mPts.SelectMany(element => element.ToString());


                    IEnumerable<string> tmps = from mPt in mPts
                                            where mPt.ts > 0
                                            select new { card = mPt.card, units = mPt.units }; //, mPt.unitType, mPt.mlid, mPt.ts, mPt.pin };
                           



                    string[] cmp = (from o in mPts
                                    select o.card + "::" + o.units + "::" + o.unitType + "::" + o.mlid + "::" + o.ts + "::" + o.pin).ToArray();

                    //https://github.com/tompazourek/NaturalSort.Extension
                    //PM: Install-Package NaturalSort.Extension
                    var ordered = cmp.OrderBy(x => x, StringComparer.OrdinalIgnoreCase.WithNaturalSort());

                    string[] array = new string[mPts.Count];
                    array.CopyTo(array, 0);
                    IList<string> s = ordered as List<string>;
                    //Gernerate Hash

                    string qsa = ordered.GetHmacSHA256(s.ToString());

                    var vQueryString = (JsonConvert.SerializeObject(mPts));
                    String v1 = vQueryString.Replace("[", "");
                    String v2 = v1.Replace("]", "");
                    var json = JsonConvert.DeserializeObject(v2);

                    var jObj = (JObject)JsonConvert.DeserializeObject(v2);

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

            //var results = 1;
            //strVal.Clear();

        }



        //public async Task<dynamic> InsertMassyApiPoints(MassyPoints mPts)
        //  {
        //      dynamic model;
        //      HttpClient _clientMassy = new HttpClient();

        //      try
        //      {
        //          _clientMassy.BaseAddress = new Uri(ClsGlobal.MassyAPIver134);
        //          _clientMassy.DefaultRequestHeaders.Accept.Clear();
        //          _clientMassy.DefaultRequestHeaders.Accept.Add(
        //              new MediaTypeWithQualityHeaderValue("application/json"));

        //          var queryString = mPts.GetQueryString("earn");

        //          //Get date Massy API "/api/catalog/list"
        //          var strPath = ClsGlobal.MassyAPIver134 + "" + queryString.ToString();

        //          var response = await _clientMassy.GetAsync("MassyAPI");

        //          response.EnsureSuccessStatusCode();

        //          var stringResponse = await response.Content.ReadAsStringAsync();

        //          model = JsonConvert.DeserializeObject<dynamic>(stringResponse);

        //          return model;
        //          //ReturnsFirst10CatalogItems
        //          // Assert.Equal(10, model.CatalogItems.Count());
        //      }
        //      catch (Exception ex)
        //      {
        //          //Console.WriteLine( ex.Message);
        //          return ex.Message;
        //      }


        //  }



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


