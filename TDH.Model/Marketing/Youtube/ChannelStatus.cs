using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDH.Model.Marketing.Youtube
{
    /// <summary>
    /// The status object encapsulates information about the privacy status of the channel.
    /// </summary>
    public class ChannelStatus
    {
        /// <summary>
        /// Privacy status of the channel.
        /// Private, Public, Unlisted
        /// </summary>
        public string privacyStatus { get; set; } = "";

        /// <summary>
        /// Indicates whether the channel data identifies a user that is already linked to either a YouTube username or a Google+ account. 
        /// A user that has one of these links already has a public YouTube identity, which is a prerequisite for several actions, such as uploading videos.
        /// </summary>
        public bool isLinked { get; set; } = false;

        /// <summary>
        /// Indicates whether the channel is eligible to upload videos that are more than 15 minutes long. 
        /// This property is only returned if the channel owner authorized the API request
        /// allowed
        /// Disallowed 
        /// eligible
        /// </summary>
        public string longUploadsStatus { get; set; } = "";

    }
}
