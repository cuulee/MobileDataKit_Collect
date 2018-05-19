using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace MobileDataKit_Collect.Common
{
    public class CustomContractResolver : DefaultContractResolver
    {

        protected override JsonContract CreateContract(Type objectType)
        {
            return base.CreateContract(objectType);
        }
        protected override JsonConverter ResolveContractConverter(Type objectType)
        {
            if (typeof(Realms.RealmObject).GetTypeInfo().IsAssignableFrom(objectType.GetTypeInfo()))
                return null;
            return base.ResolveContractConverter(objectType);
        }
    }
}
