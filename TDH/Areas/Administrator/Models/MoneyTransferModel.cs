﻿using System;
using System.ComponentModel.DataAnnotations;

namespace TDH.Areas.Administrator.Models
{
    public class MoneyTransferModel : Utils.Database.BaseModel
    {
        public Guid ID { get; set; }

        [Required(ErrorMessage = "Nội dung không được rỗng")]
        public Guid AccountFrom { get; set; }

        [Required(ErrorMessage = "Nội dung không được rỗng")]
        public Guid AccountTo { get; set; }

        [Required(ErrorMessage = "Nội dung không được rỗng")]
        [StringLength(100, ErrorMessage = "Nội dung không quá 100 ký tự")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Nội dung không được rỗng")]
        public DateTime Date { get; set; }

        public string DateString { get; set; } = "";

        [StringLength(255, ErrorMessage = "Nội dung không quá 255 ký tự")]
        public string Purpose { get; set; }

        [StringLength(255, ErrorMessage = "Nội dung không quá 255 ký tự")]
        public string Notes { get; set; }

        [Required(ErrorMessage = "Nội dung không được rỗng")]
        public decimal Money { get; set; }

        [Required(ErrorMessage = "Nội dung không được rỗng")]
        public decimal Fee { get; set; }
    }
}