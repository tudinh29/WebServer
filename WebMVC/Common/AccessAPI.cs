using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace WebMVC.Common
{
    public class AccessAPI
    {
        public  HttpClient Access()
        {

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:21212/");

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }
    }
}