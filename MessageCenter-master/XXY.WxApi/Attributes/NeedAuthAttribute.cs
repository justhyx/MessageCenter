using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using System;
using XXY.WxApi.Handlers;

namespace XXY.WxApi.Attributes {
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class NeedAuthAttribute : HandlerAttribute {

        public override ICallHandler CreateHandler(IUnityContainer container) {
            return new NeedAuthHandler();
        }


    }
}
