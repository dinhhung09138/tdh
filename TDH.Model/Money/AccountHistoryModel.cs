using System;

namespace TDH.Model.Money
{
    public class AccountHistoryModel
    {
        public DateTime Date { get; set; }

        public string DateString { get; set; }

        public string Title { get; set; }

        public decimal Money { get; set; }

        public string MoneyString { get; set; }

        public int Type { get; set; }
    }
}
