using System;
using System.Runtime.CompilerServices;

namespace Microsoft.Practices.Unity
{
    internal static class Compatibility_v_4
    {
        #region Register Factory

        public static IUnityContainer RegisterFactory(this IUnityContainer container, Type type, Func<IUnityContainer, Type, string, object> factory) 
            => container.RegisterType(type, new TransientLifetimeManager(), new InjectionFactory(factory));

        public static IUnityContainer RegisterFactory(this IUnityContainer container, Type type, string name, Func<IUnityContainer, Type, string, object> factory) 
            => container.RegisterType(type, name, new TransientLifetimeManager(), new InjectionFactory(factory));

        public static IUnityContainer RegisterFactory(this IUnityContainer container, Type type, Func<IUnityContainer, Type, string, object> factory, LifetimeManager manager) 
            => container.RegisterType(type, manager, new InjectionFactory(factory));

        public static IUnityContainer RegisterFactory(this IUnityContainer container, Type type, string name, Func<IUnityContainer, Type, string, object> factory, LifetimeManager manager) 
            => container.RegisterType(type, name, manager, new InjectionFactory(factory));

        public static IUnityContainer RegisterFactory(this IUnityContainer container, Type type, Func<IUnityContainer, object> factory) 
            => container.RegisterType(type, new TransientLifetimeManager(), new InjectionFactory(factory));

        public static IUnityContainer RegisterFactory(this IUnityContainer container, Type type, string name, Func<IUnityContainer, object> factory) 
            => container.RegisterType(type, name, new TransientLifetimeManager(), new InjectionFactory(factory));

        public static IUnityContainer RegisterFactory(this IUnityContainer container, Type type, Func<IUnityContainer, object> factory, LifetimeManager manager) 
            => container.RegisterType(type, manager, new InjectionFactory(factory));

        public static IUnityContainer RegisterFactory(this IUnityContainer container, Type type, string name, Func<IUnityContainer, object> factory, LifetimeManager manager) 
            => container.RegisterType(type, name, manager, new InjectionFactory(factory));



        public static IUnityContainer RegisterFactory<T>(this IUnityContainer container, Func<IUnityContainer, Type, string, object> factory)
            => container.RegisterType(typeof(T), new TransientLifetimeManager(), new InjectionFactory(factory));

        public static IUnityContainer RegisterFactory<T>(this IUnityContainer container, string name, Func<IUnityContainer, Type, string, object> factory)
            => container.RegisterType(typeof(T), name, new TransientLifetimeManager(), new InjectionFactory(factory));

        public static IUnityContainer RegisterFactory<T>(this IUnityContainer container, Func<IUnityContainer, Type, string, object> factory, LifetimeManager manager)
            => container.RegisterType(typeof(T), manager, new InjectionFactory(factory));

        public static IUnityContainer RegisterFactory<T>(this IUnityContainer container, string name, Func<IUnityContainer, Type, string, object> factory, LifetimeManager manager)
            => container.RegisterType(typeof(T), name, manager, new InjectionFactory(factory));

        public static IUnityContainer RegisterFactory<T>(this IUnityContainer container, Func<IUnityContainer, object> factory)
            => container.RegisterType(typeof(T), new TransientLifetimeManager(), new InjectionFactory(factory));

        public static IUnityContainer RegisterFactory<T>(this IUnityContainer container, string name, Func<IUnityContainer, object> factory)
            => container.RegisterType(typeof(T), name, new TransientLifetimeManager(), new InjectionFactory(factory));

        public static IUnityContainer RegisterFactory<T>(this IUnityContainer container, Func<IUnityContainer, object> factory, LifetimeManager manager)
            => container.RegisterType(typeof(T), manager, new InjectionFactory(factory));

        public static IUnityContainer RegisterFactory<T>(this IUnityContainer container, string name, Func<IUnityContainer, object> factory, LifetimeManager manager)
            => container.RegisterType(typeof(T), name, manager, new InjectionFactory(factory));

