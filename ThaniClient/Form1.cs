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

        private void btnRedeem_ClickAsync(object sender, EventArgs e)
        {

            if (this.btnRedeem.Text == "Save Points")
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
                    PtsCustomerNo = "7678976890222",
                    PtsFirstName = "Test",
                    PtsLastName = "Testers",
                    PtsUnitType = "P",
                    PtsMode = "P",
                    PtsTotal = 60,
                    PtsValue = 600.00,
                    PtsValueRate = .10,
                    PtsDiscount = 6.00,
                    PtsDiscountRate = .10,
                    PtsLocation = "SS",
                    PtsCashier = "Wayne"
                };


                //var url = CreatePointAsync(points);
                bool complete = await CreatePointAsync(points);

                if (complete == true)
                {
                    this.txtCus = Tpoints.PtsCustomerNo;
                    this.txtFname.Text = "";
                    this.txtLname.Text = "";
                    this.txtSales.Text = Tpoints.PtsTotal;
                    this.txtPoints.Text = Tpoints.PtsValue;
                    this.txtDiscount.Text = Tpoints.PtsDiscount;
                    this.txtLoca.Text = Tpoints.PtsLocation;
                    this.txtCashier.Text = Tpoints.PtsCashier;

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

  
    }


    internal class Point
    {
        public int Points_id { get; set; }
        public int Document_id { get; set; }
        public string PtsCustomerNo { get; set; }//Card number
        public string PtsFirstName { get; set; } //Card holder First Name
        public string PtsLastName { get; set; } //Card holder Last Name
        public string PtsUnitType { get; set; } // (P or D) – P for points, D for dollars
        public string PtsMode { get; set; }
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


}
