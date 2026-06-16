using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LimeFlow.Domain.Models
{
    internal class Budget
    {
        public int Id { get; set; }
        public int Category { get; set; }
        public int AlertTreshold { get; set; }
        public int Period { get; set; }
        public string Notes { get; set; }
    }
}
