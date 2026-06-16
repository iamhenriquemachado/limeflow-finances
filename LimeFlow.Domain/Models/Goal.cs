using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LimeFlow.Domain.Models
{
    internal class Goal
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Target { get; set; }
        public decimal AlreadySaved { get; set; }
        public DateTime TargetDate { get; set; }
        public decimal MonthlyContribution { get; set; }
        public string Icon { get; set; }
    }
}
