using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace XXY.MessageCenter.Queue {
    public class JsonMessageFormater : IMessageFormatter {
        [ThreadStatic]
        private static byte[] mBuffer;

        [ThreadStatic]
        private static System.IO.MemoryStream mStream;

        private Type type = null;

        private JsonSerializerSettings setting = new JsonSerializerSettings() {
            ContractResolver = new AllPropertiesResolver()
        };

        public JsonMessageFormater(Type t) {
            this.type = t;
        }

        public bool CanRead(Message message) {
            return message.BodyStream != null && message.BodyStream.Length > 0;
        }

        public object Read(Message message) {
            if (mBuffer == null)
                mBuffer = new byte[4096];
            int count = (int)message.BodyStream.Length;
            message.BodyStream.Read(mBuffer, 0, count);
            return JsonConvert.DeserializeObject(Encoding.UTF8.GetString(mBuffer, 0, count), this.type);

        }

        public void Write(Message message, object obj) {
            if (mStream == null)
                mStream = new System.IO.MemoryStream(4096);
            mStream.Position = 0;
            mStream.SetLength(4095);
            string value = JsonConvert.SerializeObject(obj, setting);
            int count = Encoding.UTF8.GetBytes(value, 0, value.Length, mStream.GetBuffer(), 0);
            mStream.SetLength(count);
            message.BodyStream = mStream;
        }

        public object Clone() {
            return this;
        }
    }

    public class JsonMessageFormater<T> : JsonMessageFormater {
        public JsonMessageFormater()
            : base(typeof(T)) {
        }
    }
}
