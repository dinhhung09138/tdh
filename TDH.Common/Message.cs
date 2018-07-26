using System;
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
    }
}
