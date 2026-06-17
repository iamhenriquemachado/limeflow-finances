using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LimeFlow.Domain.Models
{
    internal class Transaction
    {
        public Guid Id { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Account { get; set; }
        public int Type { get; set; }
        public decimal Amount { get; set; }

    }
}
