using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XXY.Common;
using XXY.MessageCenter.BizEntity.Conditions;
using XXY.MessageCenter.DbEntity;
using XXY.MessageCenter.DbEntity.Enums;

namespace XXY.MessageCenter.IBiz {
    public interface IMessageViewer {

        Task<IEnumerable<BaseMessage>> Search(MessageSearchCondition cond);

        Task<BaseMessage> Get(MsgTypes type, int id);

        /// <summary>
        /// For Admin
        /// </summary>
        /// <param name="type"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> Delete(MsgTypes type, int id);



        /// <summary>
        /// 
        /// </summary>
        /// <param name="msgID"></param>
        /// <param name="receiverID"></param>
        /// <param name="setReaded">是否设置已读</param>
        /// <returns></returns>
        Task<TxtMessage> GetTxtMsg(int msgID, decimal receiverID, bool setReaded = false);

        /// <summary>
        /// For User
        /// </summary>
        /// <param name="msgID"></param>
        /// <param name="receiverID"></param>
        /// <returns></returns>
        Task<bool> DeleteTxtMsg(int msgID, decimal receiverID);

        Task<IEnumerable<TxtMessage>> GetTxtMsg(decimal receiverID, Pager pager = null, bool onlyUnread = true);

        Task<int> GetUnReadTxtMsgCount(decimal receiverID);

        Task<bool> SetTxtMsgReaded(int msgID);
    }
}
