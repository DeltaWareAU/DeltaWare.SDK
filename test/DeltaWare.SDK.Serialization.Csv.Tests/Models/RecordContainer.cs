using DeltaWare.SDK.Serialization.Csv.Attributes;
using System;
using System.Collections.Generic;

namespace DeltaWare.SDK.Serialization.Csv.Tests.Models
{
    public class RecordContainer
    {
        public List<UserRecord> Users { get; set; } = new();

        public List<OrderRecord> Orders { get; set; } = new();

        public List<TransactionRecord> Transactions { get; set; } = new();

        public List<ProductRecord> Products { get; set; } = new();
    }

    [RecordType("users")]
    public class UserRecord
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime? BirthDate { get; set; }
    }

    [RecordType("orders")]
    public class OrderRecord
    {
        public Guid OrderId { get; set; }

        public DateTime Date { get; set; }

        public decimal Amount { get; set; }
    }

    [RecordType("transactions")]
    public class TransactionRecord
    {
        public string TransactionId { get; set; }

        public bool Paid { get; set; }
    }

    [RecordType("products")]
    public class ProductRecord
    {
        public string TransactionId { get; set; }

        public bool Paid { get; set; }
    }
}
