using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDH.Model.Marketing.Youtube
{
    /// <summary>
    /// The statistics object encapsulates statistics for the channel.
    /// </summary>
    public class ChannelStatistics
    {
        /// <summary>
        /// The number of times the channel has been viewed.
        /// </summary>
        public long viewCount { get; set; } = 0;

        /// <summary>
        /// The number of comments for the channel
        /// </summary>
        public long commentCount { get; set; } = 0;

        /// <summary>
        /// The number of subscribers that the channel has.
        /// </summary>
        public long subscriberCount { get; set; } = 0;

        /// <summary>
        /// Indicates whether the channel's subscriber count is publicly visible.
        /// </summary>
        public bool hiddenSubscriberCount { get; set; } = false;

        /// <summary>
        /// The number of videos uploaded to the channel.
        /// </summary>
        public long videoCount { get; set; } = 0;

    }
}
