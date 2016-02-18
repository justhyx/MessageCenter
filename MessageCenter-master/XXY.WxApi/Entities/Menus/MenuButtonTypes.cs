using Newtonsoft.Json;
using System.ComponentModel;

namespace XXY.WxApi.Entities.Menus {
    public enum MenuButtonTypes {

        [Description("点击推事件")]
        [JsonProperty("click")]
        click,

        [Description("跳转URL")]
        [JsonProperty("view")]
        view,

        [Description("扫码推事件")]
        [JsonProperty("scancode_push")]
        scancode_push,

        [Description("扫码推事件且弹出“消息接收中”提示框")]
        [JsonProperty("scancode_waitmsg")]
        scancode_waitmsg,

        [Description("弹出系统拍照发图")]
        [JsonProperty("pic_sysphoto")]
        pic_sysphoto,

        [Description("弹出拍照或者相册发图")]
        [JsonProperty("pic_photo_or_album")]
        pic_photo_or_album,

        [Description("弹出微信相册发图器")]
        [JsonProperty("pic_weixin")]
        pic_weixin,

        [Description("弹出地理位置选择器")]
        [JsonProperty("location_select")]
        location_select
    }
}
