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

namespace ThaniWebApi.Controllers.Points
{

    //public class PointsDataAccess : IPointsDataProvider
    public class PointsDataAccess : IPointsRepository
    {
        public ICollection<Point> Points { get; set; }
        public ICollection<Point> msPoints { get; set; }
        public ICollection<Comp> CompData { get; set; }

        private HttpClient _client;

        private readonly string conn = "Data Source=" + clsGlobal.SqlSource2 + "; Initial Catalog=" + clsGlobal.SqlCatalog + "; Persist Security Info=True;" +
                          "User ID=" + clsGlobal.SqlUser + ";Password=" + clsGlobal.SqlPassword + "";

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
                   
                Parm parm = new Parm { ID = -1, xMode = 0 };
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

                Parm parm = new Parm { mode = "select" };

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


        public async Task<bool> InsertPointsAsync(Point Points)
        {
            var strVal = new StringBuilder();

            strVal.Append(JsonConvert.SerializeObject(Points));

            using (var Sqlconn = new SqlConnection(conn))
            {
                await Sqlconn.OpenAsync();

                Parm parm = new Parm { document = strVal.ToString() };

                //return point object for submission to MassyAPI
                msPoints = Sqlconn.Query<Point>("InsertDocuments", parm);

                if (msPoints.Count > 0)
                {
                    //submit to massyapi

                }
            }

            var results = 1;
            strVal.Clear();


            return Convert.ToBoolean(results);
        }

        public async Task InsertMassyApiPoints(Point msPoints)
        {

            _client.BaseAddress = new Uri(clsGlobal.MassyAPIver134);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));


            //Get date Massy API "/api/catalog/list"
            var strPath = clsGlobal.MassyAPIver134 + "" + vStr
            var response = await _client.GetAsync("MassyAPI");
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<>(stringResponse);

            //ReturnsFirst10CatalogItems
            // Assert.Equal(10, model.CatalogItems.Count());
        }

        //public async Task UpdatePointsAsync(Point request)
        //{
        //    using (var stream = await Request.Content.ReadAsStreamAsync())
        //    {
        //        var data = JsonConvert.DeserializeObject<ICollection<Point>>(stream);

        //        var strVal = new StringBuilder();

        //        strVal.Append(JsonConvert.SerializeObject(data));


        //        using (var Sqlconn = new SqlConnection(conn))
        //        {
        //            await Sqlconn.OpenAsync();


        //            Parm parm = new Parm { document = "" + strVal.ToString() };

        //            var results = Sqlconn.Query("InsertDocuments", parm);

        //        }
        //    }
        //}

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

        public async Task StreamTextToServer()
        {
            using (SqlConnection SQLconn = new SqlConnection(conn))
            {
                await SQLconn.OpenAsync();

                using (SqlCommand cmd = new SqlCommand("INSERT INTO [TextStreams] (textdata) VALUES (@textdata)", SQLconn))
                {
                    using (StreamReader file = File.OpenText("textdata.txt"))
                    {

                        // Add a parameter which uses the StreamReader we just opened  
                        // Size is set to -1 to indicate "MAX"  
                        cmd.Parameters.Add("@textdata", SqlDbType.NVarChar, -1).Value = file;

                        // Send the data to the server asynchronously  
                        await cmd.ExecuteNonQueryAsync();
                    }
                }
            }
        }

        
        public async Task<Stream> GetDataPoints(string name)
        { //Return Stream Data
            var urlBlob = string.Empty;
            switch (name)
            {
                case "earth":
                    urlBlob = "https://anthonygiretti.blob.core.windows.net/videos/earth.mp4";
                    break;
                case "nature1":
                    urlBlob = "https://anthonygiretti.blob.core.windows.net/videos/nature1.mp4";
                    break;
                case "nature2":
                default:
                    urlBlob = "https://anthonygiretti.blob.core.windows.net/videos/nature2.mp4";
                    break;
            }
            return await _client.GetStreamAsync(urlBlob);
        }

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
        
        public string mode { get; set; }
        public string document { get; set; }
        public int ID { get; set; }
        public int xMode { get; set; }
        
    }
}

