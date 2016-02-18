using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net.Mail;
using System.Security.Permissions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using XXY.MessageCenter.Common;
using XXY.MessageCenter.DbEntity;
using XXY.MessageCenter.DbEntity.Enums;

namespace XXY.MessageCenter.Email {

    [Export(typeof(IMessageClient))]
    public class EmailClient : BaseMessageClient, IMessageClient {

        public event EventHandler<ProcessedArgs> OnProcessed;

        public Type AcceptMessageType {
            get {
                return typeof(EMailMessage);
            }
        }

        public async Task Send(BaseMessage msg) {
            var data = (EMailMessage)msg;

            using (var client = new System.Net.Mail.SmtpClient()) {
                client.SendCompleted += client_SendCompleted;
                var mail = new MailMessage();

                mail.Subject = data.Subject;
                mail.Body = data.Ctx;
                mail.IsBodyHtml = true;
                var receivers = data.Receiver.ToMailAddress();
                foreach (var r in receivers)
                    mail.To.Add(r);


                await client.SendMailAsync(mail, msg)
                .ContinueWith(t => {
                    t.Exception.Handle(ex => {
                        return true;//如果不加这一句，发邮件异常的时候，会直接关闭程序。
                    });
                }, TaskContinuationOptions.OnlyOnFaulted)
                .ContinueWith(t => {
                    //为毛会到这里？
                    var ex = t.Exception;
                }, TaskContinuationOptions.OnlyOnCanceled);
            }
        }

        private void client_SendCompleted(object sender, AsyncCompletedEventArgs e) {
            var state = (TaskCompletionSource<object>)e.UserState;
            var msg = (EMailMessage)state.Task.AsyncState;

            if (this.OnProcessed != null) {
                this.OnProcessed(this, new ProcessedArgs(msg.MsgType, msg.ID, e.Error));
            }
        }

        public override void Init() {

        }
    }
}
