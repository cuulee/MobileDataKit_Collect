using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileDataKit_Collect.App.HttpService
{
    public class StartHttpMessage
    {

        public StartHttpMessage()
        {

        }

    
        public string  CustomSaveHandler { get; set; } = string.Empty;

        public string Url { get; set; }

        public string DataType { get; set; }
        public Guid ID { get; set; } = Guid.NewGuid();


        public RealmConfiguration RealmConfiguration { get; set; } = new RealmConfiguration();

        public HttpMessageMethod HttpMessageMethod { get; set; }

        public Dictionary<string, object> Params { get; set; } = new Dictionary<string, object>();
        public Dictionary<string, object> Headers { get; set; } = new Dictionary<string, object>();

        public object Data { get; set; }

    }
}
