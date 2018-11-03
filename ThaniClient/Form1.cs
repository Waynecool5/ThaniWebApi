using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Insight.Database;
using Newtonsoft.Json.Linq;
using System.Net;
using Newtonsoft.Json;

namespace ThaniClient
{


    public partial class Form1 : Form
    {

        public ICollection<POSSale> PosSale { get; set; }

        static HttpClient _client = new HttpClient();

        static clsWinGlobal wcls = new clsWinGlobal();

        //static ICollection<TotalPoints> Tpoints { get; set; }
        static MassyResponse Tpoints = null;
        static UserModel Token = null;

        private readonly string conn = "Data Source=" + ClsGlobal.SqlSource + "; Initial Catalog=" + ClsGlobal.SqlCatalog + "; Persist Security Info=True;" +
                  "User ID=" + ClsGlobal.SqlUser + ";Password=" + ClsGlobal.SqlPassword + "";

        public Form1()
        {
            InitializeComponent();

            ////Call Thani's Web Api
            //_client.BaseAddress = new Uri("http://localhost:54574/");// https://localhost:44305/"); 
            //_client.DefaultRequestHeaders.Accept.Clear();
            //_client.DefaultRequestHeaders.Accept.Add(
            //    new MediaTypeWithQualityHeaderValue("application/json"));
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            this.btnRedeem.Text = "Send Points";
           // GetSale();
            //GetSale1();
        }

        private void btnRedeem_ClickAsync(object sender, EventArgs e)
        {

            if (this.btnRedeem.Text == "Send Points")
            {
                this.AddSalesPoints();

               }
            else if (this.btnRedeem.Text == "Redeem Points")
            {

                }
        }

        internal class Parm
        {

            public int StoreID { get; set; }
            //public int Transaction { get; set; }

        }

