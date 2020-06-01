using RestSharp;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace testapp
{
    public class OandaRestClient
    {
       public void PlaceOrder()
        {
            var jsonBody = "{\"order\": {\r\n    \"units\": \"1000\",\r\n    \"instrument\": \"AUD_JPY\",\r\n    \"timeInForce\": \"GTC\",\r\n    \"type\": \"LIMIT\",\r\n    \"price\":\"72.25\",\r\n    \"takeProfitOnFill\":{\"price\":\"72.50\"},\r\n    \"stopLossOnFill\" :{\"price\":\"72.10\"},\r\n    \"positionFill\": \"DEFAULT\"\r\n  }\r\n}";
            var restClient = new RestClient("https://api-fxpractice.oanda.com/v3/accounts/101-002-14835452-002/orders");
            var restRequest = new RestRequest();
            restRequest.Method = RestSharp.Method.POST;


            restRequest.AddHeader("Authorization", "Bearer 551cb19f4a836f950717a0202b8dde87-adad3a86ed84c81861f9b947c435220c");
            restRequest.AddParameter("application/json", jsonBody, ParameterType.RequestBody);
            var response = restClient.Execute(restRequest);
            Console.WriteLine(response.StatusCode);

        }
    }
}
