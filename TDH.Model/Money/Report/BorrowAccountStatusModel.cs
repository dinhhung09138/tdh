using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDH.Model.Money.Report
{
    /// <summary>
    /// Borror account status model
    /// </summary>
    public class BorrowAccountStatusModel
    {
        public Guid ID { get; set; }

        public string Name { get; set; } = "";

        public decimal Max { get; set; } = 0;

        public decimal Remain { get; set; } = 0;

        public string Title { get; set; } = "";

        public decimal Money { get; set; } = 0;

        public DateTime Date { get; set; } = DateTime.Now;
    }
}
