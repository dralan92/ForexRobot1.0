using RestSharp;
using System;
using System.Collections.Generic;

namespace testapp
{
    public class OandaRestClient
    {
        public void PlaceOrder(List<string> tradeData)
        {
            string currencyPair = "XAU_JPY";//tradeData[0];
            string takeProfit = "185344";//tradeData[1];
            string stopLoss = "18500";//tradeData[2];
            string units = "1";

            var jsonBody = "{\"order\": {\r\n    \"units\": \"" +
                units +
                "\",\r\n    \"instrument\": \"" +
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
        public void PlaceLimitOrder(List<string> tradeData)
        {
            string currencyPair = "XAU_JPY";//tradeData[0];
            string limit = "185102";
            string takeProfit = "185344";//tradeData[1];
            string stopLoss = "18500";//tradeData[2];
            string units = "1";
            string expiry = DateTime.Now.AddHours(6.0).ToString();

            var jsonBody = "{\n  \"order\": {\n    \"units\": \"" +
                units +
                "\",\n    \"price\":\"" +
                limit +
                "\",\n    \"instrument\": \"" +
                currencyPair +
                "\",\n    \"timeInForce\": \"GTC\",\n    \"gtdTime\" :\"" +
                "2020-06-16 9:48:11 PM" +
                "\",\n    \"type\": \"LIMIT\",\n    \"takeProfitOnFill\": {\n      \"price\": \"" +
                takeProfit +
                "\"\n    },\n    \"stopLossOnFill\": {\n      \"price\": \"" +
                stopLoss +
                "\"\n    },\n    \"positionFill\": \"DEFAULT\"\n  }\n}";
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
