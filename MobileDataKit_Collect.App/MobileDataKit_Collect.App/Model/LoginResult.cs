using System;
using System.Collections.Generic;
using System.Text;

namespace MobileDataKit_Collect.App.Model
{
    public class LoginResult
    {
        public string error_description { get; set; }

        public string token_type { get; set; }

        public string access_token { get; set; }

        public double expires_in { get; set; }
    }
}
