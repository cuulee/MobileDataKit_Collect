using Realms;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobileDataKit_Collect.App.Model
{
    public class RealmObjectConverter : Common.MyCustomConverter
    {




        protected override RealmObject CreateRealmObject(string primarykeyname, string primarykeyvalue, Type objecttype)
        {


            return (Realms.RealmObject)Activator.CreateInstance(objecttype);





        }

    }
}
