using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XXY.MessageCenter.DbContext;
using XXY.MessageCenter.DbEntity;
using XXY.MessageCenter.DbEntity.Enums;
using XXY.Common.Extends;
using System.Data.Entity;
using Microsoft.Practices.Unity;
using XXY.MessageCenter.IBiz;
using XXY.MessageCenter.Queue;
using Microsoft.Practices.ServiceLocation;
using XXY.MessageCenter.BizEntity.Conditions;
using XXY.MessageCenter.TxtMsgHub;
using System.Data.Entity;

namespace XXY.MessageCenter.Biz {

    public abstract class BaseMessageHandler : BaseBiz {

        public static readonly List<Type> SupportDataTypes = new List<Type>() {
            typeof(EMailMessage),
            typeof(SMSMessage),
            typeof(QQMessage),
            typeof(WeChatMessage)
        };

        public Lazy<IConfig> Config {
            get;
            set;
        }

        public BaseMessage Msg {
            get;
            set;
        }

        public BaseMessageHandler() {
            this.Config = ServiceLocator.Current.GetInstance<Lazy<IConfig>>();
        }

        public abstract Task<bool> Handle();

        public abstract Task Update(Entities db, ProcessedMsg data);

        public abstract Task<IEnumerable<BaseMessage>> Search(MessageSearchCondition cond);

        public abstract Task<BaseMessage> Get(int id);

        public abstract Task<bool> Delete(int id);
    }









    public abstract class BaseMessageHandler<T> : BaseMessageHandler where T : BaseMessage {

        protected async virtual Task<bool> InsertToDb() {
            this.Msg.Status = MsgStatus.Processing;
            this.SetCreateInfo(this.Msg);
            using (var db = new Entities()) {
                db.Set<T>().Add((T)this.Msg);
                this.Errors = db.GetErrors();
                if (!this.HasError) {
                    await db.SaveChangesAsync();
                    return true;
                }
                return false;
            }
        }

        public override async Task Update(Entities db, ProcessedMsg data) {
            var entry = await db.Set<T>().FirstOrDefaultAsync(t => !t.IsDeleted && t.ID == data.MsgID);
            if (entry != null) {
                entry.Status = data.IsSuccessed ? MsgStatus.Processed : MsgStatus.Failed;
                this.SetModifyInfo(entry);
            }
        }

        public async override Task<bool> Handle() {
            var holder = new QueueHolder(this.Config.Value.MessageMSMQPath, SupportDataTypes);
            if (await this.InsertToDb())
                return holder.Put((T)this.Msg, this.ConvertPriority(this.Msg.PRI));
            else
                return false;
        }

        private Queue.Priorities ConvertPriority(XXY.MessageCenter.DbEntity.Enums.Priorities pri) {
            switch (pri) {
                case DbEntity.Enums.Priorities.Normal:
                    return Queue.Priorities.Normal;
                case DbEntity.Enums.Priorities.Lower:
                    return Queue.Priorities.Lower;
                case DbEntity.Enums.Priorities.Immediately:
                    return Queue.Priorities.Immediately;
                case DbEntity.Enums.Priorities.Higher:
                    return Queue.Priorities.Higher;
                default:
                    return Queue.Priorities.Normal;
            }
        }

        public override async Task<IEnumerable<BaseMessage>> Search(MessageSearchCondition cond) {
            using (var db = new Entities()) {
                var datas = await cond.Filter(db.Set<T>().Where(t => !t.IsDeleted))
                    .OrderByDescending(t => t.ID)
                    .DoPage(cond.Pager)
                    .ToListAsync();

                //MsgType 并没有映射到表中, 只是构造函数中的一个参数而已.
                //所以上一段是要先 ToList

                var query2 = from m in datas
                             join e in db.FailedMessages on
                                 new {
                                     m.ID,
                                     m.MsgType
                                 }
                                 equals
                                 new {
                                     ID = e.MsgID,
                                     e.MsgType
                                 } into es
                             select new {
                                 m,
                                 es
                             };

                List<BaseMessage> results = new List<BaseMessage>();
                foreach (var d in query2) {
                    var data = d.m;
                    data.ErrorInfo = string.Join(";", d.es.Select(E => E.Log));
                    //yield return data;
                    results.Add(data);
                }

                return results;
            }
        }

        public override async Task<BaseMessage> Get(int id) {
            using (var db = new Entities()) {
                return await db.Set<T>().FirstOrDefaultAsync(t => !t.IsDeleted && t.ID == id);
            }
        }

        public override async Task<bool> Delete(int id) {
            using (var db = new Entities()) {
                var ex = await db.Set<T>().FirstOrDefaultAsync(t => !t.IsDeleted && t.ID == id);
                if (ex != null) {
                    ex.IsDeleted = true;
                    this.SetModifyInfo(ex);
                    await db.SaveChangesAsync();
                    return true;
                }
            }

            return false;
        }
    }








    public static class MessageHandlerFactory {

        public static BaseMessageHandler GetHandler(BaseMessage msg) {
            var handler = GetHandler(msg.MsgType);
            if (handler != null)
                handler.Msg = msg;
            return handler;
        }

        public static BaseMessageHandler GetHandler(MsgTypes msgType) {
            BaseMessageHandler handler = null;
            switch (msgType) {
                case MsgTypes.Email:
                    handler = new EmailMessageHandler();
                    break;
                case MsgTypes.Txt:
                    handler = new TxtMessageHandler();
                    break;
                case MsgTypes.QQ:
                    handler = new QQMessageHandler();
                    break;
                case MsgTypes.SMS:
                    handler = new SmsMessageHandler();
                    break;
                case MsgTypes.WeChat:
                    handler = new WeChatMessageHandler();
                    break;
            }
            return handler;
        }
    }








    public class EmailMessageHandler : BaseMessageHandler<EMailMessage> {
    }

    public class TxtMessageHandler : BaseMessageHandler<TxtMessage> {
        public override async Task<bool> Handle() {
            if (await this.InsertToDb()) {
                var msg = (TxtMessage)this.Msg;
                TxtMsgSender.Send(msg.ReceiverID.ToString(), msg);
                return true;
            }
            return false;
        }
    }

    public class SmsMessageHandler : BaseMessageHandler<SMSMessage> {
    }

    public class QQMessageHandler : BaseMessageHandler<QQMessage> {
    }

    public class WeChatMessageHandler : BaseMessageHandler<WeChatMessage> {
    }
}
