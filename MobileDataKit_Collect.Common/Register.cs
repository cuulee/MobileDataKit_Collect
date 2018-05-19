using System;
using System.Collections.Generic;
using System.Text;

namespace MobileDataKit_Collect.Common
{
    public class Register : Realms.RealmObject
    {

        [Realms.PrimaryKey]
        public string UserName { get; set; }


        public string ConfirmPassword { get; set; }

        public string Password { get; set; }


        public string PrimarySchool { get; set; }


        public string IMEI { get; set; }



        public string Description { get; set; }
    }


    public class CurrentTheme : Realms.RealmObject
    {
        public string ThemeName { get; set; }
    }
}
