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


namespace ThaniClient
{


    public partial class Form1 : Form
    {
        static HttpClient _client = new HttpClient();
        //static ICollection<TotalPoints> Tpoints { get; set; }
        static MassyResponse Tpoints = null;

        public Form1()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            this.btnRedeem.Text = "Send Points";
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


        private async void AddSalesPoints()
        {
            //Call Thani's Web Api
            _client.BaseAddress = new Uri("http://localhost:54574/");
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                // Create a new product
                var points = new Point
                {
                    Points_id = -1,
                    Document_id = -1,
                    ptsCustomerNo = "7678976890222",
                    ptsFirstName = "Test",
                    ptsLastName = "Testers",
                    ptsUnitType = "P",
                    ptsMode = "P",
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
            
            HttpResponseMessage response = await _client.PostAsJsonAsync("api/points/DoPointsAsync", Points);
            response.EnsureSuccessStatusCode();

            if (response.IsSuccessStatusCode)
            {
                Tpoints = await response.Content.ReadAsAsync<MassyResponse>();
            }
            // return URI of the created resource.
            //return response.Headers.Location;

            return response.IsSuccessStatusCode;
        }

  
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

    //{"response":{"balance":{"p": "POINTS","d": "DOLLARS"},"expiry": {"pts": "POINTS","dat": "EXPIRYDATE"},"footer":["Footer Line 1 Text","Footer Line 2 Text"]}}
    internal class MassyResponse
    {
        public Response response { get; set; }
    }

    internal class Response
    {
        public Balance balance { get; set; }
        public Expiry expiry { get; set; }
        public string[] footer { get; set; }
    }

    internal class Balance
    {
        public double p { get; set; }
        public double d { get; set; }
    }

    internal class Expiry
    {
        public double pts { get; set; }
        public string dat { get; set; }
    }




}
