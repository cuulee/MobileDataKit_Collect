using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobileDataKit_Collect.App.Model
{
    public class JsonConvert
    {



        public static string SerializeObject(object obj)
        {
            var wsss = new Newtonsoft.Json.JsonSerializerSettings();
            wsss.TypeNameAssemblyFormatHandling = Newtonsoft.Json.TypeNameAssemblyFormatHandling.Simple;
            wsss.TypeNameHandling = Newtonsoft.Json.TypeNameHandling.All;
            wsss.ContractResolver = new Common.CustomContractResolver();
            wsss.Converters.Add(new RealmObjectConverter());
            wsss.Converters.Add(new DateTimeOffsetConverter());
            wsss.Converters.Add(new IntConverter());
            return Newtonsoft.Json.JsonConvert.SerializeObject(obj, wsss);//.Replace("VicobaMobile.Model", "VicobaApplication.Models");
        }
        public static object DeserializeObject(string jsonstring)
        {
            jsonstring = jsonstring.Replace("System.Private.CoreLib", "mscorlib");
            var wsss = new Newtonsoft.Json.JsonSerializerSettings();
            wsss.TypeNameAssemblyFormatHandling = Newtonsoft.Json.TypeNameAssemblyFormatHandling.Simple;
            wsss.TypeNameHandling = Newtonsoft.Json.TypeNameHandling.All;
            wsss.ContractResolver = new Common.CustomContractResolver();
            wsss.Converters.Add(new RealmObjectConverter());
            wsss.Converters.Add(new IntConverter());
            wsss.Converters.Add(new DateTimeOffsetConverter());
            //wsss.ObjectCreationHandling.

            return Newtonsoft.Json.JsonConvert.DeserializeObject(jsonstring, wsss);
        }



        public static T DeserializeObject<T>(string jsonstring)
        {
            jsonstring = jsonstring.Replace("System.Private.CoreLib", "mscorlib");
            var wsss = new Newtonsoft.Json.JsonSerializerSettings();
            wsss.TypeNameAssemblyFormatHandling = Newtonsoft.Json.TypeNameAssemblyFormatHandling.Simple;
            wsss.TypeNameHandling = Newtonsoft.Json.TypeNameHandling.All;
            wsss.ContractResolver = new Common.CustomContractResolver();
            wsss.Converters.Add(new RealmObjectConverter());
            wsss.Converters.Add(new IntConverter());
            wsss.Converters.Add(new DateTimeOffsetConverter());
            //wsss.ObjectCreationHandling.

            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(jsonstring, wsss);
        }
    }


    public class IntConverter : Newtonsoft.Json.JsonConverter
    {

        public IntConverter()
        {

        }
        public override bool CanConvert(Type objectType)
        {
            if (objectType == typeof(int) || objectType == typeof(int?))
            {
                return true;
            }
            return false;
        }

        public override bool CanWrite
        {
            get
            {
                return false;
            }
        }




        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.Value == null)
                return 0;

            var rr = int.Parse(reader.Value.ToString());




            return rr;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }



    public class DateTimeOffsetConverter : JsonConverter
    {

        public DateTimeOffsetConverter()
        {

        }
        public override bool CanConvert(Type objectType)
        {
            if (objectType == typeof(DateTimeOffset) || objectType == typeof(DateTimeOffset?))
            {
                return false;
            }
            return false;
        }
        public override bool CanRead
        {
            get
            {
                return true;
            }
        }
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {

            var rr = (long)reader.Value;



            return DateTimeOffset.FromUnixTimeMilliseconds(rr);





        }

        public override bool CanWrite
        {
            get
            {
                return true;
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {



            DateTimeOffset dfd = (DateTimeOffset)value;


            writer.WriteValue(dfd.ToUnixTimeMilliseconds());


        }
    }
}
