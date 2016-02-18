using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using XXY.Common;
using XXY.Common.Attributes;
using XXY.MessageCenter.BizEntity.Conditions;
using XXY.MessageCenter.DbContext;
using XXY.MessageCenter.DbEntity;
using XXY.MessageCenter.DbEntity.Enums;
using XXY.MessageCenter.IBiz;
using XXY.Common.Extends;

namespace XXY.MessageCenter.Biz {

    [AutoInjection(typeof(IMessageViewer))]
    public class MessageViewerBiz : BaseBiz, IMessageViewer {
        public async Task<IEnumerable<BaseMessage>> Search(MessageSearchCondition cond) {
            var handler = MessageHandlerFactory.GetHandler(cond.MsgType);
            return await handler.Search(cond);
        }


        public async Task<BaseMessage> Get(MsgTypes type, int id) {
            var handler = MessageHandlerFactory.GetHandler(type);
            return await handler.Get(id);
        }


        public async Task<bool> Delete(MsgTypes type, int id) {
            var handler = MessageHandlerFactory.GetHandler(type);
            return await handler.Delete(id);
        }


        public async Task<TxtMessage> GetTxtMsg(int msgID, decimal receiverID, bool setReaded = false) {
            using (var db = new Entities()) {
                var data = await db.TxtMessages.FirstOrDefaultAsync(t => !t.IsDeleted && t.ReceiverID == receiverID && t.ID == msgID);
                if (setReaded) {
                    data.Readed = true;
                    this.SetModifyInfo(data);
                    await db.SaveChangesAsync();
                }
                return data;
            }
        }


        public async Task<int> GetUnReadTxtMsgCount(decimal receiverID) {
            using (var db = new Entities()) {
                return await db.TxtMessages.CountAsync(t => t.ReceiverID == receiverID && !t.IsDeleted && !t.Readed);
            }
        }


        public async Task<bool> SetTxtMsgReaded(int msgID) {
            using (var db = new Entities()) {
                var c = await db.TxtMessages.FirstOrDefaultAsync(t => t.ID == msgID && !t.IsDeleted && !t.Readed);
                if (c != null) {
                    c.Readed = true;
                    this.SetModifyInfo(c);
                    await db.SaveChangesAsync();
                    return true;
                }
            }
            return false;
        }


        public async Task<IEnumerable<TxtMessage>> GetTxtMsg(decimal receiverID, Pager pager = null, bool onlyUnread = true) {
            using (var db = new Entities()) {
                var query = db.TxtMessages.Where(t => t.ReceiverID == receiverID && !t.IsDeleted);
                if (onlyUnread) {
                    query = query.Where(t => !t.Readed);
                }

                if (pager == null)
                    pager = new Pager();

                return await query.OrderByDescending(t => t.CreateOn)
                    .DoPage(pager)
                    .ToListAsync();
            }
        }


        public async Task<bool> DeleteTxtMsg(int msgID, decimal receiverID) {
            using (var db = new Entities()) {
                var data = await db.TxtMessages.FirstOrDefaultAsync(t => t.ID == msgID && t.ReceiverID == receiverID && !t.IsDeleted);
                if (data != null) {
                    data.IsDeleted = true;
                    this.SetModifyInfo(data);
                    await db.SaveChangesAsync();
                    return true;
                }
            }
            return false;
        }
    }
}
