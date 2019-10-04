using System;
using System.Collections.Generic;
using System.Text;

namespace UpholdSdk.Models
{
    public enum TransactionType
    {
        Deposit,
        Transfer,
        Withdrawal
    }

    public enum TransactionStatus
    {
        Pending,
        Waiting,
        Cancelled,
        Completed
    }

    public enum TransactionPriority
    {
        Normal,
        Fast
    }

    public enum FeeTarget
    {
        Origin,
        Destination
    }

    public enum FeeType
    {
        Deposit,
        Exchange,
        Network,
        Withdrawal
    }

    public class Transaction : BaseUpholdEntity
    {
        public string Id { get; set; }
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; }

        public TransactionOrigin Origin { get; set; }
        public TransactionDestination Destination { get; set; }

        public TransactionDenomination Denomination { get; set; }
        public List<TransactionFee> Fees { get; set; }

        public TransactionStatus Status { get; set; }
        public TransactionType Type { get; set; }
        public TransactionPriority Priority { get; set; }
    }

    public class TransactionOrigin
    {
        public string CardId { get; set; }
        public string Currency { get; set; }

        // The name of the sender.
        public string Description { get; set; }

        // The amount debited, including commissions and fees.
        public decimal Amount { get; set; }

        // The amount to debit, before commissions or fees.
        public decimal Base { get; set; }

        // The commission charged by Uphold to process the transaction.
        public decimal Commission { get; set; }

        // The Bitcoin network Fee, if origin is in BTC but destination is not, or is a non-Uphold Bitcoin Address.
        public decimal Fee { get; set; }
    }

    public class TransactionDestination
    {
        public string CardId { get; set; }
        public string Currency { get; set; }

        // The name of the recipient. In the case where money is sent via email, the description will contain the email address of the recipient.
        public string Description { get; set; }

        // The amount credited, including commissions and fees.
        public decimal Amount { get; set; }

        // The amount to credit, before commissions or fees.
        public decimal Base { get; set; }

        // The commission charged by Uphold to process the transaction. Commissions are only charged when currency is converted into a different denomination.
        public decimal Commission { get; set; }

        // The Bitcoin network Fee, if destination is a BTC address but origin is not.
        public decimal Fee { get; set; }
    }

    public class TransactionDenomination
    {
        public string Currency { get; set; }
        public decimal Amount { get; set; }
        public string Pair { get; set; }
        public decimal Rate { get; set; }
    }

    public class TransactionFee
    {
        public string Currency { get; set; }
        public decimal Amount { get; set; }
        public decimal Percentage { get; set; }
        public FeeTarget Target { get; set; }
        public FeeType Type { get; set; }
    }

    // properties need to be lowercase for serialization
    public class NewTransaction
    {
        public NewTransactionDenomination denomination { get; set; }
        public string destination { get; set; }
    }

    public class NewTransactionDenomination
    {
        public string currency { get; set; }
        public decimal amount { get; set; }
    }

    public class TransactionCommit
    {
        public string Message { get; set; }
    }

}
