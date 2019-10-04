using System;
using System.Collections.Generic;
using System.Text;

namespace UpholdSdk.Models
{
    public enum AddressNetwork
    {
        Bitcoin,
        Ethereum,
        Litecoin
    }

    public class CardAddress : BaseUpholdEntity
    {
        public string Type { get; set; }
        public List<CardAddressFormat> Formats { get; set; }
    }

    public class CardAddressFormat
    {
        public string Format { get; set; }
        public string Value { get; set; }
    }

    // properties need to be lowercase for serialization
    public class NewCardAddress
    {
        public string network { get; set; }
    }

}
