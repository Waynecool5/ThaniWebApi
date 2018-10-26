using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThaniWebApi
{
    public class ClsGlobal
    {
        public const Int32 BUFFER_SIZE = 10; // Unmodifiable
        public static String FILE_NAME = "Output.txt"; // Modifiable
        public static readonly String CODE_PREFIX = "US-"; // Unmodifiable
        public static String Encoding_KB = "8080808080808080"; // Unmodifiable
        public static String Encoding_IV = "8080808080808080"; // Unmodifiable

        public static String SqlUser = "sa"; // Modifiable
        public static String SqlPassword = "dedan!0987o"; // Modifiable
        public static String SqlCatalog = "WebAsync";
        public static String SqlSource2 = "LAPTOPHP\\SQLEXP2017"; //office\\sql2017"; // "(LOCALDB)\\MSSQLLOCALDB";
       // public static String SqlSource2 = "OFFICE\\SQL2017"; //office\\sql2017"; // "(LOCALDB)\\MSSQLLOCALDB";
        public static String MassyAPIver134 = "http://beta.massycard.com/loyalty/massy/api/rest2/";
        //Install-Package System.Data.Common -Version 4.3.0
        //Install-Package System.Data.SqlClient -Version 4.5.1
        //Install-Package Newtonsoft.Json -Version 11.0.2
        //Install-Package EPPlus -Version 4.5.2.1
    }
}
