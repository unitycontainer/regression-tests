﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;
#if V4
using Microsoft.Practices.Unity;
#else
using Unity;
#endif

namespace Resolution
{
    [TestClass]
    public partial class Arrays
    {
        public const string Legacy = "legacy";
        protected const string Name = "name";
        protected IUnityContainer Container;

        [TestInitialize]
        public virtual void TestInitialize() => Container = new UnityContainer();


        #region Test Data

    public class EmptyClass
    {
    }

    public class InjectedObject
    {
        public readonly object InjectedValue;

        public InjectedObject(object injectedValue)
        {
            this.InjectedValue = injectedValue;
        }
    }

    public class GenericTypeWithArrayProperty<T>
    {
        public T[] Prop { get; set; }
    }


    public class ClassWithOneArrayGenericParameter<T>
    {
        private T[] injectedValue;
        public readonly bool DefaultConstructorCalled;

        public ClassWithOneArrayGenericParameter()
        {
            DefaultConstructorCalled = true;
        }

        public ClassWithOneArrayGenericParameter(T[] injectedValue)
        {
            DefaultConstructorCalled = false;

            this.injectedValue = injectedValue;
        }

        public T[] InjectedValue
        {
            get { return this.injectedValue; }
            set { this.injectedValue = value; }
        }
    }

    public interface ILogger
    {
    }

    public class SpecialLogger : ILogger
    {
    }

    public class MockLogger : ILogger
    {
    }

    public class TypeWithArrayConstructorParameter
    {
        public readonly ILogger[] Loggers;

        public TypeWithArrayConstructorParameter(ILogger[] loggers)
        {
            Loggers = loggers;
        }
    }

    public class GenericTypeWithArrayConstructorParameter<T>
    {
        public readonly T[] Values;

        public GenericTypeWithArrayConstructorParameter(T[] values)
        {
            Values = values;
        }
    }

    public class TypeWithArrayProperty
    {
        [Dependency]
        public ILogger[] Loggers { get; set; }
    }

    public class TypeWithArrayConstructorParameterOfRankTwo
    {
        private readonly ILogger[,] _unknown;

        public TypeWithArrayConstructorParameterOfRankTwo(ILogger[,] array)
        {
            _unknown = array;
        }
    }

    public interface IFoo<TEntity>
    {
        TEntity Value { get; }
    }

    public class Foo<TEntity> : IFoo<TEntity>
    {
        //public Foo()
        //{
        //}

        public Foo(TEntity value)
        {
            Value = value;
        }

        public TEntity Value { get; }
    }

    public interface IService
    {
    }

    public interface IOtherService
    {
    }

    public class Service : IService, IDisposable
    {
        public string Id { get; } = Guid.NewGuid().ToString();

        public static int Instances;

        public Service()
        {
            Interlocked.Increment(ref Instances);
        }

        public bool Disposed;
        public void Dispose()
        {
            Disposed = true;
        }
    }

    public class OtherService : IService, IOtherService, IDisposable
    {
        [InjectionConstructor]
        public OtherService()
        {

        }

        public OtherService(IUnityContainer container)
        {

        }


        public bool Disposed = false;
        public void Dispose()
        {
            Disposed = true;
        }
    }

    #endregion
    }
}
