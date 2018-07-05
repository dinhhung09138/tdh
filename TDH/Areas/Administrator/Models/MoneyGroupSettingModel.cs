﻿using System;
using System.ComponentModel.DataAnnotations;

namespace TDH.Areas.Administrator.Models
{
    public class MoneyGroupSettingModel : Utils.Database.BaseModel
    {
        public Guid ID { get; set; }

        /// <summary>
        /// Format yyyyMM
        /// </summary>
        [Required(ErrorMessage = "Nội dung không được rỗng")]
        public float YearMonth { get; set; }

        [Required(ErrorMessage = "Nội dung không được rỗng")]
        public Guid GroupID { get; set; }

        public string GroupName { get; set; } = "";
        
        public short PercentSetting { get; set; } = 0;

        public short PercentCurrent { get; set; } = 0;

        public short MoneySetting { get; set; } = 0;

        public short MoneyCurrent { get; set; } = 0;
        
    }
}