using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TDH.Areas.Money.Utils
{
    public class Message
    {
        public static class Report
        {
            public static readonly string INDEX = "Báo cáo thống kê";
        }

        public static class Account
        {
            public static readonly string LIST = "Danh sách tài khoản";
            public static readonly string HISTORY = "Lịch sử giao dịch tài khoản";
            public static readonly string CREATE = "Thêm tài khoản";
            public static readonly string EDIT = "Cập nhật tài khoản";
        }

        public static class AccountType
        {
            public static readonly string LIST = "Danh sách nhóm tài khoản";
            public static readonly string CREATE = "Thêm nhóm tài khoản";
            public static readonly string EDIT = "Cập nhật nhóm tài khoản";
        }

        public static class Category
        {
            public static readonly string LIST = "Danh sách danh mục thu chi";
            public static readonly string HISTORY = "Lịch sử giao dịch";
            public static readonly string CREATE = "Thêm danh mục thu chi";
            public static readonly string EDIT = "Cập nhật danh mục thu chi";
        }

        public static class Group
        {
            public static readonly string LIST = "Danh sách quy tắc chi tiêu";
            public static readonly string CREATE = "Thêm quy tắc chi tiêu";
            public static readonly string SETTING = "Thiết lập quy tắc chi tiêu";
            public static readonly string EDIT = "Cập nhật quy tắc chi tiêu";
        }

        public static class Flow
        {
            public static readonly string INDEX = "Lịch sử thu nhập - chi tiêu - Chuyển khoản";
        }

    }
}