        #endregion


        #region Singleton

        public static IUnityContainer RegisterSingleton<T>(this IUnityContainer container, params InjectionMember[] injectionMembers)
        {
            return (container ?? throw new ArgumentNullException(nameof(container)))
                .RegisterType(null, typeof(T), null, new ContainerControlledLifetimeManager(), injectionMembers);
        }

        public static IUnityContainer RegisterSingleton<T>(this IUnityContainer container, string name, params InjectionMember[] injectionMembers)
        {
            return (container ?? throw new ArgumentNullException(nameof(container)))
                .RegisterType(null, typeof(T), name, new ContainerControlledLifetimeManager(), injectionMembers);
        }

        public static IUnityContainer RegisterSingleton<TFrom, TTo>(this IUnityContainer container, params InjectionMember[] injectionMembers)
            where TTo : TFrom
        {
            return (container ?? throw new ArgumentNullException(nameof(container)))
                .RegisterType(typeof(TFrom), typeof(TTo), null, new ContainerControlledLifetimeManager(), injectionMembers);
        }

        public static IUnityContainer RegisterSingleton<TFrom, TTo>(this IUnityContainer container, string name, params InjectionMember[] injectionMembers)
            where TTo : TFrom
        {
            return (container ?? throw new ArgumentNullException(nameof(container)))
                .RegisterType(typeof(TFrom), typeof(TTo), name, new ContainerControlledLifetimeManager(), injectionMembers);
        }

        public static IUnityContainer RegisterSingleton(this IUnityContainer container, Type type, params InjectionMember[] injectionMembers)
        {
            return (container ?? throw new ArgumentNullException(nameof(container)))
                .RegisterType(null, type, null, new ContainerControlledLifetimeManager(), injectionMembers);
        }

        public static IUnityContainer RegisterSingleton(this IUnityContainer container, Type type, string name, params InjectionMember[] injectionMembers)
        {
            return (container ?? throw new ArgumentNullException(nameof(container)))
                .RegisterType(null, type, name, new ContainerControlledLifetimeManager(), injectionMembers);
        }

        public static IUnityContainer RegisterSingleton(this IUnityContainer container, Type from, Type to, params InjectionMember[] injectionMembers)
        {
            return (container ?? throw new ArgumentNullException(nameof(container)))
                .RegisterType(from, to, null, new ContainerControlledLifetimeManager(), injectionMembers);
        }

        public static IUnityContainer RegisterSingleton(this IUnityContainer container, Type from, Type to, string name, params InjectionMember[] injectionMembers)
        {
            return (container ?? throw new ArgumentNullException(nameof(container)))
                .RegisterType(from, to, name, new ContainerControlledLifetimeManager(), injectionMembers);
        }
        
        #endregion
    }

    public static partial class Inject
    {
        #region Array

        public static ParameterBase Array(Type elementType, params object[] elementValues)
            => new ResolvedArrayParameter(elementType, elementValues);

        public static ParameterBase Array<TElement>(params object[] elementValues)
            => new ResolvedArrayParameter(typeof(TElement), elementValues);

        #endregion


        #region Parameter

        public static ParameterBase Parameter(object value) => new InjectionParameter(value);

        public static ParameterBase Parameter(Type type, object value)
            => new InjectionParameter(type ?? throw new ArgumentNullException(nameof(type)), value);
        public static ParameterBase Parameter<TTarget>(object value) => new InjectionParameter(typeof(TTarget), value);

        #endregion


        #region Field

        public static InjectionMember Field(string name, object value)
            => new InjectionField(name ?? throw new ArgumentNullException(nameof(name)), value);

        #endregion


        #region Property

        public static InjectionMember Property(string name, object value)
            => new InjectionProperty(name ?? throw new ArgumentNullException(nameof(name)), value);

        #endregion
    }

}