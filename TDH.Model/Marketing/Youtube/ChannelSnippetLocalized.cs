using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDH.Model.Marketing.Youtube
{
    /// <summary>
    /// The snippet.localized object contains a localized title and description for the channel 
    /// or it contains the channel's title and description in the default language for the channel's metadata.
    /// </summary>
    public class ChannelSnippetLocalized
    {
        /// <summary>
        /// The localized channel title.
        /// </summary>
        public string title { get; set; } = "";

        /// <summary>
        /// The localized channel description.
        /// </summary>
        public string description { get; set; } = "";
    }
}
