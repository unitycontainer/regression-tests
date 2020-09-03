using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
#if NET45
using Microsoft.Practices.Unity;
#else
using Unity.Injection;
using Unity.Lifetime;
using Unity;
#endif

namespace Registrations.Mapping
{
    [TestClass]
    public class Types
    {
        #region Scaffolding

        IUnityContainer Container;
        const string Name = "name";
        const string Other = "other";

        [TestInitialize]
        public void TestInitialize() => Container = new UnityContainer();

        #endregion


        [TestMethod]
        public void Open_Generic_Build()
        {
            // Arrange
            Container.RegisterType(typeof(IService<>), typeof(Service<>));

            // Act
            var value = Container.Resolve<IService<object>>();

            // Validate
            Assert.IsNotNull(value);
            Assert.IsInstanceOfType(value, typeof(Service<object>));
        }

        [TestMethod]
        public void Open_Generic_Build_Named()
        {
            // Arrange
            Container.RegisterType(typeof(IService<>), typeof(Service<>), Name);

            // Act
            var value = Container.Resolve<IService<object>>(Name);

            // Validate
            Assert.IsNotNull(value);
            Assert.IsInstanceOfType(value, typeof(Service<object>));
        }

        [TestMethod]
        public void Map_If_Matching_Registration()
        {
            // Arrange
            var instance = new Service<object>();
            Container.RegisterType(typeof(IService<>), typeof(Service<>))
                     .RegisterInstance(instance);

            // Act
            var value = Container.Resolve<IService<object>>();

            // Validate
            Assert.IsNotNull(value);
            Assert.AreSame(instance, value);
        }

        [TestMethod]
        public void Map_If_Matching_Named_Registration()
        {
            // Arrange
            var instance = new Service<object>();
            Container.RegisterType(typeof(IService<>), typeof(Service<>), Name)
                     .RegisterInstance(instance);

            // Act
            var value = Container.Resolve<IService<object>>(Name);

            // Validate
            Assert.IsNotNull(value);
            Assert.AreNotSame(instance, value);
        }

        [TestMethod]
        public void Map_If_Matching_Named_Instance()
        {
            // Arrange
            var instance = new Service<object>();
            Container.RegisterType(typeof(IService<>), typeof(Service<>), Name)
                     .RegisterInstance(Name, instance);

            // Act
            var value = Container.Resolve<IService<object>>(Name);

            // Validate
            Assert.IsNotNull(value);
            Assert.AreSame(instance, value);
        }

        [TestMethod]
        public void Build_If_No_Matching_Instance()
        {
            // Arrange
            var instance = new Service<object>();
            Container.RegisterType(typeof(IService<>), typeof(Service<>), Name)
                     .RegisterInstance(Other, instance);

            // Act
            var value = Container.Resolve<IService<object>>(Name);

            // Validate
            Assert.IsNotNull(value);
            Assert.AreNotSame(instance, value);
        }


        [TestMethod]
        [ExpectedException(typeof(Exception), AllowDerivedTypes = true)]
        public void Throw_If_No_Matching_Registration()
        {
            // Arrange
            var instance = new Service<object>();
            Container.RegisterType(typeof(IService<>), typeof(Service<>))
                     .RegisterInstance(instance);

            // Act
            _ = Container.Resolve<IService<object>>(Name);
        }
    }
}
