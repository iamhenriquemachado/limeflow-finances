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

        public Account(string name, string bank)
        {
            this.Id = Guid.NewGuid();
            this.Name = string.IsNullOrWhiteSpace(name) ? throw new ArgumentNullException("Name cannot be null") : name;
            this.Bank = bank;
            this.Balance = 0;
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

        
    }
}
