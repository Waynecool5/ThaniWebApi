using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ThaniClient
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }

    public class ClsGlobal
    {
        public static String SqlUser = "sa"; // Modifiable
        public static String SqlPassword = "aswat*!@#"; // Modifiable
        public static String SqlCatalog = "MSPOS";
        public static String SqlSource = @"DESKTOP-KTMFJJK\SQLEXPRESS";//"LAPTOPHP\\SQLEXP2017"; //office\\sql2017"; // "(LOCALDB)\\MSSQLLOCALDB";

    }
}
