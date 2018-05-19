using System;
using System.Collections.Generic;
using System.Text;

namespace MobileDataKit_Collect.App.Model
{
    public static class SecureHttpClient
    {

        private static System.Net.Http.HttpClient _HttpClient = null;

        public static System.Net.Http.HttpClient HttpClient
        {
            get
            {
                if (_HttpClient != null && !string.IsNullOrWhiteSpace(App.Token))
                    return _HttpClient;

                var _HttpClient2 = new System.Net.Http.HttpClient();

                if (!string.IsNullOrWhiteSpace(App.Token))
                    _HttpClient2.DefaultRequestHeaders.Add("Authorization", "Bearer " + App.Token);
                _HttpClient2.DefaultRequestHeaders.Add("IMEI", Plugin.DeviceInfo.CrossDeviceInfo.Current.Id);

             

                _HttpClient2.BaseAddress = new Uri(Model.AppConstants.Url);
                _HttpClient2.Timeout = TimeSpan.FromMinutes(5);
                if (!string.IsNullOrWhiteSpace(App.Token))
                    _HttpClient = _HttpClient2;
                else
                    return _HttpClient2;
                return _HttpClient;
            }
        }


        private static System.Net.Http.HttpClient _HttpClient3 = null;//new System.Net.Http.HttpClient();
        public static System.Net.Http.HttpClient MediaHttpClient
        {
            get
            {

                if (_HttpClient3 == null)
                {
                    _HttpClient3 = new System.Net.Http.HttpClient();
                    foreach (var t in HttpClient.DefaultRequestHeaders)
                        _HttpClient3.DefaultRequestHeaders.Add(t.Key, t.Value);

                    _HttpClient3.BaseAddress = HttpClient.BaseAddress;
                    _HttpClient3.Timeout = TimeSpan.FromMinutes(30);
                }



                return _HttpClient3;

            }
        }






    }
}
