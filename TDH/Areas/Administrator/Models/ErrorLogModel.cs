using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TDH.Areas.Administrator.Models
{
    public class ErrorLogModel : Utils.Database.BaseModel
    {
        public Guid ID { get; set; }

        public string FileName { get; set; }

        public string MethodName { get; set; }

        public string Message { get; set; }

        public string InnerException { get; set; }

        public string StackTrace { get; set; }

        public DateTime Date { get; set; }

        public string DateString { get; set; }

    }
}