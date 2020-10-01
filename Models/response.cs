using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace login_app.Models
{
    public class response
    {
        private static response _response;

        // Creating Object if not exists
        public static response GetInstance()
        {
            if (_response == null)
            {
                _response = new response();
            }
            return _response;
        }



        public int apiStatuscode { get; set; } = 500;
        public int appErrorcode { get; set; } = 0;
        public bool success { get; set; } = false;
        public string error { get; set; } = " Internal  Error";
        public String data { get; set; } = "";
        public dynamic request { get; set; } = "";

    }
}
