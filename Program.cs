using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using Telegram.Bot;

namespace testapp
{
    class Program
    {
        private static readonly TelegramBotClient bot = new TelegramBotClient("1208677836:AAHvCKbmeUZvrFPtg4VFs0DDge161osjcWc");

        private static HttpClient client = new HttpClient();
        private static OandaRestClient orc = new OandaRestClient();

        static void Main(string[] args)
        {


            //orc.PlaceLimitOrder(new List<string>());

            Console.WriteLine("Waiting...");
            bot.OnMessage += Bot_OnMessage;

            bot.StartReceiving();
            Console.ReadLine();
            bot.StopReceiving();
        }

        public static string FilterMessage(string message)
        {

            char[] arr = message.ToCharArray();
            arr = Array.FindAll<char>(arr, (c => (char.IsLetterOrDigit(c) || c == '.')));
            var filteredSting = new string(arr).ToUpper();
            return filteredSting;
        }
        public static List<string> ParseTradeMessage(string tradeMessage)
        {
            var filteredSting = FilterMessage(tradeMessage);
            var tradeList = new List<string>();

            var tp1Index = filteredSting.IndexOf("TP");
            var tp2Index = filteredSting.IndexOf("TP", tp1Index + 2);
            var tp3Index = filteredSting.IndexOf("TP", tp2Index + 2);
            var slIndex = filteredSting.IndexOf("SL", tp3Index + 2);
            var actionIndex = 0;
            if (BuyOrSell(filteredSting))
            {
                actionIndex = filteredSting.IndexOf("BUY");
            }
            actionIndex = filteredSting.IndexOf("SELL");

            var currencyPair = filteredSting.Substring(0, actionIndex);
            var formattedCurrencyPair = FormatCurrencyPair(currencyPair);

            var tp1 = filteredSting.Substring(tp1Index + 2, tp2Index - tp1Index - 2);
            var tp2 = filteredSting.Substring(tp2Index + 2, tp3Index - tp2Index - 2);
            var tp3 = filteredSting.Substring(tp3Index + 2, slIndex - tp3Index - 2);
            var sl = filteredSting.Substring(slIndex + 2, filteredSting.Length - slIndex - 3);
            tradeList.Add(formattedCurrencyPair);
            tradeList.Add(tp1);
            tradeList.Add(sl);
            return tradeList;

        }
        public static string FormatCurrencyPair(string currencyPair)
        {
            return currencyPair.Insert(currencyPair.Length - 3, "_");
        }
        public static bool BuyOrSell(string str)
        {
            int sellCount = Regex.Matches(str, "SELL").Count;
            if (sellCount >= 1)
            {
                return false;
            }
            return true;
        }

        public static bool IsTradeData(string input)
        {
            int tpCount = Regex.Matches(input, "TP").Count;
            int slCount = Regex.Matches(input, "SL").Count;
            int sellCount = Regex.Matches(input, "SELL").Count;
            int buyCount = Regex.Matches(input, "BUY").Count;
            if ((sellCount == 1 || buyCount == 1) && tpCount >= 1 && slCount >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        private static void Bot_OnMessage(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            if (e.Message.Type == Telegram.Bot.Types.Enums.MessageType.Text)
            {
                if (IsTradeData(FilterMessage(e.Message.Text)))
                {
                    Console.WriteLine(e.Message.Text);
                    //var td = ParseTradeMessage(e.Message.Text);
                    //Console.WriteLine("Trade Message"+td[0]+":"+td[1]+":"+td[2]);
                    //orc.PlaceOrder(td);
                    var msg = e.Message.Text;



                    var restClient = new RestClient("https://tradeapi20200720060309.azurewebsites.net/api/goldcup?message=" + msg);
                    var restRequest = new RestRequest();
                    restRequest.Method = RestSharp.Method.GET;
                    var response = restClient.Execute(restRequest);
                    HttpStatusCode statusCode = response.StatusCode;
                    int numericStatusCode = (int)statusCode;
                    Console.WriteLine(numericStatusCode);
                    if (numericStatusCode == 200)
                    {
                        Console.WriteLine("Message Posted");
                    }
                    else
                    {
                        Console.WriteLine("Message Posting FAILED!!!");

                    }

                }
                else
                {
                    Console.WriteLine("Not a trade message");
                }

            }
        }
    }
}
