using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace MobileDataKit_Collect.Common
{
    public class MyCustomConverter : JsonConverter
    {

        public MyCustomConverter()
        {
            ttttt.Add("BacklinksCount");
            ttttt.Add("IsManaged");
            ttttt.Add("IsValid");
            ttttt.Add("ObjectSchema");
            ttttt.Add("Realm");
        }



        protected virtual Realms.RealmObject CreateRealmObject(string primarykeyname, string primarykeyvalue, Type objecttype)
        {
            var ttt = (Realms.RealmObject)Activator.CreateInstance(objecttype);

            SafeSetter(ttt, primarykeyname, primarykeyvalue);


            return ttt;
        }
        public override bool CanConvert(Type objectType)
        {
            return typeof(Realms.RealmObject).GetTypeInfo().IsAssignableFrom(objectType.GetTypeInfo());
        }
        public override bool CanRead
        {
            get
            {
                return true;
            }
        }


        protected void SafeSetter(Object obj, string propertyName, object value)
        {
            var value2 = value;
            var prop = obj.GetType().GetProperty(propertyName);

            if (typeof(Int64) == prop.PropertyType)
                value2 = Int64.Parse(value2.ToString());


            Csla.Reflection.MethodCaller.CallPropertySetter(obj, propertyName, value2);

        }
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jsonObject = JObject.Load(reader);
            var primarykeyname = string.Empty;

            var properties = jsonObject.Properties().ToList();

            var obj = Activator.CreateInstance(objectType);

            if (typeof(Realms.RealmObject).IsAssignableFrom(objectType))
            {



                var primarykeyvalue = string.Empty;
                foreach (var i in properties)
                {
                    try
                    {
                        var prop_methoinfo = objectType.GetTypeInfo().GetDeclaredProperty(i.Name);

                        //if (Attribute.IsDefined(prop_methoinfo, typeof(Realms.PrimaryKeyAttribute), false))
                        //{
                        //    primarykeyname = i.Name;
                        //    primarykeyvalue = i.Value.ToString();
                        //}


                    }
                    catch
                    {

                    }

                }


                if (!string.IsNullOrWhiteSpace(primarykeyvalue))
                    obj = CreateRealmObject(primarykeyname, primarykeyvalue, objectType);

            }



            foreach (var i in properties)
            {
                try
                {
                    var prop = objectType.GetTypeInfo().GetDeclaredProperty(i.Name);
                    if (!ttttt.Contains(i.Name))
                    {

                        object val = null;
                        if (prop.PropertyType == typeof(DateTimeOffset) || prop.PropertyType == typeof(DateTimeOffset?))
                            val = serializer.Deserialize<DateTimeOffset>(i.Value.CreateReader());
                        else if (prop.PropertyType == typeof(int) || prop.PropertyType == typeof(int?))
                            val = serializer.Deserialize<int>(i.Value.CreateReader());
                        else
                        {
                            try
                            {
                                val = serializer.Deserialize(i.Value.CreateReader(), prop.PropertyType);
                            }
                            catch
                            {
                                val = serializer.Deserialize(i.Value.CreateReader());
                            }
                        }

                        if (prop.SetMethod == null && typeof(System.Collections.IEnumerable).IsAssignableFrom(prop.GetMethod.ReturnType) && typeof(System.Collections.IEnumerable).IsAssignableFrom(val.GetType()) && prop.GetMethod.ReturnType != typeof(string))
                        {
                            var rrrrr = Csla.Reflection.MethodCaller.CallPropertyGetter(obj, i.Name);
                            var y = (System.Collections.IEnumerable)val;
                            Csla.Reflection.MethodCaller.CallMethod(rrrrr, "Clear");
                            foreach (var y1 in y)
                            {
                                Csla.Reflection.MethodCaller.CallMethod(rrrrr, "Add", y1);


                            }
                        }



                        if (i.Name != primarykeyname)
                            SafeSetter(obj, i.Name, val);



                    }

                }
                catch (Exception eded)
                {
                    var sdsd = eded;

                }

            }

            return obj;

        }

        public override bool CanWrite
        {
            get
            {
                return true;
            }
        }
        private List<string> ttttt = new List<string>();
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {




            writer.WriteStartObject();

            writer.WritePropertyName("$type");
            var sdsd = value.GetType().AssemblyQualifiedName;

            var sddssss = sdsd.Substring(0, sdsd.IndexOfAny(",".ToArray()));

            var wswswss = sdsd.Substring(sdsd.IndexOfAny(",".ToArray()) + 1).Trim();


            var sddssss2 = wswswss.Substring(0, wswswss.IndexOfAny(",".ToArray()));


            writer.WriteValue(sddssss + ", " + sddssss2);

            //     "$type":"VicobaApplication.Models.RegisterUser, VicobaApplication.Models

            foreach (var r in value.GetType().GetTypeInfo().DeclaredProperties)
            {

                if (!ttttt.Contains(r.Name))
                {
                    try
                    {
                        var value1 = r.GetValue(value);
                        if (value1 != null)
                        {
                            writer.WritePropertyName(r.Name);
                            serializer.Serialize(writer, value1);
                        }
                    }
                    catch
                    {

                    }


                }

            }


            writer.WriteEndObject();


        }
    }
}
