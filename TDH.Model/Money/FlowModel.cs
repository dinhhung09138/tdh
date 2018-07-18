using System;

namespace TDH.Model.Money
{
    public class FlowModel
    {
        public Guid ID { get; set; } = Guid.NewGuid();

        public string FromName { get; set; } = "";

        public string ToName { get; set; } = "";

        public string CategoryName { get; set; } = "";

        public string Title { get; set; } = "";

        public string DateString { get; set; } = "";

        public DateTime Date { get; set; } = DateTime.Now;

        public string Money { get; set; } = "";

        public string Fee { get; set; } = "";

        public int Type { get; set; } = 0;

        public string TypeName { get; set; } = "";
    }
}
