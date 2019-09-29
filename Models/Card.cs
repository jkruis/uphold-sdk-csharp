using System;
using System.Collections.Generic;
using System.Text;

namespace UpholdSdk.Models
{
    public class Card : BaseUpholdEntity
    {
        public string Id { get; set; }
        public string Label { get; set; }
        public string Currency { get; set; }

        public decimal Balance { get; set; }
        public decimal Available { get; set; }

        public Dictionary<string, string> Address { get; set; }
        public CardSettings Settings { get; set; }

        public DateTime? LastTransactionAt { get; set; }
    }

    public class CardSettings
    {
        public int Position { get; set; }
        public bool Protected { get; set; }
        public bool Starred { get; set; }
    }

    // properties need to be lowercase for serialization
    public class NewCard
    {
        public string label { get; set; }
        public string currency { get; set; }
    }
}
