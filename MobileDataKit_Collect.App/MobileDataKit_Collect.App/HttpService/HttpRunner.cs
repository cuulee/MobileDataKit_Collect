using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;


namespace MobileDataKit_Collect.App.HttpService
{
    public class HttpRunner
    {



        public async Task RunHttp(StartHttpMessage startHttpMessage)
        {

            var message = new HttpMessage();


            var client2 = Model.SecureHttpClient.HttpClient;
            HttpRequestMessage httpRequestMessage = null;

            System.Net.Http.HttpResponseMessage response2 = null;




            var url = startHttpMessage.Url;
            if (startHttpMessage.HttpMessageMethod == HttpMessageMethod.GET || startHttpMessage.HttpMessageMethod == HttpMessageMethod.DELETE)
            {

                foreach (var r in startHttpMessage.Params)
                {
                    url = url + "/" + r.Value.ToString();
                }

            }

            if (startHttpMessage.HttpMessageMethod == HttpMessageMethod.GET)
                httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, url);
            if (startHttpMessage.HttpMessageMethod == HttpMessageMethod.POST)
                httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, url);
            if (startHttpMessage.HttpMessageMethod == HttpMessageMethod.DELETE)
                httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, url);
            if (startHttpMessage.HttpMessageMethod == HttpMessageMethod.PUT)
                httpRequestMessage = new HttpRequestMessage(HttpMethod.Put, url);

            
            foreach (var r in startHttpMessage.Headers)
            {
                if (r.Value != null)
                    httpRequestMessage.Headers.Add(r.Key, r.Value.ToString());
                else
                    httpRequestMessage.Headers.Add(r.Key, string.Empty);
            }
            if (startHttpMessage.HttpMessageMethod == HttpMessageMethod.GET || startHttpMessage.HttpMessageMethod == HttpMessageMethod.DELETE)
            {

                response2 = await client2.SendAsync(httpRequestMessage);
            }


            if (startHttpMessage.HttpMessageMethod == HttpMessageMethod.POST)
            {
                if(startHttpMessage.Data.GetType() ==typeof(string))
                {


                    httpRequestMessage.Content = new StringContent(startHttpMessage.Data.ToString());
                }
                else
                {
                    var e = Model.JsonConvert.SerializeObject(startHttpMessage.Data);

                    httpRequestMessage.Content = new StringContent(e, Encoding.UTF8, "application/json");
                }

               
                response2 = await client2.SendAsync(httpRequestMessage);
            }




            var JsonString = await response2.Content.ReadAsStringAsync();

            message.Data = Model.JsonConvert.DeserializeObject(JsonString);











            message.ID = startHttpMessage.ID;






            message.CustomSaveHandler = startHttpMessage.CustomSaveHandler;
            //{
            //    Message = i.ToString()
            //};



            Device.BeginInvokeOnMainThread(() => {
               
                    MessagingCenter.Send<HttpMessage>(message, "BackgroudEnded");

             


            });


        }
    }
}
