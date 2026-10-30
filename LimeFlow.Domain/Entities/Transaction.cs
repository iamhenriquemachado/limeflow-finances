using LimeFlow.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LimeFlow.Domain.Models
{
    public class Transaction
    {
        public Guid Id { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Description { get; set; }
        public Guid AccountId { get; set; }
        public Guid CategoryId { get; set; }
        public TransactionType Type { get; set; }
        public decimal Amount { get; set; }

    }
}
