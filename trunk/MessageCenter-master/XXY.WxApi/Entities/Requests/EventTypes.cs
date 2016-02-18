using System.Xml.Serialization;

namespace XXY.WxApi.Entities.Requests {
    public enum EventTypes {

        /// <summary>
        /// 订阅
        /// </summary>
        [XmlEnum("subscribe")]
        Subscribe,

        /// <summary>
        /// 取消订阅
        /// </summary>
        [XmlEnum("unsubscribe")]
        Unsubscribe,

        /// <summary>
        /// 
        /// </summary>
        [XmlEnum("SCAN")]
        Scan,

        /// <summary>
        /// 上报地理位置事件
        /// </summary>
        [XmlEnum("LOCATION")]
        Location,



        [XmlEnum("CLICK")]
        Click,


        [XmlEnum("VIEW")]
        View
    }
}
