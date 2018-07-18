
namespace TDH.Model.Money
{
    public class ReportCollectionByYearModel
    {
        public int Year { get; set; }

        public int Month { get; set; }

        public decimal Income { get; set; }

        public decimal Payment { get; set; }

        public decimal Total { get; set; }
    }
}
