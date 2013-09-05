using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace Core.Ioc
{
    public class Container
    {
        public const string CONTAINER = "container";
        
        private IUnityContainer uContainer;
        
        private string name = CONTAINER;

        public Container()
        {
            uContainer = new UnityContainer();

            uContainer.AddNewExtension<Interception>();

            uContainer.RegisterInstance(typeof (Container), this);
        }

        public Container(IUnityContainer unityContainer, string name)
        {
            this.uContainer = unityContainer;
            this.name = name;
        }

        public void Dispose()
        {
            uContainer.Dispose();
            uContainer = null;
        }

        public string Name
        {
            get { return name; }
        }

        public void RegisterInstance(Type type, object obj)
        {
            uContainer.RegisterInstance(type, obj);
        }

        public void RegisterInstance(Type type, string name, object obj)
        {
            uContainer.RegisterInstance(type, name, obj);
        }

        public void RegisterType(Type interfaceClass, Type implementationClass)
        {
            uContainer.RegisterType(interfaceClass, implementationClass);
        }

        public void RegisterType(Type interfaceClass, Type implementationClass, string name)
        {
            uContainer.RegisterType(interfaceClass, implementationClass, name);
        }

        public void RegisterType(Type interfaceClass, Type implementationClass, LifetimeManager lifetimeManager)
        {
            uContainer.RegisterType(interfaceClass, implementationClass, lifetimeManager);
        }

        public void RegisterType(Type interfaceClass, Type implementationClass, string name, LifetimeManager lifetimeManager)
        {
            uContainer.RegisterType(interfaceClass, implementationClass, name, lifetimeManager);
        }

        public void RegisterType(Type type, LifetimeManager lifetimeManager)
        {
            uContainer.RegisterType(type, lifetimeManager);
        }

        public void AddInterceptor(Type type)
        {
            uContainer.Configure<Interception>().SetInterceptorFor(type, new VirtualMethodInterceptor());
        }

        public void AddInterceptor(Type type, string name)
        {
            uContainer.Configure<Interception>().SetInterceptorFor(type, name, new VirtualMethodInterceptor());
        }

        public void AddInterceptor<T>()
        {
            uContainer.Configure<Interception>().SetInterceptorFor<T>(new VirtualMethodInterceptor());
        }

        public void AddInterceptor<T>(string name)
        {
            uContainer.Configure<Interception>().SetInterceptorFor<T>(name, new VirtualMethodInterceptor());
        }

        public T GetInstance<T>()
        {
            return uContainer.Resolve<T>();
        }

        public T GetInstance<T>(string name)
        {
            return uContainer.Resolve<T>(name);
        }

        public object GetInstance(Type type)
        {
            return uContainer.Resolve(type);
        }

        public List<T> GetInstances<T>()
        {
            return new List<T>(uContainer.ResolveAll<T>());
        }

        public void RegisterLifeCycle(params string[] assemblyNames)
        {
            foreach (string assemblyName in assemblyNames)
            {
                Assembly assembly = Assembly.Load(assemblyName);
                foreach (Type type in assembly.GetTypes())
                {
                    if (type.IsClass && type.GetCustomAttributes(typeof(ServiceAttribute), true).Length > 0)
                    {
                        uContainer.RegisterType(type, new ContainerControlledLifetimeManager());
                    }
                    else if(type.IsClass && type.GetCustomAttributes(typeof(DataAccessAttribute), true).Length > 0)
                    {
                        uContainer.RegisterType(type, new ContainerControlledLifetimeManager());
                    }
                }
            }
        }

        public void BuildUp(object obj)
        {
            uContainer.BuildUp(obj.GetType(), obj);
        }

        public void BuildUp(object obj, Type type)
        {
            uContainer.BuildUp(type, obj);
        }

        public void EnableGlobalStopWatch(params string[] assemblies)
        {
            //uContainer.AddNewExtension<GlobalInterceptExtension>();
        }

        public Container CreateChildContainer(string name)
        {
            IUnityContainer childContainer = uContainer.CreateChildContainer();
            
            childContainer.AddNewExtension<Interception>();
            
            return new Container(childContainer, name);
        }

        public void AddInterceptor(string policy, IMatchingRule matchingRule, params Type[] classTypes)
        {
            Interception interception = uContainer.Configure<Interception>();
            foreach (Type classType in classTypes)
            {
                interception = interception.SetInterceptorFor(classType, new VirtualMethodInterceptor());
            }
            interception.AddPolicy(policy)
                .AddMatchingRule(matchingRule);
        }
    }
}
