using System;
using System.Collections.Generic;
using System.Text;

namespace MobileDataKit_Collect.Common
{
    public class SafeRealmConfiguration : Realms.RealmConfiguration
    {
        public SafeRealmConfiguration(string path) : base(path)
        {

            SchemaVersion = 12;

        }
    }
}
