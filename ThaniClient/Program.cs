using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ThaniClient
{
    static class Program
    {

        //public static AppStore gsStore { get; set; }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //// Create application domain setup information
            //var domaininfo = new AppDomainSetup();
            //domaininfo.ConfigurationFile = System.Environment.CurrentDirectory +
            //                               Path.DirectorySeparatorChar +
            //                               "ADSetup.exe.config";
            //domaininfo.ApplicationBase = System.Environment.CurrentDirectory;

            //string xPath = domaininfo.ApplicationBase.ToString();
            //string AppPath = xPath.Substring(0, xPath.IndexOf("\\bin"));

            //JObject o1 = JObject.Parse(File.ReadAllText(@"" + AppPath + "\\store.json"));

            //gsStore = JsonConvert.DeserializeObject<AppStore>(o1.ToString());



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

    //public class AppStore
    //{
    //    public static String LocID { get; set; }
    //    public static String LocationName { get; set; }
    //    public static String SqlSource { get; set; }
    //    public static String SqlCatalog { get; set; }
    //    public static String SqlUser { get; set; }
    //    public static String SqlPassword { get; set; }
    //    public static String WebThaniApiPath { get; set; }
    //}
}
