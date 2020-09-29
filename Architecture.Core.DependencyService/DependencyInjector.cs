using System;
using System.Collections.Generic;

namespace Architecture.Core.DependencyService
{
    public enum ServiceLifetime { Singleton, Scoped, Transient }

    public delegate object Factory(Type Interfacetype, Type Concretetype);

    public class ServiceDescriptor
    {
        public Type ServiceType { get; set; }
        public Type ImplementationType { get; set; }
        public Factory Factory { get; set; }
        public object Implementation { get; set; }
        public ServiceLifetime Lifetime { get; set; }
    }

    //IServiceCollection keeps me from fully implementing a stand-alone service-provider,
    //as all made applications have services registered, prior
    //to having the chance creating my own DepInjector
    //ergo need constructor with IServiceCollection == hard relation with MS.Ext.DepInj.ServiceDescriptor
    //even worse; we need a different ImplementationFactory type to be compatible
    //IGNORE DepInjector for now
    [System.Obsolete]
    public class DependencyInjector : IServiceProvider
    {
        //services - DI Container
        private Dictionary<Type, ServiceDescriptor> _services = new Dictionary<Type, ServiceDescriptor>();

        public DependencyInjector() { }
        public bool RegisterTransient<T, U>(Factory factory)
        {
            _services.Add(typeof(T), new ServiceDescriptor()
            {
                ServiceType = typeof(T),
                ImplementationType = typeof(U),
                Factory = factory,
                Lifetime = ServiceLifetime.Transient
            });
            return true;
        }

        public bool RegisterScoped<T>() where T : new()
        {
            return RegisterScoped(new T());
        }

        public bool RegisterScoped(object implementation)
        {
            _services.Add(implementation.GetType(), new ServiceDescriptor()
            {
                ServiceType = implementation.GetType(),
                ImplementationType = implementation.GetType(),
                Implementation = implementation,
                Lifetime = ServiceLifetime.Scoped
            });
            return true;
        }
        public bool RegisterSingleton<T>() where T : new()
        {
            return RegisterSingleton(new T());
        }
        public bool RegisterSingleton(object implementation)
        {
            _services.Add(implementation.GetType(), new ServiceDescriptor()
            {
                ServiceType = implementation.GetType(),
                ImplementationType = implementation.GetType(),
                Implementation = implementation,
                Lifetime = ServiceLifetime.Singleton
            });
            return true;
        }

        public object GetService(Type serviceType)
        {
            ServiceDescriptor descr;
            _services.TryGetValue(serviceType, out descr);

            if (descr == null)
                throw new Exception($"Service of type {serviceType.Name} isn't registered");

            if (descr.Implementation != null)
                return descr.Implementation;

            var actualType = descr.ImplementationType ?? descr.ServiceType;

            if (actualType.IsAbstract || actualType.IsInterface)
                throw new Exception("Cannot instantiate abstract classes or interfaces");

            var constructorInfo = actualType.GetConstructors()[0];

            List<object> l = new List<object>();
            foreach (var param in constructorInfo.GetParameters())
            {
                l.Add(GetService(param.ParameterType));
            }
            var implementation = Activator.CreateInstance(actualType, l.ToArray());

            if (descr.Lifetime == ServiceLifetime.Singleton ||
                descr.Lifetime == ServiceLifetime.Scoped)
            {
                descr.Implementation = implementation;
            }

            return implementation;
        }

        public T GetService<T>()
        {
            return (T)GetService(typeof(T));
        }

    }
}