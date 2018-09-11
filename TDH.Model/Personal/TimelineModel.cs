using System;
namespace TDH.Model.Personal
{
    public class TimelineModel
    {
        /// <summary>
        /// The identifier
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// School
        /// </summary>
        public string School { get; set; }

        /// <summary>
        /// Duration
        /// </summary>
        public string Duration { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// YYYYMM
        /// </summary>
        public int MonthYear { get; set; } = 0;

        /// <summary>
        /// Date
        /// </summary>
        public DateTime Date { get; set; } = new DateTime();

        /// <summary>
        /// Date as format string
        /// </summary>
        public string DateString { get; set; } = "";

        /// <summary>
        /// Is planning status
        /// Default is true
        /// </summary>
        public bool IsPlan { get; set; } = true;

        /// <summary>
        /// Is finish status
        /// Default is false
        /// </summary>
        public bool IsFinish { get; set; } = false;

        /// <summary>
        /// Is cancel status
        /// Default is false
        /// </summary>
        public bool IsCancel { get; set; } = false;

        /// <summary>
        /// Education type code
        /// </summary>
        public string TypeCode { get; set; }

        /// <summary>
        /// Education type name
        /// </summary>
        public string TypeName { get; set; }

    }
}