        private async void GetSale()
        {
            using (var Sqlconn = new SqlConnection(conn))
            {
                await Sqlconn.OpenAsync();

                Parm parm = new Parm { StoreID = 1 };

                //Execute Storeprocedure for all Points
                try { 
                    PosSale = Sqlconn.Query<POSSale>("UBPOSGetLoyaltyTransactions", parm); //Parameters.Empty);//,

                    ////Return data and place into 2 objects that are link by IList<>
                    //PosSale = Sqlconn.Query("UBPOSGetLoyaltyTransactions", parm,
                    //                Query.Returns(Some<Comp>.Records)
                    //                 .ThenChildren(Some<CompList>.Records)); //, thrird object
                    //                                                         //id: Comp => Comp.ID,
                                                                             //into: (Comp, CompList) => beer.Glasses = CompList);


                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }


        }

        private async void GetSale1()
        {

            SqlDataReader reader = null;

            using (var SQLconn = new SqlConnection(conn))
            {
                await SQLconn.OpenAsync();
                // using (SqlCommand cmd = new SqlCommand("dbo.GetList", SQLconn)) //"SELECT FROM... FOR JSON PATH", SQLconn)
                SqlCommand cmd = SQLconn.CreateCommand();

                cmd.CommandText = "UBPOSGetLoyaltyTransactions";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Clear();

                SqlParameter StoreID = new SqlParameter("@StoreID", SqlDbType.Int, 10);
                StoreID.Direction = ParameterDirection.Input;
                StoreID.Value = 1;
                cmd.Parameters.Add(StoreID);


                try
                {

                    using (reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (!reader.HasRows)
                            {

                            }
                            else
                            {
                                radioButton1.Select();
                                //label19.Text = reader.GetValue(0).ToString();
                                label19.Text = reader.GetValue(1).ToString();
                                txtCus.Text = reader.GetValue(9).ToString(); //card number
                                label20.Text = Convert.ToDateTime(reader.GetValue(3).ToString()).ToShortDateString(); //Date
                                // txtCus.Text = reader.GetValue(4).ToString();
                                //txtCus.Text = reader.GetValue(5).ToString();
                                txtSales.Text = reader.GetValue(6).ToString(); //Total Sales
                                decimal p = Convert.ToDecimal((Convert.ToDecimal(reader.GetValue(6).ToString()) / 10).ToString());
                                txtTPoints.Text = Math.Floor(p).ToString();
                                //txtCus.Text = reader.GetValue(7).ToString();

                                string[] n = reader.GetValue(8).ToString().Split(' ');
                                if (n.Length > 0)
                                { txtFname.Text = n[0]; }
                                if (n.Length > 1)
                                { txtLname.Text = n[1]; }
                                //txtCus.Text = reader.GetValue(9).ToString();
                                // txtFname.Text = reader.GetValue(10).ToString();
                                txtLoca.Text = reader.GetValue(11).ToString();
                                txtCashier.Text = reader.GetValue(12).ToString();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally {
                    SQLconn.Close();
                }


            }


        }


    private async void AddSalesPoints()
        {
            ////Default error message from Massy
            //string json = @"{""response"":{ ""invoice"":""000000"",""points"":""0"",""userid"":""TERMINAL"",
            //                         ""balance"":{""p"":""0"",""d"":""0.00""},""footer"":[""Earnings Footer Text""],""expiry"":{""pts"":""0"",""dat"":""1900-01-31""}},""code"":""FAIL"",""HttpStatusCode"":""900""}";

            try
            {
                //dynamic mp = PosSale.Select(x => new MassyPoints()
                // {
                //     card = x.ptsCustomerNo,
                //     units = x.ptsTotal,
                //     unitType = x.ptsUnitType,
                //     mlid = x.ptsMlid,
                //     ts = x.ptsUnix,
                //     pin = x.ptsPin,
                //     qsa = x.ptsQsa
                // });

                //var points = new Point
                //{
                //    Points_id = -1,
                //    Document_id = -1,
                //    ptsCustomerNo = PosSale. txtCus.Text,
                //    ptsFirstName = txtFname.Text,
                //    ptsLastName = txtLname.Text,
                //    ptsUnitType = "D",
                //    ptsMode = "D",
                //    ptsTotal = Convert.ToDouble(txtPoints.Text),
                //    ptsValue = Convert.ToDouble(txtPoints.Text) * 10,
                //    ptsValueRate = .10,
                //    ptsDiscount = 6.00,
                //    ptsDiscountRate = .10,
                //    ptsLocation = txtLname.Text,
                //    ptsCashier = txtCashier.Text
                //};

                var points = new Point
                {
                    Points_id = -1,
                    Document_id = -1,
                    ptsCustomerNo = "42100999892",
                    ptsFirstName = "Test",
                    ptsLastName = "Testers",
                    ptsUnitType = "D",
                    ptsMode = "D",
                    ptsTotal = 60,
                    ptsValue = 600.00,
                    ptsValueRate = .10,
                    ptsDiscount = 6.00,
                    ptsDiscountRate = .10,
                    ptsLocation = "SS",
                    ptsCashier = "Wayne"
                };





                //var url = CreatePointAsync(points);
                bool complete = await CreatePointAsync(points);

                if (complete == true)
                {
    
                    this.txtCus.Text = points.ptsCustomerNo;// "7678976890222";
                    this.txtFname.Text = points.ptsFirstName; //"Test";
                    this.txtLname.Text = points.ptsLastName; // "Testers";
                    this.txtSales.Text = points.ptsTotal.ToString();
                    this.txtPoints.Text = points.ptsValue.ToString();
                    this.txtLoca.Text = points.ptsLocation;
                    this.txtCashier.Text = points.ptsCashier;

                    //Reponse from Web Api services
                    this.txtMPoints.Text = Tpoints.response.balance.p.ToString();
                    this.txtMValues.Text = Tpoints.response.balance.d.ToString();
                    this.txtMDiscount.Text = (Tpoints.response.balance.p * .10).ToString();

                    this.btnRedeem.Text = "Redeem Points";
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        //static async Task<Uri> CreatePointAsync(Point Points)
        static async Task<bool> CreatePointAsync(Point Points)
        {
            try
            {
                // for testing in Postman; Passed in as json parammeter
                //{"Id":1,"FirstName":"Test1","LastName":"User1","Username":"test1","Password":"test1","Token":""}

                //Thani Loacation values
                var userParam = new UserModel
                {
                    Id = 1,
                    FirstName = "Test1",
                    LastName = "User1",
                    Username = "test1",
                    Password = "test1",
                    Token = ""
                };

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:54574/");// https://localhost:44305/"); 
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    //StringContent content = new StringContent(JsonConvert.SerializeObject(userParam), Encoding.UTF8, "application/json");

                    // HTTP POST to get token
                    HttpResponseMessage response1 = await client.PostAsync("api/User/authenticate", clsWinGlobal.GetStringContent_UTF8(userParam)); // content); //.Result();
                    if (response1.IsSuccessStatusCode)
                    {
                        Token =await response1.Content.ReadAsAsync<UserModel>();
                    }
                }


                using (var _client = new HttpClient())
                {
                    //Call Thani's Web Api
                    _client.BaseAddress = new Uri("http://localhost:54574/");// https://localhost:44305/"); 
                    _client.DefaultRequestHeaders.Accept.Clear();
                    _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    if (!string.IsNullOrWhiteSpace(Token.Token))
                    {
                        //var t = JsonConvert.DeserializeObject<Token>(token);

                        _client.DefaultRequestHeaders.Clear();
                        _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Token.Token.ToString());// t.access_token);
   

                        HttpResponseMessage response = await _client.PostAsJsonAsync("api/points/DoPointsAsync", Points);
                        response.EnsureSuccessStatusCode();

                        if (response.IsSuccessStatusCode)
                        {
                            Tpoints = await response.Content.ReadAsAsync<MassyResponse>();

                            return true;
                        }
                        else
                        {
                            return false;
                        }
                        // return URI of the created resource.
                        //return response.Headers.Location;

                        //return response.IsSuccessStatusCode;
                        
                    }
                    else
                    {
                        return false;
                    }


                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
    }

  
    }


    public class POSSale
    {
        public int StoreID { get; set; }
        public int TransactionNumber { get; set; }
        public int BatchNumber { get; set; }
        public DateTime Time { get; set; } 
        public int CustomerID { get; set; } 
        public int CashierID { get; set; } 
        public double Total { get; set; }
        public double SalesTax { get; set; } 
        public string Comment { get; set; } 
        public string ReferenceNumber { get; set; } 
        public string CompName { get; set; } 
        public string Name { get; set; } 
        public string Cashier { get; set; } 
    }

   
    internal class Point
    {
        public int Points_id { get; set; }
        public int Document_id { get; set; }
        public string ptsCustomerNo { get; set; }//Card number
        public string ptsFirstName { get; set; } //Card holder First Name
        public string ptsLastName { get; set; } //Card holder Last Name
        public string ptsUnitType { get; set; } // (P or D) – P for points, D for dollars
        public string ptsMode { get; set; }
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
        public string ptsSecret { get; set; } // (string) The Secret for the selected store
    }

    //----------------------------------------------------------------
    // Massypoint response for display
    //{"response":{"invoice":"510162","points":6,"userid":"TERMINAL",
    // "balance":{"p":122,"d":"12.20"},
    //"footer":["Earnings Footer Text"],
    //"expiry":{"pts":107,"dat":"2018-10-31"}},"code":1,"HttpStatusCode":200}
    //------------------------------------------------------
    internal class MassyResponse
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

    //-----------------------------------------------
    public class UserModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
    }

}
