using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TDH.ViewModel
{
    public class CategoryViewModel : MetaViewModel
    {
        public Guid ID { get; set; }

        public Guid NavigationID { get; set; }

        public string Title { get; set; } = "";

        public string Alias { get; set; } = "";

        public int Count { get; set; } = 0;
        
        public List<PostViewModel> Posts { get; set; } = new List<PostViewModel>();

    }
}