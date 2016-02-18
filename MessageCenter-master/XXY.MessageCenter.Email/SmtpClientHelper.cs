using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Mail;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace XXY.MessageCenter.Email {
    public static class SmtpClientHelper {

        [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
        public static Task SendMailAsync(this SmtpClient client, MailMessage message, object userState) {
            TaskCompletionSource<object> tcs = new TaskCompletionSource<object>(userState);

            SendCompletedEventHandler handler = null;
            handler = delegate(object sender, AsyncCompletedEventArgs e) {
                HandleCompletion((SmtpClient)sender, tcs, e, handler);
            };
            client.SendCompleted += handler;

            try {
                client.SendAsync(message, tcs);
            } catch {
                client.SendCompleted -= handler;
                throw;
            }
            return tcs.Task;
        }

        private static void HandleCompletion(SmtpClient client, TaskCompletionSource<object> tcs, AsyncCompletedEventArgs e, SendCompletedEventHandler handler) {
            if (e.UserState != tcs)
                return;
            try {
                client.SendCompleted -= handler;
            } finally {
                if (e.Error != null)
                    tcs.TrySetException(e.Error);
                else if (e.Cancelled)
                    tcs.TrySetCanceled();
                else
                    tcs.TrySetResult((object)null);
            }
        }

    }
}
