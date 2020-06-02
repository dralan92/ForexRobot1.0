using RestSharp;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace testapp
{
    public class OandaRestClient
    {
       public void PlaceOrder(List<string> tradeData)
        {
            string currencyPair = tradeData[0];
            string takeProfit = tradeData[1];
            string stopLoss = tradeData[2];

            var jsonBody = "{\"order\": {\r\n    \"units\": \"1000\",\r\n    \"instrument\": \"" +
               currencyPair +
                "\",\r\n    \"timeInForce\": \"FOK\",\r\n    \"type\": \"MARKET\",\r\n    \"takeProfitOnFill\":{\"price\":\"" +
                takeProfit +
                "\"},\r\n    \"stopLossOnFill\" :{\"price\":\"" +
                stopLoss +
                "\"},\r\n    \"positionFill\": \"DEFAULT\"\r\n  }\r\n}";
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
