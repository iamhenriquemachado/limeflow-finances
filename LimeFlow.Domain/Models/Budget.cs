using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LimeFlow.Domain.Models
{
    public class Budget
    {
        public Guid Id { get; set; }
        public int CategoryId { get; set; }
        public decimal LimitAmount { get; set; }
        public int AlertTreshold { get; set; }
        public int Period { get; set; }
        public string Notes { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
