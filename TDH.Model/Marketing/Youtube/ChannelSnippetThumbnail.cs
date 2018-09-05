using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDH.Model.Marketing.Youtube
{
    /// <summary>
    /// A map of thumbnail images associated with the channe
    /// Valid key values are:
    /// default – The default thumbnail image.
    /// The default thumbnail for a video – or a resource that refers to a video, 
    /// such as a playlist item or search result – is 120px wide and 90px tall. 
    /// The default thumbnail for a channel is 88px wide and 88px tall.
    /// medium – A higher resolution version of the thumbnail image. 
    /// For a video (or a resource that refers to a video), 
    /// this image is 320px wide and 180px tall. For a channel, this image is 240px wide and 240px tall.
    /// high – A high resolution version of the thumbnail image. 
    ///         For a video (or a resource that refers to a video), 
    ///         this image is 480px wide and 360px tall. For a channel, this image is 800px wide and 800px tall.
    /// </summary>
    public class ChannelSnippetThumbnail
    {
        /// <summary>
        /// The image's URL
        /// </summary>
        public string url { get; set; } = "";

        /// <summary>
        /// The image's width.
        /// </summary>
        public int width { get; set; } = 0;

        /// <summary>
        /// The image's height.
        /// </summary>
        public int height { get; set; } = 0;

    }
}
