using System;

namespace TDH.ViewModel
{
    public class PostViewModel : MetaViewModel
    {
        public Guid ID { get; set; }

        public Guid CategoryID { get; set; }

        public string Title { get; set; } = "";

        public string Alias { get; set; } = "";

        public string Description { get; set; } = "";

        public string Content { get; set; } = "";

        public string Image { get; set; } = "";

        public DateTime CreateDate { get; set; } = new DateTime();
    }
}