using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TDH.Areas.Administrator.Models
{
    public class MoneyReportCollectionByYearModel
    {
        public int Year { get; set; }

        public int Month { get; set; }

        public decimal Income { get; set; }

        public decimal Payment { get; set; }

        public decimal Total { get; set; }
    }
}