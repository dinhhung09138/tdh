using System;

namespace TDH.Model.Money
{
    /// <summary>
    /// 
    /// </summary>
    public class FlowModel
    {
        /// <summary>
        /// 
        /// </summary>
        public Guid ID { get; set; } = Guid.NewGuid();

        /// <summary>
        /// 
        /// </summary>
        public string FromName { get; set; } = "";

        /// <summary>
        /// 
        /// </summary>
        public string ToName { get; set; } = "";

        /// <summary>
        /// 
        /// </summary>
        public string CategoryName { get; set; } = "";

        /// <summary>
        /// 
        /// </summary>
        public string Title { get; set; } = "";

        /// <summary>
        /// 
        /// </summary>
        public string DateString { get; set; } = "";

        /// <summary>
        /// 
        /// </summary>
        public DateTime Date { get; set; } = DateTime.Now;

        /// <summary>
        /// 
        /// </summary>
        public string Money { get; set; } = "";

        /// <summary>
        /// 
        /// </summary>
        public string Fee { get; set; } = "";

        /// <summary>
        /// 
        /// </summary>
        public int Type { get; set; } = 0;

        /// <summary>
        /// 
        /// </summary>
        public string TypeName { get; set; } = "";
    }
}
