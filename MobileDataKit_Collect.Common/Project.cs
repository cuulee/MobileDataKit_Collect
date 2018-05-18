using System;
using System.Collections.Generic;
using System.Text;

namespace MobileDataKit_Collect.Common
{
  public  class Project : Realms.RealmObject
    {

       public string ProjectName { get; set; }

        public IList<FormElement> FormElements { get; }
    }
}
