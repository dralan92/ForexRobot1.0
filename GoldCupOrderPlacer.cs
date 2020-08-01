using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using testapp.Models;

namespace testapp
{
    class GoldCupOrderPlacer
    {
        public void PlaceGoldCupOrder(GoldCupTradeData goldCupTradeData)
        {
            var isLong = goldCupTradeData.Action == "BUY" ? true : false;
            var price = new PriceService().GetPriceList(goldCupTradeData.InstrumentName).Prices[0];
            var units = CalculateUnits(price);
            if (units >= 1)
            {
                if (!isLong) { units *= -1; }
                PlaceLimitOrder(goldCupTradeData.InstrumentName,
                                goldCupTradeData.TP,
                                goldCupTradeData.SL,
                                goldCupTradeData.Entry,
                                units);
            }
        }
        void PlaceLimitOrder(string instrument, double? tp, double? sl, double? entry, int units)
        {
            var jsonBody = "{\r\n  \"order\": {\r\n    \"units\": \"" +
                units +
                "\",\r\n    \"price\":\"" +
                entry +
                "\",\r\n    \"instrument\": \"" +
                instrument +
                "\",\r\n    \"timeInForce\": \"GTC\",\r\n    \"type\": \"LIMIT\",\r\n    \"takeProfitOnFill\": {\r\n      \"price\": \"" +
                tp +
                "\"\r\n    },\r\n    \"stopLossOnFill\": {\r\n      \"price\": \"" +
                sl +
                "\"\r\n    },\r\n    \"positionFill\": \"DEFAULT\"\r\n  }\r\n}";

            var restClient = new RestClient("https://api-fxtrade.oanda.com/v3/accounts/001-002-4175284-001/orders");
            var restRequest = new RestRequest();
            restRequest.Method = RestSharp.Method.POST;


            restRequest.AddHeader("Authorization", "Bearer c1a37f61db390caa27c3976317888313-0396b4d54ce9d3872bf14f4d159ea9fb");
            restRequest.AddParameter("application/json", jsonBody, ParameterType.RequestBody);
            var response = restClient.Execute(restRequest);
        }
        int CalculateUnits(Price price)
        {
            int availableUnits;
            var parseUnit = int.TryParse(price.UnitsAvailable.Default.Long, out availableUnits);
            var unitsForTrade = (int)Math.Floor(availableUnits * .02);
            if (unitsForTrade >= 1)
            {
                return unitsForTrade;
            }

            return -1;
        }
    }
}
