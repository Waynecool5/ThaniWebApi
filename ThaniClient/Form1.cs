﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using Insight.Database;
using Insight.Database.Reliable;
using Insight.Database.Providers;
using Newtonsoft.Json.Linq;
using System.Net;
using Newtonsoft.Json;
using System.IO;
using System.Security.Cryptography;

namespace ThaniClient
{


    public partial class Form1 : Form
    {

        static HttpClient _client = new HttpClient();
        static bool FTime = true;
        //static clsWinGlobal wcls;
        static clsSecurity wSec = new clsSecurity();

        //static ICollection<TotalPoints> Tpoints { get; set; }
        static MassyResponse Tpoints = null;
        static MassyRespProfile TProfile = null;
        static UserModel userParam = null;
        static ICollection<POSSale> PosSales { get; set; }
        static UserModel Token = null;
        static AppStore gsStore { get; set; }


        float storeDiscountRate = 0.10F;

        public static string conn = "";
        //private readonly string conn = "Data Source=" + ClsGlobal.SqlSource + "; Initial Catalog=" + ClsGlobal.SqlCatalog + "; Persist Security Info=True;" +
        //          "User ID=" + ClsGlobal.SqlUser + ";Password=" + ClsGlobal.SqlPassword + "";

        public Form1()
        {
            InitializeComponent();

            ////Call Thani's Web Api
            //_client.BaseAddress = new Uri(gsStore.WebThaniApiPath);// https://localhost:44305/"); 
            //_client.DefaultRequestHeaders.Accept.Clear();
            //_client.DefaultRequestHeaders.Accept.Add(
            //    new MediaTypeWithQualityHeaderValue("application/json"));
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                // Create application domain setup information
                var domaininfo = new AppDomainSetup();
                domaininfo.ConfigurationFile = System.Environment.CurrentDirectory +
                                               Path.DirectorySeparatorChar +
                                               "ADSetup.exe.config";
                domaininfo.ApplicationBase = System.Environment.CurrentDirectory;

                string xPath = domaininfo.ApplicationBase.ToString();
                string AppPath = xPath.Substring(0, xPath.IndexOf("\\bin"));

                JObject o1 = JObject.Parse(File.ReadAllText(@"" + AppPath + "\\store.json"));

                gsStore = JsonConvert.DeserializeObject<AppStore>(o1.ToString());


                //Create connection to sql
                conn = "Data Source=tcp:" + gsStore.SqlSource + "; Initial Catalog=" + gsStore.SqlCatalog + "; Persist Security Info=True;" +
                  "User ID=" + gsStore.SqlUser.ToString() + ";Password=" + gsStore.SqlPassword + "";


                //Creating the raw signature string
                string requestSignatureBase64String = "";
              
                //Calculate UNIX time
                DateTime epochStart = new DateTime(1970, 01, 01, 0, 0, 0, 0, DateTimeKind.Utc);
                TimeSpan timeSpan = DateTime.UtcNow - epochStart;
                string requestTimeStamp = Convert.ToUInt64(timeSpan.TotalSeconds).ToString();

                string signatureRawData = String.Format("{0}{1}", gsStore.APPId, requestTimeStamp); // {2}{3}{4}{5}", APPId, requestHttpMethod, requestUri, requestTimeStamp, nonce, requestContentBase64String);

                var secretKeyByteArray = Convert.FromBase64String(gsStore.APIKey);

                byte[] signature = Encoding.UTF8.GetBytes(signatureRawData);

                using (HMACSHA256 hmac = new HMACSHA256(secretKeyByteArray))
                {
                    byte[] signatureBytes = hmac.ComputeHash(signature);
                    requestSignatureBase64String = Convert.ToBase64String(signatureBytes);
                    //Setting the values in the Authorization header using custom scheme (amx)
                   // request.Headers.Authorization = new AuthenticationHeaderValue("amx", string.Format("{0}:{1}:{2}:{3}", APPId, requestSignatureBase64String, nonce, requestTimeStamp));
                }

                // response = await base.SendAsync(request, cancellationToken);


                //Thani Loacation values
                userParam = new UserModel
                {
                    Id = 1,
                    FirstName = "Test1",
                    LastName = "User1",
                    Username = "test1",
                    Password = "test1",
                    APPId = gsStore.APPId,
                    APIData = requestSignatureBase64String,
                    APITimeStamp = requestTimeStamp,
                    Token = ""
                };


                this.btnRedeem.Text = "Send Points";

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                //return null;
            }
            //oJson as Object = JsonConvert.DeserializeObject(File.ReadAllText(MyFilePath))
        }


