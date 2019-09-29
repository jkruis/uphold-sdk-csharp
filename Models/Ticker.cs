using System;
using System.Collections.Generic;
using System.Text;

namespace UpholdSdk.Models
{
    public class Ticker : BaseUpholdEntity
    {
        public decimal Ask { get; set; }
        public decimal Bid { get; set; }
        public string Currency { get; set; }
        public string Pair { get; set; }
    }
}
