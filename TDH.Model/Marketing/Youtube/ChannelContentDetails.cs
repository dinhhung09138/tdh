using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDH.Model.Marketing.Youtube
{
    /// <summary>
    /// The contentDetails object encapsulates information about the channel's content.
    /// </summary>
    public class ChannelContentDetails
    {
        /// <summary>
        /// The relatedPlaylists object is a map that identifies playlists associated with the channel, such as the channel's uploaded videos or liked videos
        /// </summary>
        public ChannelContentDetailsRelatedPlaylists relatedPlaylists { get; set; } = new ChannelContentDetailsRelatedPlaylists();        
    }
}
