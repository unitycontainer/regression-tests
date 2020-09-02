using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
#if NET45
using Microsoft.Practices.Unity;
#else
using Unity.Injection;
using Unity.Lifetime;
using Unity;
#endif

namespace Generics
{
    [TestClass]
    public class Basics
    {
        #region Scaffolding

        IUnityContainer Container;
        const string Name = "name";
        const string Other = "other";

        [TestInitialize]
        public void TestInitialize() => Container = new UnityContainer()
            .RegisterType(typeof(IService<>), typeof(Service<>))
            .RegisterType(typeof(IService<>), typeof(OtherService<>), Name)
            .RegisterType(typeof(IFoo<>),     typeof(Foo<>), Other);

        #endregion

        /// <summary>
        /// Resolving solid type from generic registrations
        /// </summary>
        /// <remarks>
        /// There are anonymous and matching named registrations
        /// </remarks>
        [TestMethod]
        public void Constructable()
        {
            // Act
            var anonymous = Container.Resolve<IService<int>>();
            var named     = Container.Resolve<IService<int>>(Name);
            var other     = Container.Resolve<IFoo<int>>(Other);

            // Validate
            Assert.IsInstanceOfType(anonymous, typeof(Service<int>));
            Assert.IsInstanceOfType(named,     typeof(OtherService<int>));
            Assert.IsInstanceOfType(other,     typeof(Foo<int>));
        }

        /// <summary>
        /// Attempting to resolve anonymous contract 
        /// </summary>
        /// <remarks>
        /// There is a named <see cref="Other"/> registration
        /// </remarks>
        [TestMethod]
        [ExpectedException(typeof(Exception), AllowDerivedTypes = true)]
        public void No_Default()
        {
            // Act
            _ = Container.Resolve<IFoo<int>>();
        }

        /// <summary>
        /// Attempting to resolve a contract with wrong name
        /// </summary>
        /// <remarks>
        /// There are anonymous and named <see cref="Name"/> registrations
        /// </remarks>
        [TestMethod]
        [ExpectedException(typeof(Exception), AllowDerivedTypes = true)]
        public void No_Name()
        {
            // Act
            _ = Container.Resolve<IService<int>>(Other);
        }

    }

    #region Test Data


    public interface IFoo<T>
    { }

    public class Foo<T> : IFoo<T>
    { }

    public interface IService<T>
    { }

    public class Service<T> : IService<T>
    { }

    public class OtherService<T> : IService<T>
    { }

    #endregion

}
