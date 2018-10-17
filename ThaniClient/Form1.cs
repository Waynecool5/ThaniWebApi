using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ThaniClient
{


    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
                               
           var points = new Point { Points_id = -1, Document_id = -1, ptsCustomerNo = "7678976890222", ptsUnitType = "P", ptsMode = "P", ptsTotal = 60, ptsValue = 600.00, ptsDiscount = 6.00, ptsLocation = "SS", ptsCashier = "Wayne" };

        }   
    }


    internal class Point
    {
        public int Points_id { get; set; }
        public int Document_id { get; set; }
        public string ptsCustomerNo { get; set; }
        public string ptsUnitType { get; set; }
        public string ptsMode { get; set; }
        public int ptsTotal { get; set; }
        public double ptsValue { get; set; }
        public double ptsDiscount { get; set; }
        public string ptsLocation { get; set; }
        public string ptsCashier { get; set; }
    }

}
