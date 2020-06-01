using System;
using TradingAPI.MT4Server;
using Telegram.Bot;
using System.Net.Http;
using System.Text.RegularExpressions;

namespace testapp
{
    class Program
    {
        private static readonly TelegramBotClient bot = new TelegramBotClient("1208677836:AAHvCKbmeUZvrFPtg4VFs0DDge161osjcWc");
        private static HttpClient client = new HttpClient();
        static void Main(string[] args)
        {

            string message = "*XAUSUJ-SELL*1725.45 TP 1723.89 TP 1723.89 TP 1723.89 SL 1789.09";
            char[] arr = message.ToCharArray();
            arr = Array.FindAll<char>(arr, (c => (char.IsLetterOrDigit(c)||c=='.')));
            var filteredSting = new string(arr).ToUpper();

            

            if (IsTradeData(filteredSting))
            {
                var tp1Index = filteredSting.IndexOf("TP");
                var tp2Index = filteredSting.IndexOf("TP", tp1Index+2);
                var tp3Index = filteredSting.IndexOf("TP", tp2Index+ 2);
                var slIndex = filteredSting.IndexOf("SL", tp3Index+ 2);
                var actionIndex = 0;
                if (BuyOrSell(filteredSting))
                {
                    actionIndex = filteredSting.IndexOf("BUY");
                }
                actionIndex = filteredSting.IndexOf("SELL");

                var currencyPair = filteredSting.Substring(0, actionIndex);

                var tp1 = filteredSting.Substring(tp1Index+2, tp2Index- tp1Index-2);
                var tp2 = filteredSting.Substring(tp2Index+2, tp3Index- tp2Index-2);
                var tp3 = filteredSting.Substring(tp3Index+2, slIndex- tp3Index-2);
                var sl = filteredSting.Substring(slIndex+ 2, filteredSting.Length-slIndex-3);


            }

            OandaRestClient orc = new OandaRestClient();
            orc.PlaceOrder();
            //Console.WriteLine("Waiting...");
            //bot.OnMessage += Bot_OnMessage;

            //bot.StartReceiving();
            Console.ReadLine();
            //bot.StopReceiving();
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
            if((sellCount==1|| buyCount==1)&& tpCount >=1 && slCount>=1)
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
            if(e.Message.Type == Telegram.Bot.Types.Enums.MessageType.Text)
            {
                var result = client.GetAsync("https://localhost:5001/weatherforecast?message="+ e.Message.Text).Result;
                Console.WriteLine(e.Message.Text+ "-->"+ result.StatusCode);
            }
        }
    }
}
