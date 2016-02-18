using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;
using System.ComponentModel.Composition;
using XXY.MessageCenter.Common;
using XXY.MessageCenter.DbEntity;
using System.Configuration;
using System.Reflection;


namespace XXY.MessageCenter.Service {

    class Program {

        private static readonly List<Type> SupportDataTypes = new List<Type>() {
            typeof(EMailMessage),
            typeof(SMSMessage),
            typeof(QQMessage),
            typeof(WeChatMessage)
        };

        private static string QueuePath = ConfigurationManager.AppSettings.Get("MSMQPath");
        private static string ProcessedQueuePath = ConfigurationManager.AppSettings.Get("ProcessedMSMQPath");

        static void Main(string[] args) {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            HostFactory.Run((x) => {
                x.StartAutomaticallyDelayed();
                x.RunAsLocalSystem();
                x.SetDescription("XXY Message Center Service");
                x.SetDisplayName("XXY.MessageCenter");
                x.SetInstanceName("XXY.MessageCenter");
                x.SetServiceName("XXY.MessageCenter");

                x.Service(s => {
                    var server = new Server(QueuePath, ProcessedQueuePath , SupportDataTypes);
                    try {
                        var catalog = new DirectoryCatalog(AppDomain.CurrentDomain.BaseDirectory);
                        var container = new CompositionContainer(catalog);
                        container.ComposeParts(server);
                    } catch (ReflectionTypeLoadException ex) {
                        Console.WriteLine(ex.Message);
                    } catch (Exception ex1) {
                        Console.WriteLine(ex1.Message);
                    }
                    finally
                    {
                        Console.ReadLine();
                    }
                    return server;
                });
            });
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e) {
            var ex = (Exception)e.ExceptionObject;
        }

    }
}
