﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDH.Common
{
    /// <summary>
    /// Common setting message
    /// </summary>
    public class Message
    {
        /// <summary>
        /// The data are using, cann't delete
        /// </summary>
        public static readonly string CheckExists = "Dữ liệu đang được sử dụng, không thể xóa";

        /// <summary>
        /// Delete success
        /// </summary>
        public static readonly string DeleteSuccess = "Xóa dữ liệu thành công";

        /// <summary>
        /// Error while execute process
        /// </summary>
        public static readonly string Error = "Lỗi trong quá trình thực thi";

        /// <summary>
        /// Insert success
        /// </summary>
        public static readonly string InsertSuccess = "Thêm mới dữ liệu thành công";

        /// <summary>
        /// Update success
        /// </summary>
        public static readonly string Success = "Cập nhật dữ liệu thành công";

        /// <summary>
        /// Update success
        /// </summary>
        public static readonly string UpdateSuccess = "Cập nhật dữ liệu thành công";

        /// <summary>
        /// Item not found
        /// </summary>
        public static readonly string ItemNotFound = "Nội dung không được tìm thấy";
    }

    /// <summary>
    /// Error message
    /// 
    ///status    user      time
    ///create    hung      07/11/2018
    /// 
    /// 
    /// </summary>
    public class ErrorMessage
    {
        /// <summary>
        /// Error service
        /// </summary>
        public static readonly string ErrorService = "Service has an error";

        /// <summary>
        /// Error controller
        /// </summary>
        public static readonly string ErrorController = "Service has an error";
    }
}
