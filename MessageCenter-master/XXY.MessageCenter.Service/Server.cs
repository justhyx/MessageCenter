using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;
using XXY.MessageCenter.Common;
using XXY.MessageCenter.DbEntity;
using XXY.MessageCenter.DbEntity.Enums;
using XXY.MessageCenter.Queue;

namespace XXY.MessageCenter.Service {

    public class Server : ServiceControl {

        private QueueHolder Holder = null;
        private QueueHolder ProcessedHolder = null;


        [ImportMany]
        public IEnumerable<Lazy<IMessageClient>> Clients {
            get;
            set;
        }

        public Server(string queuePath, string processedQueuePath, IEnumerable<Type> supportDataTypes) {
            this.Holder = new QueueHolder(queuePath, supportDataTypes);
            this.ProcessedHolder = new QueueHolder(processedQueuePath, typeof(ProcessedMsg));
        }

        public bool Start(HostControl hostControl) {
            foreach (var c in this.Clients) {
                c.Value.OnProcessed += Processed;
            }
            this.Holder.OnDataReceived += Holder_OnDataReceived;
            this.Holder.Listen();
            return true;
        }


        void Processed(object sender, ProcessedArgs e) {
            try {
                this.ProcessedHolder.Put(new ProcessedMsg() {
                    Error = e.Exception != null ? e.Exception.Message : null,
                    IsSuccessed = e.Exception == null,
                    MsgID = e.ID,
                    MsgType = e.MsgType
                });
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
        }

        public bool Stop(HostControl hostControl) {
            foreach (var c in this.Clients) {
                c.Value.OnProcessed -= Processed;
            }
            this.Holder.OnDataReceived -= Holder_OnDataReceived;
            return true;
        }

        async void Holder_OnDataReceived(object sender, DataReceivedArgs e) {
            if (e.Data != null) {
                var msg = (BaseMessage)e.Data;
                if (msg != null) {
                    var client = this.Clients.FirstOrDefault(c => c.Value.AcceptMessageType.Equals(msg.GetType()));
                    if (client != null) {
                        try {
                            await client.Value.Send(msg);
                        } catch (Exception ex) {
                            this.Processed(this, new ProcessedArgs(msg.MsgType, msg.ID, ex));
                            Console.WriteLine(ex.Message);
                        }
                    }
                }
            }
        }
    }
}
