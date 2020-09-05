using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
#if NET45
using Microsoft.Practices.Unity;
#else
using Unity;
using Unity.Resolution;
#endif

namespace Specification
{
    [TestClass]
    public partial class Properties
    {
        protected const string Name = "name";
        protected const string Other = "other";
        protected IUnityContainer Container;

        [TestInitialize]
        public virtual void TestInitialize()
        {
            Container = new UnityContainer();
            Container.RegisterInstance(Name);
            Container.RegisterInstance(Name, Name);
            Container.RegisterInstance(Name, Other);
            Container.RegisterType<ObjectWithProperty>(
                    Invoke.Constructor(),
                    Resolve.Property(nameof(ObjectWithProperty.MyProperty)))
                .RegisterType<ISomething, Something1>()
                .RegisterType<ISomething, Something2>(Name)
                .RegisterInstance(Name);
        }

        #region Test Data

        public class ValidatingResolverFactory : IResolverFactory<Type>
        {
            private object _value;

            public ValidatingResolverFactory(object value)
            {
                _value = value;
            }

            public Type Type { get; private set; }
            public string Name { get; private set; }

            public ResolveDelegate<TContext> GetResolver<TContext>(Type info)
                where TContext : IResolveContext
            {
                return (ref TContext context) =>
                {
                    Type = context.Type;
                    Name = context.Name;

                    return _value;
                };
            }
        }

        public class ValidatingResolver : IResolve
        {
            private object _value;

            public ValidatingResolver(object value)
            {
                _value = value;
            }

            public object Resolve<TContext>(ref TContext context) where TContext : IResolveContext
            {
                Type = context.Type;
                Name = context.Name;

                return _value;
            }

            public Type Type { get; private set; }

            public string Name { get; private set; }
        }

        public class ObjectWithHiddenProperties
        {
            private string PrivateProperty { get; set; }

            public string ReadonlyProperty { get; }
        }

        public class ObjectWithThreeProperties : ObjectWithHiddenProperties
        {
            [Dependency]
            public string Name { get; set; }

            public object Property { get; set; }

            [Dependency]
            public IUnityContainer Container { get; set; }
        }

        public class ObjectWithNamedDependency
        {
            [Dependency(Name)]
            public string Property { get; set; }

            [OptionalDependency]
            public IUnityContainer Container { get; set; }
        }

        public class ObjectWithFourProperties : ObjectWithThreeProperties
        {
            public object SubProperty { get; set; }

            public object ReadOnlyProperty { get; }
        }

        public class ObjectWithDependency
        {
            public ObjectWithDependency(ObjectWithThreeProperties obj)
            {
                Dependency = obj;
            }

            public ObjectWithThreeProperties Dependency { get; }

        }

        // Our generic interface 
        public interface ICommand<T>
        {
            void Execute(T data);
            void ChainedExecute(ICommand<T> inner);
        }

        // An implementation of ICommand that executes them.
        public class ConcreteCommand<T> : ICommand<T>
        {
            private object p = null;

            public void Execute(T data)
            {
            }

            public void ChainedExecute(ICommand<T> inner)
            {
            }

            public object NonGenericProperty
            {
                get { return p; }
                set { p = value; }
            }
        }

        // And a decorator implementation that wraps an Inner ICommand<>
        public class LoggingCommand<T> : ICommand<T>
        {
            private ICommand<T> inner;

            public bool ChainedExecuteWasCalled = false;
            public bool WasInjected = false;

            public LoggingCommand(ICommand<T> inner)
            {
                this.inner = inner;
            }

            public LoggingCommand()
            {
            }

            public ICommand<T> Inner
            {
                get { return inner; }
                set { inner = value; }
            }

            public void Execute(T data)
            {
                // do logging here
                Inner.Execute(data);
            }

            public void ChainedExecute(ICommand<T> innerCommand)
            {
                ChainedExecuteWasCalled = true;
            }

            public void InjectMe()
            {
                WasInjected = true;
            }
        }


        // A couple of sample objects we're stuffing into our commands
        public class User
        {
            public void DoSomething(string message)
            {
            }
        }

        public class Account
        {
        }

        // InjectionParameterValue type used for testing nesting
        public struct Customer
        {
        }

        public class NoAttributeType
        {
            public object Value { get; set; }
            public int Called = 1;
        }

        public class DependencyAttributeType
        {
            [Dependency]
            public object Value { get; set; }
            public int Called = 2;
        }

