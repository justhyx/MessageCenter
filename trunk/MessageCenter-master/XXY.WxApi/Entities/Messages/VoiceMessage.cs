
namespace XXY.WxApi.Entities.Messages {
    public class VoiceMessage : Message {

        public override MsgTypes MsgType {
            get {
                return MsgTypes.Voice;
            }
        }
    }
}
