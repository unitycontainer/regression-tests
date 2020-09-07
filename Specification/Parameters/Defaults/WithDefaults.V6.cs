using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Unity;
using Unity.Injection;

namespace Specification
{
    public partial class Parameters
    {
        [TestMethod]
        public void Defaults_NoAttributeWithDefaultInt_Legacy()
        {
            // Arrange
            Container.RegisterType<Service>(new InjectionMethod(nameof(Service.NoAttributeWithDefaultInt), typeof(int)));

            // Act
            var result = Container.Resolve<Service>();

            // Assert
            Assert.AreEqual(result.Called, 9);
            Assert.AreEqual(result.Value, Service.DefaultInt);
        }

        [TestMethod]
        public void Defaults_NoAttributeWithDefaultUnresolved_Legacy()
        {
            // Arrange
            Container.RegisterType<Service>(new InjectionMethod(nameof(Service.NoAttributeWithDefaultUnresolved), typeof(long)));

            // Act
            var result = Container.Resolve<Service>();

            // Assert
            Assert.AreEqual(result.Called, 14);
            Assert.AreEqual((long)result.Value, 100);
        }

        [TestMethod]
        public void Defaults_NoAttributeWithDisposableUnresolved_Legacy()
        {
            // Arrange
            Container.RegisterType<Service>(new InjectionMethod(nameof(Service.WithDefaultDisposableUnresolved), typeof(IDisposable)));

            // Act
            var result = Container.Resolve<Service>();

            // Assert
            Assert.AreEqual(result.Called, 15);
            Assert.IsNull(result.Value);
        }
    }
}
