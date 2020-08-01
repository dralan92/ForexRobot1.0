using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using testapp.Models;

namespace testapp
{
    class PriceService
    {
        public PriceList GetPriceList(string instrumentName)
        {
            var uri = "https://api-fxtrade.oanda.com/v3/accounts/001-002-4175284-001/pricing?instruments=" + instrumentName;
            var restClient = new RestClient(uri);
            var restRequest = new RestRequest();
            restRequest.Method = RestSharp.Method.GET;
            restRequest.AddHeader("Authorization", "Bearer c1a37f61db390caa27c3976317888313-0396b4d54ce9d3872bf14f4d159ea9fb");
            var response = restClient.Execute(restRequest);
            return JsonConvert.DeserializeObject<PriceList>(response.Content);


        }
    }
}
