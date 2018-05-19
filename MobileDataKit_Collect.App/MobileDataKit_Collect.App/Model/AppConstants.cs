using System;
using System.Collections.Generic;
using System.Text;

namespace MobileDataKit_Collect.App.Model
{
   public partial class AppConstants
    {


        public static string UserName { get; set; }
      

        public static string CDNUrl = "https://kimambimangeapp.b-cdn.net/";

        public static string Host = "192.168.43.64";
#if DEBUG
        public static string Port = 49921.ToString();
#else
        public static string Port = 5000.ToString();
#endif
    }
}
