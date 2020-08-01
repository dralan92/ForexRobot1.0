using System;
using System.Collections.Generic;
using System.Text;

namespace testapp.Models
{
    class Price
    {
        public string CloseOutBid { get; set; }
        public string CloseOutAsk { get; set; }
        public UnitsAvailable UnitsAvailable { get; set; }
        public string Instrument { get; set; }
    }
}
