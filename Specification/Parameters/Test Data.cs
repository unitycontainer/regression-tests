using System;
#if NET45
using Microsoft.Practices.Unity;
#else
using Unity;
using Unity.Resolution;
#endif

namespace Specification
{
    public partial class Parameters
    {
        #region Test Data

        public class GenericService<T1, T2, T3>
        {
            public object Value { get; private set; }

            public int Called { get; private set; }

            public void Method(T1 value)
            {
                Value = value;
                Called = 1;
            }

            public void Method(T2 value)
            {
                Value = value;
                Called = 2;
            }

            public void Method(T3 value)
            {
                Value = value;
                Called = 3;
            }
        }

        // Our generic interface 
        public interface ICommand<T>
        {
            T Executed { get; }

            ICommand<T> Chained { get; }

            void Execute(T data);

            void ChainedExecute(ICommand<T> inner);
        }

        // An implementation of ICommand that executes them.
        public class ConcreteCommand<T> : ICommand<T>
        {
            public T Executed { get; private set; }

            public ICommand<T> Chained { get; private set; }

            public void Execute(T data)
            {
                Executed = data;
            }

            public void ChainedExecute(ICommand<T> inner)
            {
                Chained = inner;
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

            public T Executed { get; private set; }

            public ICommand<T> Chained { get; private set; }


            public ICommand<T> Inner
            {
                get { return inner; }
                set { inner = value; }
            }

            public void Execute(T data)
            {
                // do logging here
                Inner.Execute(data);
                Executed = data;
            }

            public void ChainedExecute(ICommand<T> innerCommand)
            {
                ChainedExecuteWasCalled = true;
                Chained = innerCommand;
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

        #endregion
        #region Test Data

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

        public class OtherService
        {
            #region Properties

            public object Value { get; private set; }

            public object ValueOne { get; private set; }

            public object ValueTwo { get; private set; }

            #endregion

            [InjectionMethod]
            public void Method(object value)
            {
                Value = value;
            }

            public void MethodOne(object value)
            {
                ValueOne = value;
            }

            public void MethodTwo(object one, object two)
            {
                ValueOne = one;
                ValueTwo = two;
            }
        }

        public class Service
        {
            public const string DefaultString = "default";
            public const int DefaultInt = 111;

            #region Properties

            public int Called { get; private set; } = -1;

            public object Value { get; private set; }

            public object ValueOne { get; private set; }

            public object ValueTwo { get; private set; }

            #endregion

            [InjectionMethod]
            public void Method(object value)
            {
                ValueOne = value;
            }

            public void MethodOne(string value)
            {
                ValueOne = value;
            }

            public void MethodTwo(object one, object two)
            {
                ValueOne = one;
                ValueTwo = two;
            }

            public void NoParameters() => Called = 0;

            public void NoAttributeParameter(object value)
            {
                Value = value;
                Called = 1;
            }

            public void DependencyAttribute([Dependency] object value)
            {
                Value = value;
                Called = 2;
            }

            public void NamedDependencyAttribute([Dependency(Name)] string value)
            {
                Value = value;
                Called = 3;
            }

            public void OptionalDependencyAttribute([OptionalDependency] object value)
            {
                Value = value;
                Called = 4;
            }

            public void OptionalNamedDependencyAttribute([OptionalDependency(Name)] string value)
            {
                Value = value;
                Called = 5;
            }

            public void OptionalDependencyAttributeMissing([OptionalDependency] IDisposable value)
            {
                Value = value;
                Called = 6;
            }

            public void OptionalNamedDependencyAttributeMissing([OptionalDependency(Name)] IDisposable value)
            {
                Value = value;
                Called = 7;
            }

            public void NoAttributeWithDefault(string value = DefaultString)
            {
                Value = value;
                Called = 8;
            }

            public void NoAttributeWithDefaultInt(int value = DefaultInt)
            {
                Value = value;
                Called = 9;
            }

            public void DependencyAttributeWithDefaultInt([Dependency] int value = DefaultInt)
            {
                Value = value;
                Called = 10;
            }

            public void NamedDependencyAttributeWithDefaultInt([Dependency(Name)] int value = DefaultInt)
            {
                Value = value;
                Called = 11;
            }

            public void OptionalDependencyAttributeWithDefaultInt([OptionalDependency] int value = DefaultInt)
            {
                Value = value;
                Called = 12;
            }

            public void OptionalNamedDependencyAttributeWithDefaultInt([OptionalDependency(Name)] int value = DefaultInt)
            {
                Value = value;
                Called = 13;
            }

            public void NoAttributeWithDefaultUnresolved(long value = 100)
            {
                Value = value;
                Called = 14;
            }

            public void WithDefaultDisposableUnresolved(IDisposable value = null)
            {
                Value = value;
                Called = 15;
            }

            public void DependencyAttributeWithDefaultNullUnresolved([Dependency] IDisposable value = null)
            {
                Value = value;
                Called = 16;
            }
        }

        public interface IService { }

        public class Service1 : IService { }

        public class Service2 : IService { }

        public interface IFoo { }

        public class Foo : IFoo
        {
            public object Fred { get; }

            public object George { get; }

            public Foo([OptionalDependency("Fred")] IService x,
                       [OptionalDependency("George")] IService y)
            {
                Fred = x;
                George = y;
            }
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

        public class TestType
        {
            private readonly string _dependency;

            public TestType() { }

            public TestType(string dependency)
            {
                _dependency = dependency;
            }

            public string DependencyField;
            public string DependencyProperty { get; set; }

            public override string ToString() => _dependency;
        }

        public interface IController
        {
            string GetMessage();
        }

        public class TheController : IController
        {
            private readonly IMessageProvider _messageProvider;

            public TheController(IMessageProvider messageProvider)
            {
                _messageProvider = messageProvider;
            }

            public string GetMessage()
            {
                return _messageProvider.CalculateMessage();
            }
        }

        public interface IMessageProvider
        {
            string CalculateMessage();
        }

        public class DefaultMessageProvider : IMessageProvider
        {
            public string CalculateMessage()
            {
                return "Hello World";
            }
        }

        public class AlternativeMessageProvider : IMessageProvider
        {
            public string CalculateMessage()
            {
                return "Goodbye cruel world!";
            }
        }


        #endregion
    }
}
