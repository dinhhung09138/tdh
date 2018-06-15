
using System;
using System.Collections.Generic;

namespace TDH.ViewModel
{
    public class NavigationViewModel : MetaViewModel
    {
        public Guid ID { get; set; }

        public string Title { get; set; } = "";

        public string Alias { get; set; } = "";

        public List<CategoryViewModel> Categories { get; set; } = new List<CategoryViewModel>();

        public List<PostViewModel> Posts { get; set; } = new List<PostViewModel>();
    }
}