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
    public partial class Mapping
    {
        protected const string Name = "name";
        public const string Legacy = "legacy";
        protected IUnityContainer Container;

        [TestInitialize]
        public virtual void TestInitialize() => Container = new UnityContainer();

        #region Test Data

        public interface IFoo<TEntity>
        {
            TEntity Value { get; }
        }

        public interface IFoo { }
        public interface IFoo1 { }
        public interface IFoo2 { }

        public class Foo<TEntity> : IFoo<TEntity>
        {
            public Foo(TEntity value)
            {
                Value = value;
            }

            public TEntity Value { get; }
        }

        public class Foo : IFoo, IFoo1, IFoo2
        {
        }

        public interface IService
        {
        }

        public interface IGenericService<T>
        {
        }

        public interface ITest1<T> { }

        public interface ITest2<T> { }

        public class Test<T> : ITest1<T>, ITest2<T>
        {
            public string Id { get; } = Guid.NewGuid().ToString();
        }

        public class Service : IService, IDisposable
        {
            public string ID { get; } = Guid.NewGuid().ToString();

            public static int Instances = 0;

            public Service()
            {
                Interlocked.Increment(ref Instances);
            }

            public bool Disposed = false;
            public void Dispose()
            {
                Disposed = true;
            }
        }

        public interface IOtherService
        {
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
