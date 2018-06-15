﻿using System;
namespace TDH.Areas.Administrator.Models
{
    public class HomeCategoryModel : Utils.Database.BaseModel
    {
        public Guid ID { get; set; }

        public Guid CategoryID { get; set; }

        public string CategoryTitle { get; set; } = "";

        public string NavigationTitle { get; set; } = "";

        public short Ordering { get; set; } = 0;

        public bool Selected { get; set; } = false;
    }
}