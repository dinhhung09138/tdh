using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDH.Model.Marketing.Youtube
{
    /// <summary>
    /// The topicDetails object encapsulates information about topics associated with the channe
    /// </summary>
    public class ChannelTopicDetails
    {
        /// <summary>
        /// A list of topic IDs associated with the channel.
        /// </summary>
        public List<string> topicIds { get; set; } = new List<string>();

        /// <summary>
        /// A list of Wikipedia URLs that describe the channel's content.
        /// </summary>
        public List<string> topicCategories { get; set; } = new List<string>();

    }
}
