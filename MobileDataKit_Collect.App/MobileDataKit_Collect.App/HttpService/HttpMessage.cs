using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileDataKit_Collect.App.HttpService
{
    public class HttpMessage
    {




        public string CustomSaveHandler { get; set; }
        public Guid ID { get; set; }
        public object Data { get; set; } = new List<object>();
    }
}
