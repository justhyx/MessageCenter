using Microsoft.Practices.Unity;
using System;
using Microsoft.Practices.Unity.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Practices.ObjectBuilder2;
using XXY.Common.Attributes;
using XXY.MessageCenter.Biz;
using XXY.MessageCenter.ServiceImpl;


namespace XXY.MessageCenter {
    public static class UnityConfig {

        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() => {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });


        public static IUnityContainer GetConfiguredContainer() {
            return container.Value;
        }


        public static void RegisterTypes(IUnityContainer container) {
            container.LoadConfiguration();

            //对全局异常处理程序自注入
            //container.RegisterType<ExceptionLogAttribute, ExceptionLogAttribute>();

            var conv = new Convention(container,
                typeof(BaseBiz).Assembly,
                typeof(TemplateServiceImpl).Assembly,
                Assembly.GetExecutingAssembly()
                );

            container.RegisterTypes(conv);
        }

        public class Convention : RegistrationConvention {

            private readonly IUnityContainer container;
            private readonly IEnumerable<Type> types;

            public Convention(IUnityContainer unity, params Assembly[] assemblies)
                : this(unity, assemblies.SelectMany(a => a.GetExportedTypes()).ToArray()) {
                this.container = unity;
            }

            public Convention(IUnityContainer unity, params Type[] types) {
                this.container = unity;
                this.types = types ?? Enumerable.Empty<Type>();
            }

            public override Func<Type, IEnumerable<Type>> GetFromTypes() {
                return t => {
                    var ifs = t.GetInterfaces();
                    //多个接口可以对应同一个实现
                    var ais = t.GetCustomAttributes<AutoInjectionAttribute>()
                                .Select(a => a.Interface);
                    var results = new List<Type>();
                    foreach (var i in ais) {
                        if (ifs.Contains(i))
                            results.Add(i);
                    }
                    return results;
                };
            }

            public override Func<Type, IEnumerable<InjectionMember>> GetInjectionMembers() {
                return (x => Enumerable.Empty<InjectionMember>());
            }

            public override Func<Type, LifetimeManager> GetLifetimeManager() {
                var bt = typeof(BaseBiz);
                return t => {
                    if (t.IsSubclassOf(bt)) {
                        //return WithLifetime.Transient(t);
                        return WithLifetime.PerResolve(t);
                    }
                    return WithLifetime.ContainerControlled(t);
                };
            }

            public override Func<Type, String> GetName() {
                return t => WithName.Default(t);
            }

            public override IEnumerable<Type> GetTypes() {
                var types = this.types.Where(t =>
                    t.IsPublic &&
                    t.GetInterfaces().Count() > 0 &&
                    !t.IsAbstract &&
                    t.IsClass &&
                    t.GetCustomAttributes<AutoInjectionAttribute>(false).Count() > 0);

                return types;
            }
        }
    }
}