using System;
using System.Collections.Generic;
using System.Text;

namespace MobileDataKit_Collect.Common
{
   public class FormElement :  Realms.RealmObject
    {

        
        public string Type { get; set; }

        public  string Name { get; set; }


        public string Label { get; set; }

        public string Relevant { get; set; }


        public IList<FormElement> Fields { get; }
    }
}