        private async void Form1_ActivatedAsync(object sender, EventArgs e)
        {
            if (FTime == true)
            {
                HideButtons(true);

                //get Token Security Access to API
                Token = await wSec.GetSecurityToken(gsStore.WebThaniApiPath, userParam);


                ////get the transaction of Sale - LIVE
                //ICollection<POSSale> pos = await GetSale();
                ////Get Card number from selection - LIVE
                //string CardNo = (from o in pos select o.ReferenceNumber).ToString();
                //string Cashier = (from o in pos select o.CashierID).ToString();
                //this.lblTrans.Text = (from o in pos select o.TransactionNumber).ToString();
                //decimal Totalsales = Convert.ToDecimal(from o in pos select o.Total);
                //decimal TotalPoints = Totalsales * clsWinGlobal.gsMassyRate;
                //decimal PointValue = TotalPoints * clsWinGlobal.gsRate;

                //Testing
                string CardNo = "42100999892";
                this.lblLoca.Text = gsStore.LocID;
                this.txtLoca.Text = gsStore.LocationName;
                this.lblTrans.Text = "Test-09876";

                //GetSale1();

                //After card is swiped, get the Card holders Name from MassyAPI
                bool complete = await GetCustomerProfile(CardNo, userParam, "CustomerProfile");

                if (complete == true)
                {
                    this.txtCus.Text = CardNo;// "7678976890222";
                    this.txtFname.Text = TProfile.response.firstname; //"Test";
                    this.txtLname.Text = TProfile.response.lastname; // "Testers";
                    //this.txtCashier.Text = Cashier;
                    // this.txtLoca.Text = gsStore.LocationName;
                    //this.txtSales.Text = Totalsales;
                    //this.txtTPoints.Text = TotalPoints;
                    //this.txtPoints.Text = PointValue;
                    this.txtMDiscount.Text = "";
                    this.txtMPoints.Text = "";
                    this.txtMValues.Text = "";

                    //SELECT TOP(1)  [Transaction].StoreID, TransactionNumber, BatchNumber, Time,  [Transaction].CustomerID, 
                    //            CashierID, Total, SalesTax, Comment, ReferenceNumber, 


                }

                

                FTime = false;
            }

        }

        private void HideButtons(bool doChange)
        {
            //this.btnRedeem.Visible = doChange;

            if (!doChange)
            {
                this.btnRedeem.Text = "Redeem Points";
            }
            else
            {
                this.btnRedeem.Text = "Send Points";
            }


            this.btnVoid.Visible = !doChange;
            this.btnHistory.Visible = !doChange;
            this.btnRefund.Visible = !doChange;
            this.btnVerify.Visible = !doChange;
            this.btnBalance.Visible = !doChange;
        }


