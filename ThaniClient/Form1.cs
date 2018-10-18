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
        static dynamic Tpoints = null;

        public Form1()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            this.btnRedeem.Text = "Save Points";
        }

        private async void button1_ClickAsync(object sender, EventArgs e)
        {

            if (this.btnRedeem.Text == "Save Points")
            {
                //Call Thani's Web Api
                _client.BaseAddress = new Uri("http://localhost:54574/");
                _client.DefaultRequestHeaders.Accept.Clear();
                _client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

              try
                {
                    // Create a new product
                    var points = new Point {
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
                        ptsCashier = "Wayne" };


                    //var url = CreatePointAsync(points);
                    bool complete = await CreatePointAsync(points);

                    if (complete == true)
                    {
                        this.txtCus = Tpoints.ptsCustomerNo;
                        this.txtFname.Text = "";
                        this.txtLname.Text = "";
                        this.txtSales.Text = Tpoints.ptsTotal;
                        this.txtPoints.Text = Tpoints.ptsValue;
                        this.txtDiscount.Text = Tpoints.ptsDiscount;
                        this.txtLoca.Text = Tpoints.ptsLocation;
                        this.txtCashier.Text = Tpoints.ptsCashier;

                        this.btnRedeem.Text = "Redeem Points";
                    }


                 }
                 catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
               }
            else
                {

                }
        }

        //static async Task<Uri> CreatePointAsync(Point Points)
        static async Task<bool> CreatePointAsync(Point Points)
        {
            
            HttpResponseMessage response = await _client.PostAsJsonAsync("api/points/InsertPointsAsync", Points);
            response.EnsureSuccessStatusCode();

            if (response.IsSuccessStatusCode)
            {
                Tpoints = await response.Content.ReadAsAsync<dynamic>();
            }
            // return URI of the created resource.
            //return response.Headers.Location;

            return response.IsSuccessStatusCode;
        }

        private void optType_CheckedChanged(object sender, EventArgs e)
        {

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
    }


}
