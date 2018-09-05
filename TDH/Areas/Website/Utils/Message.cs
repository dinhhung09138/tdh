using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TDH.Areas.Website.Utils
{
    public class Message
    {
        public static class Navigation
        {
            public static readonly string LIST = "Danh mục bài viết";
            public static readonly string CREATE = "Thêm danh mục bài viết";
            public static readonly string EDIT = "Cập nhật danh mục bài viết";
        }
        public static class Category
        {
            public static readonly string LIST = "Chủ đề bài viết";
            public static readonly string CREATE = "Thêm chủ đề bài viết";
            public static readonly string EDIT = "Cập nhật chủ đề bài viết";
        }
        public static class News
        {
            public static readonly string LIST = "Danh sách bài viết";
            public static readonly string CREATE = "Thêm bài viết";
            public static readonly string EDIT = "Cập nhật bài viết";
        }
        public static class About
        {
            public static readonly string LIST = "Giới thiệu";
        }
        public static class Setting
        {
            public static readonly string NAVIGATION = "Danh mục hiển thị ở trang chủ";
            public static readonly string CATEGORY = "Nhóm chủ đề hiển thị ở trang chủ";
            public static readonly string CONFIGURATION = "Thiết lập chung";
            public static readonly string EDIT_CONFIGURATION = "Cập nhật thông tin thiết lập";
        }
    }
}