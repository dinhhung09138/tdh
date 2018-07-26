using System;

namespace TDH.Model.Money
{
    /// <summary>
    /// Flow model
    /// </summary>
    public class FlowModel
    {
        /// <summary>
        /// The identifier
        /// </summary>
        public Guid ID { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Account's name send money
        /// </summary>
        public string FromName { get; set; } = "";

        /// <summary>
        /// Account's name receive money
        /// </summary>
        public string ToName { get; set; } = "";

        /// <summary>
        /// Category's name
        /// </summary>
        public string CategoryName { get; set; } = "";

        /// <summary>
        /// Title
        /// </summary>
        public string Title { get; set; } = "";

        /// <summary>
        /// Date, format as string
        /// </summary>
        public string DateString { get; set; } = "";

        /// <summary>
        /// Date
        /// </summary>
        public DateTime Date { get; set; } = DateTime.Now;

        /// <summary>
        /// Total money value, format as string
        /// </summary>
        public string Money { get; set; } = "";

        /// <summary>
        /// Total fee value, format as string
        /// </summary>
        public string Fee { get; set; } = "";

        /// <summary>
        /// Type's id
        /// </summary>
        public int Type { get; set; } = 0;

        /// <summary>
        /// Type's name
        /// </summary>
        public string TypeName { get; set; } = "";
    }
}
