using System;
using System.Collections.Generic;
using System.Text;

namespace MobileDataKit_Collect.App.HttpService
{
   public class RealmConfiguration
    {

        public string DatabasePath { get; set; }

        public string EncryptionKey { get; set; }


        public int SchemaVersion { get; set; }
    }
}