        #region Set rate options
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            clsWinGlobal.gsRate = clsWinGlobal.gsMassyRate;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            clsWinGlobal.gsRate = clsWinGlobal.gsBarpRate;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            clsWinGlobal.gsRate = clsWinGlobal.gsStaffRate;
        }

        #endregion


        #region Form events to call MassyApi

        private async void btnVoid_Click(object sender, EventArgs e)
        {
            //Propmpt for approval

            //use exsisting security token
            bool proceed = await CheckToken(Token);
            if (proceed == true)
            {
                DoVoidSalesPoints();

            }

        }

        private async void btnHistory_Click(object sender, EventArgs e)
        {
            bool proceed = await CheckToken(Token);
            if (proceed == true)
            {

            }
        }

        private async void btnVerify_Click(object sender, EventArgs e)
        {
            bool proceed = await CheckToken(Token);
            if (proceed == true)
            {


                this.panDisplay.Visible = true;
            }
        }

        private async void btnRefund_Click(object sender, EventArgs e)
        {
            bool proceed = await CheckToken(Token);
            if (proceed == true)
            {
                RefundSalesPoints();
            }
        }

        private async void btnBalance_Click(object sender, EventArgs e)
        {
            bool proceed = await CheckToken(Token);
            if (proceed == true)
            {

            }
        }




        private void btnRedeem_ClickAsync(object sender, EventArgs e)
        {

            if (this.btnRedeem.Text == "Send Points")
            {
                this.AddSalesPoints("earn");

            }
            else if (this.btnRedeem.Text == "Redeem Points")
            {
                this.RedeemSalesPoints();
            }
        }

        #endregion


        internal class Parm
        {

            public int StoreID { get; set; }
            //public int Transaction { get; set; }

        }

        private async Task<ICollection<POSSale>> GetSale()
        {
            using (var Sqlconn = new SqlConnection(conn))
            {
                await Sqlconn.OpenAsync();

                Parm parm = new Parm { StoreID = 1 };

                //Execute Storeprocedure for all Points
                try
                {
                    //SELECT TOP(1)  [Transaction].StoreID, TransactionNumber, BatchNumber, Time,  [Transaction].CustomerID, 
                    //            CashierID, Total, SalesTax, Comment, ReferenceNumber,   
                    //                                                                                              [Transaction].CompName, Store.Name, Cashier.FirstName Cashier
                    //FROM[Transaction]
                    //INNER JOIN Store on Store.ID = [Transaction].StoreID
                    //INNER JOIN Cashier on Cashier.ID = CashierID
                    //WHERE LEN(ReferenceNumber) > 0 and[Transaction].StoreID = @StoreID
                    //ORDER BY TransactionNumber DESC

                    PosSales = Sqlconn.Query<POSSale>("UBPOSGetLoyaltyTransactions", parm); //Parameters.Empty);//,

                    ////Return data and place into 2 objects that are link by IList<>
                    //PosSale = Sqlconn.Query("UBPOSGetLoyaltyTransactions", parm,
                    //                Query.Returns(Some<Comp>.Records)
                    //                 .ThenChildren(Some<CompList>.Records)); //, thrird object
                    //                                                         //id: Comp => Comp.ID,
                    //into: (Comp, CompList) => beer.Glasses = CompList);

                    return PosSales;
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                    return null;
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
                                lblTrans.Text = reader.GetValue(1).ToString();
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
                finally
                {
                    SQLconn.Close();
                }


            }


        }

        private void DoVoidSalesPoints()
        {
            try
            {
                this.panDisplay.Location = new System.Drawing.Point(100, 20);
                this.txtInvoice.Text = this.lblInvoice.Text;
                this.txtCardNo.Text = this.txtCus.Text;
                this.btnHide.Text = "Cancel Void";
                this.lblMode.Text = "void";
                this.lblEdit.Text = "Total Void";

                this.btnSubmit.Text = "Submit Void";

                this.txtBalance.Text = this.txtMPoints.Text;
                this.txtType.Text = this.txtMValues.Text;
                this.txtPts.Text = this.txtMValues.Text;

                this.panDisplay.Visible = true;
                this.txtPts.Focus();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void RedeemSalesPoints()
        {
            ////Default error message from Massy
            //string json = @"{""response"":{ ""invoice"":""000000"",""points"":""0"",""userid"":""TERMINAL"",
            //                         ""balance"":{""p"":""0"",""d"":""0.00""},""footer"":[""Earnings Footer Text""],""expiry"":{""pts"":""0"",""dat"":""1900-01-31""}},""code"":""FAIL"",""HttpStatusCode"":""900""}";

            try
            {
                this.panDisplay.Location = new System.Drawing.Point(100, 20);
                this.txtCardNo.Text = this.txtCus.Text;
                this.btnHide.Text = "Cancel Redeem";
                this.lblMode.Text = "redeem";
                this.lblEdit.Text = "Total Redeem";
                this.btnSubmit.Text = "Submit Redeem";

                this.txtBalance.Text = this.txtMPoints.Text;
                this.txtType.Text = this.txtMValues.Text;
                this.txtPts.Text = this.txtMValues.Text;



                this.panDisplay.Visible = true;
                this.txtPts.Focus();

            }
            catch (Exception)
            {

                throw;
            }
        }

        private void RefundSalesPoints()
        {
            ////Default error message from Massy
            //string json = @"{""response"":{ ""invoice"":""000000"",""points"":""0"",""userid"":""TERMINAL"",
            //                         ""balance"":{""p"":""0"",""d"":""0.00""},""footer"":[""Earnings Footer Text""],""expiry"":{""pts"":""0"",""dat"":""1900-01-31""}},""code"":""FAIL"",""HttpStatusCode"":""900""}";

            try
            {
                this.panDisplay.Location = new System.Drawing.Point(100, 20);
                this.btnHide.Text = "Cancel Refund";
                this.lblMode.Text = "refund";
                this.lblEdit.Text = "Total Refund";
                this.btnSubmit.Text = "Submit Refund";

                this.txtBalance.Text = this.txtMPoints.Text;
                this.txtType.Text = this.txtMValues.Text;
                this.txtPts.Text = this.txtMValues.Text;



                this.panDisplay.Visible = true;
                this.txtPts.Focus();

            }
            catch (Exception)
            {

                throw;
            }
        }

        private async void AddSalesPoints(string apiType)
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

                var points = new Point
                {
                    Points_id = -1,
                    Document_id = -1,
                    ptsCustomerNo = txtCus.Text,
                    ptsFirstName = txtFname.Text,
                    ptsLastName = txtLname.Text,
                    ptsUnitType = "D",
                    ptsMode = "D",
                    ptsTotal = Convert.ToDouble(txtTPoints.Text),
                    ptsValue = Convert.ToDouble(txtPoints.Text), // * 10,
                    ptsValueRate = Convert.ToDouble(clsWinGlobal.gsRate),
                    ptsDiscount = 0.00,
                    ptsDiscountRate = 0.10,
                    ptsLocation = lblLoca.Text,
                    ptsCashier = txtCashier.Text,
                    ptsInvoice = "",
                    ptsLimit = "",
                    ptsfcn = ""
                };

                //var points = new Point
                //{
                //    Points_id = -1,
                //    Document_id = -1,
                //    ptsCustomerNo = "42100999892",
                //    ptsFirstName = "Test",
                //    ptsLastName = "Testers",
                //    ptsUnitType = "D",
                //    ptsMode = "D",
                //    ptsTotal = 60,
                //    ptsValue = 600.00,
                //    ptsValueRate = .10,
                //    ptsDiscount = 6.00,
                //    ptsDiscountRate = .10,
                //    ptsLocation = gsStore.LocID, //"SS",
                //    ptsCashier = "Wayne",
                //    ptsInvoice = "",
                //    ptsLimit = "",
                //    ptsfcn = ""
                //};





                //var url = CreatePointAsync(points);
                bool complete = await CreatePointAsync(points, apiType);

                if (complete == true)
                {

                    this.txtCus.Text = points.ptsCustomerNo;// "7678976890222";
                    this.txtFname.Text = points.ptsFirstName; //"Test";
                    this.txtLname.Text = points.ptsLastName; // "Testers";
                    this.txtSales.Text = points.ptsTotal.ToString();
                    this.txtPoints.Text = points.ptsValue.ToString();
                    this.txtLoca.Text = gsStore.LocationName; //points.ptsLocation;
                    this.txtCashier.Text = points.ptsCashier;
                    this.lblInvoice.Text = Tpoints.response.invoice;
                    //Reponse from Web Api services
                    this.txtMPoints.Text = Tpoints.response.balance.p.ToString();
                    this.txtMValues.Text = Tpoints.response.balance.d.ToString();
                    this.txtMDiscount.Text = (Tpoints.response.balance.p * storeDiscountRate).ToString();

                    HideButtons(false);
                    //this.btnRedeem.Text = "Redeem Points";

                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        private async Task<bool> DoRedeem(string CardNo, UserModel userParam, string apiType, double Tpts)
        {//redeem Points

            //values only for getting CustomerProfile
            var points = new Point
            {
                Points_id = -1,
                Document_id = -1,
                ptsCustomerNo = "42100999892", //CardNo, // "42100999892", //required
                ptsFirstName = "",
                ptsLastName = "",
                ptsUnitType = "D",
                ptsMode = "D",
                ptsTotal = Tpts, //massy points units  //required
                ptsValue = 0.00,
                ptsValueRate = 0.00,
                ptsDiscount = 0.00,
                ptsDiscountRate = 0.00,
                ptsLocation = gsStore.LocID, //"SS", //required
                ptsCashier = "",
                ptsPin = 0,
                ptsSecret = "",
                ptsUnix = 0,
                ptsInvoice = "",
                ptsLimit = "",
                ptsfcn = ""
            };


            if (!string.IsNullOrWhiteSpace(Token.Token))
            {
                using (var _client = new HttpClient())
                {
                    //Call Thani's Web Api
                    _client.BaseAddress = new Uri(gsStore.WebThaniApiPath);// https://localhost:44305/"); 
                    _client.DefaultRequestHeaders.Accept.Clear();
                    _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    //var t = JsonConvert.DeserializeObject<Token>(token);

                    _client.DefaultRequestHeaders.Clear();
                    _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Token.Token.ToString());// t.access_token);

                    //redeem Points
                    HttpResponseMessage response = await _client.PostAsJsonAsync("api/points/GetRedeemProfile?apiType=" + apiType.ToString(), points);

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
            }
            else
            {
                return false;
            }
        }

        private async Task<bool> DoRefund(string CardNo, UserModel userParam, string apiType, double Tpts)
        {//redeem Points

            //values only for getting CustomerProfile
            var points = new Point
            {
                Points_id = -1,
                Document_id = -1,
                ptsCustomerNo = "42100999892", //CardNo, // "42100999892",//required
                ptsFirstName = "",
                ptsLastName = "",
                ptsUnitType = "D",
                ptsMode = "D",
                ptsTotal = Tpts, //massy points units //required
                ptsValue = 0.00,
                ptsValueRate = 0.00,
                ptsDiscount = 0.00,
                ptsDiscountRate = 0.00,
                ptsLocation = gsStore.LocID, //"SS", //required
                ptsCashier = "",
                ptsPin = 0,
                ptsSecret = "",
                ptsUnix = 0,
                ptsInvoice = "",
                ptsLimit = "",
                ptsfcn = ""
            };


            if (!string.IsNullOrWhiteSpace(Token.Token))
            {
                using (var _client = new HttpClient())
                {
                    //Call Thani's Web Api
                    _client.BaseAddress = new Uri(gsStore.WebThaniApiPath);// https://localhost:44305/"); 
                    _client.DefaultRequestHeaders.Accept.Clear();
                    _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    //var t = JsonConvert.DeserializeObject<Token>(token);

                    _client.DefaultRequestHeaders.Clear();
                    _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Token.Token.ToString());// t.access_token);

                    //redeem Points
                    HttpResponseMessage response = await _client.PostAsJsonAsync("api/points/GetRefundProfile?apiType=" + apiType.ToString(), points);

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
            }
            else
            {
                return false;
            }
        }

        private async Task<bool> DoVoid(string InvoiceNo, UserModel userParam, string apiType, double Tpts)
        {//redeem Points

            //values only for getting CustomerProfile
            var points = new Point
            {
                Points_id = -1,
                Document_id = -1,
                ptsCustomerNo = "", //CardNo, // "42100999892",
                ptsFirstName = "",
                ptsLastName = "",
                ptsUnitType = "D",
                ptsMode = "D",
                ptsTotal = Tpts, //massy points units
                ptsValue = 0.00,
                ptsValueRate = 0.00,
                ptsDiscount = 0.00,
                ptsDiscountRate = 0.00,
                ptsLocation = gsStore.LocID, //"SS", required
                ptsCashier = "",
                ptsPin = 0,
                ptsSecret = "",
                ptsUnix = 0,
                ptsInvoice = InvoiceNo, //required
                ptsLimit = "",
                ptsfcn = ""
            };


            if (!string.IsNullOrWhiteSpace(Token.Token))
            {
                using (var _client = new HttpClient())
                {
                    //Call Thani's Web Api
                    _client.BaseAddress = new Uri(gsStore.WebThaniApiPath);// https://localhost:44305/"); 
                    _client.DefaultRequestHeaders.Accept.Clear();
                    _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    //var t = JsonConvert.DeserializeObject<Token>(token);

                    _client.DefaultRequestHeaders.Clear();
                    _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Token.Token.ToString());// t.access_token);

                    //redeem Points
                    HttpResponseMessage response = await _client.PostAsJsonAsync("api/points/GetVoidProfile?apiType=" + apiType.ToString(), points);

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
            }
            else
            {
                return false;
            }
        }

        private async Task<bool> GetCustomerProfile(string CardNo, UserModel userParam, string apiType)
        {

            //values only for getting CustomerProfile
            var points = new Point
            {
                Points_id = -1,
                Document_id = -1,
                ptsCustomerNo = "42100999892",
                ptsFirstName = "",
                ptsLastName = "",
                ptsUnitType = "D",
                ptsMode = "D",
                ptsTotal = 0,
                ptsValue = 0.00,
                ptsValueRate = 0.00,
                ptsDiscount = 0.00,
                ptsDiscountRate = 0.00,
                ptsLocation = gsStore.LocID, //"SS",
                ptsCashier = "",
                ptsPin = 0,
                ptsSecret = "",
                ptsUnix = 0,
                ptsInvoice = "",
                ptsLimit = "",
                ptsfcn = ""
            };


            if (!string.IsNullOrWhiteSpace(Token.Token))
            {
                using (var _client = new HttpClient())
                {
                    //Call Thani's Web Api
                    _client.BaseAddress = new Uri(gsStore.WebThaniApiPath);// https://localhost:44305/"); 
                    _client.DefaultRequestHeaders.Accept.Clear();
                    _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    //var t = JsonConvert.DeserializeObject<Token>(token);

                    _client.DefaultRequestHeaders.Clear();
                    _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Token.Token.ToString());// t.access_token);

                    HttpResponseMessage response = await _client.PostAsJsonAsync("api/points/GetCustProfile?apiType=" + apiType.ToString(), points);

                    response.EnsureSuccessStatusCode();

                    if (response.IsSuccessStatusCode)
                    {
                        TProfile = await response.Content.ReadAsAsync<MassyRespProfile>();

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
            }
            else
            {
                return false;
            }
        }

        static async Task<bool> CreatePointAsync(Point Points, string apiType)
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

                //using (var client = new HttpClient())
                //{
                //    client.BaseAddress = new Uri(gsStore.WebThaniApiPath);// https://localhost:44305/"); 
                //    client.DefaultRequestHeaders.Accept.Clear();
                //    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //    //StringContent content = new StringContent(JsonConvert.SerializeObject(userParam), Encoding.UTF8, "application/json");

                //    // HTTP POST to get token
                //    HttpResponseMessage response1 = await client.PostAsync("api/User/authenticate", clsWinGlobal.GetStringContent_UTF8(userParam)); // content); //.Result();
                //    if (response1.IsSuccessStatusCode)
                //    {
                //        Token = await response1.Content.ReadAsAsync<UserModel>();
                //    }
                //}

                //get new token for access to webapi's
                UserModel Token = await wSec.GetSecurityToken("http://localhost:54574/", userParam);

                if (!string.IsNullOrWhiteSpace(Token.Token))
                {
                    using (var _client = new HttpClient())
                    {
                        //Call Thani's Web Api
                        _client.BaseAddress = new Uri(gsStore.WebThaniApiPath);// https://localhost:44305/"); 
                        _client.DefaultRequestHeaders.Accept.Clear();
                        _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                        //var t = JsonConvert.DeserializeObject<Token>(token);

                        _client.DefaultRequestHeaders.Clear();
                        _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Token.Token.ToString());// t.access_token);

                        HttpResponseMessage response = await _client.PostAsJsonAsync("api/points/DoPointsAsync?apiType=" + apiType.ToString(), Points);

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
                }
                else
                {
                    Console.WriteLine("Secure access not granted.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        private async Task<bool> CheckToken(UserModel Token)
        {
            if (!string.IsNullOrWhiteSpace(Token.Token))
            {
                return true;
            }
            else
            {
                //get Token Security Access to API
                Token = await wSec.GetSecurityToken(gsStore.WebThaniApiPath, userParam);

                return true;
            }
        }



        private void btnHide_Click(object sender, EventArgs e)
        {
            this.txtMPoints.Text = "";
            this.txtMPoints.Text = this.txtBalance.Text;
            this.txtMValues.Text = "";
            this.txtMValues.Text = this.txtType.Text;
            
            this.panDisplay.Visible = false;
            this.panDisplay.Location = new System.Drawing.Point(690, 20);
        }

        private async void btnSubmit_ClickAsync(object sender, EventArgs e)
        {
            try
            {

                bool complete = false;
                string Mode = this.lblMode.Text;
                string CardNo = this.txtCardNo.Text;
                string InvoiceNo = this.txtInvoice.Text;
                double Tpts = Convert.ToDouble(this.txtPts.Text);

                switch (Mode)
                {
                    case "redeem":
                        //redeem?card=CARD&units=UNITVALUE&unitType=UNITTYPE&mlid=LOCATIONID&ts=UNIXTIMESTAMP&pin=PIN&qsa=GENERATEDHASH
                         complete = await DoRedeem(CardNo, userParam, Mode, Tpts);

                        if (complete == true)
                        {
                            this.txtInvoice.Text = "";
                            this.txtInvoice.Text = Convert.ToString(Tpoints.response.invoice);
                            this.txtBalance.Text = "";
                            this.txtBalance.Text = Convert.ToString(Tpoints.response.balance.p); //"Test";
                            this.txtType.Text = "";
                            this.txtType.Text = Tpoints.response.balance.d; // "Testers";
                            this.txtExpired.Text = "";
                            this.txtExpired.Text = Convert.ToString(Tpoints.response.expiry.pts); // "Testers";

                            //Show new balance
                            HideButtons(true);
                        }
                        return;
                    case "refund":
                        //redeem?card=CARD&units=UNITVALUE&unitType=UNITTYPE&mlid=LOCATIONID&ts=UNIXTIMESTAMP&pin=PIN&qsa=GENERATEDHASH
                         complete = await DoRefund(CardNo, userParam, Mode, Tpts);

                        if (complete == true)
                        {
                            this.txtInvoice.Text = "";
                            this.txtInvoice.Text = Convert.ToString(Tpoints.response.invoice);
                            this.txtBalance.Text = "";
                            this.txtBalance.Text = Convert.ToString(Tpoints.response.balance.p); //"Test";
                            this.txtType.Text = "";
                            this.txtType.Text = Tpoints.response.balance.d; // "Testers";
                            this.txtExpired.Text = "";
                            this.txtExpired.Text = Convert.ToString(Tpoints.response.expiry.pts); // "Testers";

                            //Show new balance
                            HideButtons(true);
                        }
                        return;
                    case "void":
                        //redeem?card=CARD&units=UNITVALUE&unitType=UNITTYPE&mlid=LOCATIONID&ts=UNIXTIMESTAMP&pin=PIN&qsa=GENERATEDHASH
                        complete = await DoVoid(InvoiceNo, userParam, Mode, Tpts);

                        if (complete == true)
                        {
                            this.txtInvoice.Text = "";
                            this.txtInvoice.Text = Convert.ToString(Tpoints.response.invoice);
                            this.txtBalance.Text = "";
                            this.txtBalance.Text = Convert.ToString(Tpoints.response.balance.p); //"Test";
                            this.txtType.Text = "";
                            this.txtType.Text = Tpoints.response.balance.d; // "Testers";
                            this.txtExpired.Text = "";
                            this.txtExpired.Text = Convert.ToString(Tpoints.response.expiry.pts); // "Testers";

                            //Show new balance
                            HideButtons(true);
                        }
                        return;
                    //case "balance":
                    //    //balance?card=CARD&mlid=LOCATIONID&ts=UNIXTIMESTAMP&qsa=GENERATEDHASH
                    //    return await MassyController.InsertMassyApiPoints(mPts, "balance");
                    default:
                        return;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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
            public string ReferenceNumber { get; set; } // scan Card Number
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
            public string ptsInvoice { get; set; } // (string) The invoice # of process
            public string ptsLimit { get; set; } // (string) The limit to retreive data
            public string ptsfcn { get; set; }
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
            public string APPId { get; set; }
            public string APIData { get; set; }
            //public string APIKey { get; set; }
            public string APITimeStamp { get; set; }
            public string Token { get; set; }
        }


        //----------------------------------
        public class apiCall
        {
            public string apiType { get; set; }
        }

        public class AppStore
        {
            public string LocID { get; set; }
            public string LocationName { get; set; }
            public string SqlSource { get; set; }
            public string SqlCatalog { get; set; }
            public string SqlUser { get; set; }
            public string SqlPassword { get; set; }
            public string WebThaniApiPath { get; set; }
            public string APIKey { get; set; }
            public string APPId { get; set; }
        }

}

