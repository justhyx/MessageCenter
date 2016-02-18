using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;
using XXY.Common.Extends;
using XXY.WxApi.Entities;
using XXY.WxApi.Entities.Requests;

namespace XXY.WxApi {
    public static class ClientHelper {

        private static readonly Regex MsgTypeRx = new Regex(@"<MsgType><!\[CDATA\[(?<msgType>[^\]]*)\]\]></MsgType>");
        private static readonly Regex EventTypeRx = new Regex(@"<Event><!\[CDATA\[(?<eventType>[^\]]*)\]\]></Event>");

        /// <summary>
        /// 解析请求
        /// </summary>
        /// <param name="client"></param>
        /// <param name="xml"></param>
        /// <param name="useAes"></param>
        /// <returns></returns>
        public static BaseRequest Parse(this ApiClient client, string xml, bool useAes) {

            try {
                //如果启用AES加密, 返回的XML中, Encrypt 元素的内容是加密后的结果
                if (useAes) {
                    var doc = new XmlDocument();
                    doc.LoadXml(xml);

                    xml = doc.DocumentElement.SelectSingleNode("Encrypt").FirstChild.Value;

                    var appID = "";
                    //使用的是AES加密,但是处理方式可能和你写的逻辑不一样,所以,请使用TX提供的AES处理程序
                    xml = Cryptography.AESDecrypt(xml, client.Config.AesKey, ref appID);
                }

                //EventType 是事件推送的结果中的.
                //普通消息是没有这个字段的
                EventTypes? eventType = null;
                var ma = EventTypeRx.Match(xml);
                if (ma.Success)
                    eventType = ma.Groups["eventType"].Value.ToEnum<EventTypes>();

                //消息类型, 事件推送的结果全是 event
                ma = MsgTypeRx.Match(xml);
                if (ma.Success) {
                    var msgType = ma.Groups["msgType"].Value.ToEnum<RequestTypes>();

                    Type targetType = BaseRequest.GetMessageType(msgType, eventType);
                    if (targetType != null) {
                        var bytes = Encoding.UTF8.GetBytes(xml);
                        var ser = new XmlSerializer(targetType);
                        using (var msm = new MemoryStream(bytes)) {
                            return ser.Deserialize(msm) as BaseRequest;
                        }
                    }
                }
            } catch {

            }

            return null;
        }


        private static readonly string Fmt = @"<xml>
<Encrypt><![CDATA[{0}]]></Encrypt>
<MsgSignature><![CDATA[{1}]]></MsgSignature>
<TimeStamp><![CDATA[{2}]]></TimeStamp>
<Nonce><![CDATA[{3}]]></Nonce>
</xml>";

        /// <summary>
        /// 处理回复, 如果不使用 aes 加密, 则不加密
        /// </summary>
        /// <param name="client"></param>
        /// <param name="msg"></param>
        /// <param name="nonce"></param>
        /// <param name="useAes"></param>
        /// <returns></returns>
        public static string Encrypt(this ApiClient client, Reply msg, string nonce, bool useAes) {
            if (msg == null)
                throw new ArgumentNullException("msg");

            var ctx = "";
            var ser = new XmlSerializer(typeof(Reply));
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            using (var msm = new MemoryStream()) {
                ser.Serialize(msm, msg, ns);
                ctx = Encoding.UTF8.GetString(msm.GetBuffer());
            }

            if (useAes) {
                ctx = Cryptography.AESEncrypt(ctx, client.Config.AesKey, client.Config.AppID);

                var ticks = DateTime.Now.ToUnixTimestamp();
                var signature = Cryptography.Signature(client.Config.Token, nonce, ctx, ticks.ToString()).ToLower();
                return string.Format(Fmt, ctx, signature, ticks, nonce);
            } else
                return ctx;
        }

        public static long ToUnixTimestamp(this DateTime date) {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            TimeSpan diff = date - origin;
            return (long)Math.Floor(diff.TotalSeconds);
        }

        public static DateTime FromUnixTimestamp(this long timestamp) {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return origin.AddSeconds(timestamp);
        }
    }
}
