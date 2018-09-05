using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDH.Model.Marketing.Youtube
{
    public class Channel
    {
        /// <summary>
        /// Identifies the API resource's type. The value will be youtube#channel.
        /// </summary>
        public string kind { get; set; } = "";

        /// <summary>
        /// The Etag of this resource.
        /// </summary>
        public string etag { get; set; } = "";

        /// <summary>
        /// The ID that YouTube uses to uniquely identify the channel.
        /// </summary>
        public string id { get; set; } = "";

        /// <summary>
        /// The snippet object contains basic details about the channel, such as its title, description, and thumbnail images.
        /// </summary>
        public ChannelSnippet snippet { get; set; } = new ChannelSnippet();

        /// <summary>
        /// The contentDetails object encapsulates information about the channel's content.
        /// </summary>
        public ChannelContentDetails contentDetails { get; set; } = new ChannelContentDetails();

        /// <summary>
        /// The statistics object encapsulates statistics for the channel.
        /// </summary>
        public ChannelStatistics statistics { get; set; } = new ChannelStatistics();

        /// <summary>
        /// The topicDetails object encapsulates information about topics associated with the channel
        /// </summary>
        public ChannelTopicDetails topicDetails { get; set; } = new ChannelTopicDetails();

        /// <summary>
        /// The status object encapsulates information about the privacy status of the channel.
        /// </summary>
        public ChannelStatus status { get; set; } = new ChannelStatus();

        /// <summary>
        /// TODO
        /// https://developers.google.com/youtube/v3/docs/channels#kind
        /// brandingSettings
        /// </summary>
        ///public ChannelTopicDetails topicDetails { get; set; } = new ChannelTopicDetails();

        /// <summary>
        /// 
        /// </summary>
        ///public ChannelTopicDetails topicDetails { get; set; } = new ChannelTopicDetails();
    }
}
