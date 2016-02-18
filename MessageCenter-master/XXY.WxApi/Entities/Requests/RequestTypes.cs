using System.Xml.Serialization;

namespace XXY.WxApi.Entities.Requests {
    public enum RequestTypes {
        /// <summary>
        /// 文本消息
        /// </summary>
        [XmlEnum(Name = "text")]
        Text,

        /// <summary>
        /// 事件消息
        /// </summary>
        [XmlEnum(Name = "event")]
        Event,

        /// <summary>
        /// 图片消息
        /// </summary>
        [XmlEnum(Name = "image")]
        Image,

        /// <summary>
        /// 语音消息
        /// </summary>
        [XmlEnum(Name = "voice")]
        Voice,

        /// <summary>
        /// 视频消息
        /// </summary>
        [XmlEnum(Name = "video")]
        Video,

        /// <summary>
        /// 小视频消息
        /// </summary>
        [XmlEnum(Name = "shortvideo")]
        ShortVideo,

        /// <summary>
        /// 地理位置消息
        /// </summary>
        [XmlEnum(Name = "location")]
        Location,

        /// <summary>
        /// 链接消息
        /// </summary>
        [XmlEnum(Name = "link")]
        Link
    }
}
