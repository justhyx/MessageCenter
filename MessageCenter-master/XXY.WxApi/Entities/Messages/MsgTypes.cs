using XXY.WxApi.Attributes;

namespace XXY.WxApi.Entities.Messages {
    public enum MsgTypes {
        [SpecifyValue("text")]
        Text,

        [SpecifyValue("image")]
        Image,

        [SpecifyValue("voice")]
        Voice,

        [SpecifyValue("video")]
        Video,

        [SpecifyValue("music")]
        Music,

        [SpecifyValue("news")]
        News
    }
}
