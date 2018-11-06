using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThaniClient
{
    //---------------------------------------------
    //{"response":{"firstname":"UNEXPECTED","lastname":"CARD"},"code":1,"HttpStatusCode":200}

    public class MassyRespProfile
    {
        public RespNames response { get; set; }
        public int code { get; set; }
        public int HttpStatusCode { get; set; }
    }

    public class RespNames
    {
        public string firstname { get; set; }
        public string lastname { get; set; }
    }


}
