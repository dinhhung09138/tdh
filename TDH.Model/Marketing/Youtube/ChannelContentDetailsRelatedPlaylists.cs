using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDH.Model.Marketing.Youtube
{
    /// <summary>
    /// The relatedPlaylists object is a map that identifies playlists associated with the channel, 
    /// such as the channel's uploaded videos or liked videos
    /// </summary>
    public class ChannelContentDetailsRelatedPlaylists
    {
        /// <summary>
        /// The ID of the playlist that contains the channel's liked videos
        /// </summary>
        public string likes { get; set; } = "";

        /// <summary>
        /// The ID of the playlist that contains the channel's favorite videos
        /// </summary>
        public string favorites { get; set; } = "";

        /// <summary>
        /// The ID of the playlist that contains the channel's uploaded videos
        /// </summary>
        public string uploads { get; set; } = "";

        /// <summary>
        /// Note: This property has been deprecated.
        /// </summary>
        public string watchHistory { get; set; } = "";

        /// <summary>
        /// Note: This property has been deprecated.
        /// </summary>
        public string watchLater { get; set; } = "";
          
    }
}