        public class NamedDependencyAttributeType
        {
            [Dependency(Name)]
            public string Value { get; set; }
            public int Called = 3;
        }

        public class OptionalDependencyAttributeType
        {
            [OptionalDependency]
            public object Value { get; set; }
            public int Called = 4;
        }

        public class OptionalNamedDependencyAttributeType
        {
            [OptionalDependency(Name)]
            public string Value { get; set; }
            public int Called = 5;
        }

        public class OptionalDependencyAttributeMissingType
        {
            [OptionalDependency]
            public IDisposable Value { get; set; }
            public int Called = 6;
        }

        public class OptionalNamedDependencyAttributeMissingType
        {
            [OptionalDependency(Name)]
            public IDisposable Value { get; set; }
            public int Called = 7;
        }

#pragma warning disable 649
        public class DependencyAttributePrivateType
        {
            [Dependency]
            private object Dependency { get; set; }

            public object Value => Dependency;
            public int Called = 8;
        }
#pragma warning restore 649

        public class DependencyAttributeProtectedType
        {
            [Dependency]
            protected object Dependency { get; set; }

            public object Value => Dependency;
            public int Called = 9;
        }

        public class ObjectWithNamedDependencyProperties
        {
            [Dependency(Name)]
            public string Property { get; set; }

            public IUnityContainer Container { get; set; }
        }

        public class SimpleTestObject
        {
            public SimpleTestObject()
            {
            }

            [InjectionConstructor]
            public SimpleTestObject(int x)
            {
                X = x;
            }

            public int X { get; private set; }
        }

        public class ObjectThatDependsOnSimpleObject
        {
            public SimpleTestObject TestObject { get; set; }

            public ObjectThatDependsOnSimpleObject(SimpleTestObject testObject)
            {
                TestObject = testObject;
            }

            public SimpleTestObject OtherTestObject { get; set; }
        }

        public interface ISomething { }
        public class Something1 : ISomething { }
        public class Something2 : ISomething { }

        public class ObjectWithProperty
        {
            public ISomething MyProperty { get; set; }

            public ObjectWithProperty()
            {
            }

            public ObjectWithProperty(ISomething property)
            {
                MyProperty = property;
            }
        }

        public class Outer
        {
            public Outer(Inner inner, int logLevel)
            {
                this.Inner = inner;
                this.LogLevel = logLevel;
            }

            public int LogLevel { get; private set; }
            public Inner Inner { get; private set; }
        }

        public class Inner
        {
            public Inner(int logLevel, string label)
            {
                this.LogLevel = logLevel;
            }

            public int LogLevel { get; private set; }
        }

        #endregion
    }

    [TestClass]
    public partial class Properties_Diagnostic : Properties
    {
        [TestInitialize]
#if NET45
        public override void TestInitialize() => Container = new UnityContainer();
#else
        public override void TestInitialize() => Container = new UnityContainer()
            .AddExtension(new Unity.Diagnostic());
#endif
        #region Test Data

        public class DependencyInjectedTypeBase
        {
            private object PrivateProperty { get; set; }

            protected object ProtectedProperty { get; set; }

            public object ReadonlyProperty { get; }

            public static object StaticProperty { get; set; }

            public object this[int i]
            {
                get { return new object(); }
                set { }
            }
        }

        public class DependencyInjectedType : DependencyInjectedTypeBase
        {
            public object NormalProperty { get; set; }
        }

        public class DependencyAttributeStaticType
        {
            [Dependency]
            public static object Dependency { get; set; }
        }

        public class OptionalDependencyAttributeStaticType
        {
            [OptionalDependency]
            public static object Dependency { get; set; }
        }

        public class DependencyAttributeIndexType
        {
            [Dependency]
            public object this[int i]
            {
                get { return new object(); }
                set { }
            }
        }

        public class OptionalDependencyAttributeIndexType
        {
            [OptionalDependency]
            public object this[int i]
            {
                get { return new object(); }
                set { }
            }
        }

        public class DependencyAttributeReadOnlyType
        {
            [Dependency]
            public object Dependency { get; }
        }

        public class OptionalDependencyAttributeReadOnlyType
        {
            [OptionalDependency]
            public object Dependency { get; }
        }

#pragma warning disable 649

        public class OptionalDependencyAttributePrivateType
        {
            [OptionalDependency]
            private object Dependency { get; set; }
        }

#pragma warning restore 649

        public class OptionalDependencyAttributeProtectedType
        {
            [OptionalDependency]
            protected object Dependency { get; set; }
        }

        #endregion
    }
}
