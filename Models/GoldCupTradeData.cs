using System;
using System.Collections.Generic;
using System.Text;

namespace testapp.Models
{
    class GoldCupTradeData
    {
        public string InstrumentName { get; set; }
        public string Action { get; set; }
        public double? Entry { get; set; }
        public double? SL { get; set; }
        public double? TP { get; set; }
    }
}
