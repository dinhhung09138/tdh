using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDH.Model.Marketing.Youtube
{
    /// <summary>
    /// The snippet object contains basic details about the channel, such as its title, description, and thumbnail images.
    /// </summary>
    public class ChannelSnippet
    {
        /// <summary>
        /// The channel's title.
        /// </summary>
        public string title { get; set; } = "";

        /// <summary>
        /// The channel's description. The property's value has a maximum length of 1000 characters.
        /// </summary>
        public string description { get; set; } = "";

        /// <summary>
        /// The channel's custom URL
        /// </summary>
        public string customUrl { get; set; } = "";

        /// <summary>
        /// The date and time that the channel was created. The value is specified in ISO 8601 (YYYY-MM-DDThh:mm:ss.sZ) format
        /// </summary>
        public DateTime publishedAt { get; set; }

        /// <summary>
        /// A map of thumbnail images associated with the channe
        /// </summary>
        public ChannelSnippetThumbnail thumbnails { get; set; } = new ChannelSnippetThumbnail();

        /// <summary>
        /// The language of the text in the channel resource's snippet.title and snippet.description properties.
        /// </summary>
        public string defaultLanguage { get; set; } = "";

        /// <summary>
        /// The snippet.localized object contains a localized title and description for the channel 
        /// or it contains the channel's title and description in the default language for the channel's metadata.
        /// </summary>
        public ChannelSnippetLocalized localized { get; set; } = new ChannelSnippetLocalized();

        /// <summary>
        /// The country with which the channel is associated
        /// </summary>
        public string country { get; set; } = "";

    }
}
