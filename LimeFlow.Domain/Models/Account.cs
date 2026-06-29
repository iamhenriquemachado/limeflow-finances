using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LimeFlow.Domain.Models
{
    public class Account
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Bank { get; private set; }
        public decimal Balance { get; private set; }
        public DateTime CreatedAt { get; init; }
        public DateTime UpdatedAt { get; private set; }
        public string UserId { get; private set; }

        public Account(string name, string bank, string userId)
        {
            var guid = Guid.NewGuid();

            Id = guid;
            Name = name;
            Bank = bank;
            Balance = 0.0m;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
            UserId = userId;

        }


        public void AddCredit(decimal amount)
        {
            if (amount <= 0) throw new ArgumentException("Value must be greater than zero.");
            this.Balance += amount;
        }

        public void Debit(decimal amount)
        {
            if (amount <= 0) throw new ArgumentException("Value must be greater than zero.");
            this.Balance -= amount;
        }

        public void UpdateName(string name)
        {
            Name = name;
        }

        public void UpdateBank(string bank)
        {
            Bank = bank;
        }


    }
}
