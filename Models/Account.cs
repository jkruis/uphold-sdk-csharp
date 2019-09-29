using System;
using System.Collections.Generic;
using System.Text;

namespace UpholdSdk.Models
{
    public enum AccountType
    {
        Card,
        Sepa
    }

    public enum AccountStatus
    {
        Ok, 
        Failed
    }

    public class Account : BaseUpholdEntity
    {
        public string Id { get; set; }
        public AccountType Type { get; set; }
        public string Label { get; set; }
        public string Currency { get; set; }
        public AccountStatus Status { get; set; }

        public List<AccountBilling> Billing { get; set; }
    }

    public class AccountBilling
    {
        public string Name { get; set; }
    }

}
