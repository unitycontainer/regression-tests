using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Unity;
using Unity.Injection;

namespace Specification
{
    public partial class Parameters
    {
        [TestMethod]
        public void Defaults_NoAttribute()
        {
            // Arrange
            Container.RegisterType<Service>(new InjectionMethod(nameof(Service.NoAttributeParameter)));

            // Act
            var result = Container.Resolve<Service>();

            // Assert
            Assert.AreEqual(result.Called, 1);
            Assert.IsInstanceOfType(result.Value, typeof(object));
        }

        [TestMethod]
        public void Defaults_NoAttribute_Legacy()
        {
            // Arrange
            Container.RegisterType<Service>(new InjectionMethod(nameof(Service.NoAttributeParameter), typeof(object)));

            // Act
            var result = Container.Resolve<Service>();

            // Assert
            Assert.AreEqual(result.Called, 1);
            Assert.IsInstanceOfType(result.Value, typeof(object));
        }

        [TestMethod]
        public void Defaults_NoAttributeWithDefault()
        {
            // Arrange
            Container.RegisterType<Service>(new InjectionMethod(nameof(Service.NoAttributeWithDefault)));

            // Act
            var result = Container.Resolve<Service>();

            // Assert
            Assert.AreEqual(result.Called, 8);
            Assert.AreNotEqual(result.Value, Service.DefaultString);
        }

        [TestMethod]
        public void Defaults_NoAttributeWithDefault_Legacy()
        {
            // Arrange
            Container.RegisterType<Service>(new InjectionMethod(nameof(Service.NoAttributeWithDefault), typeof(string)));

            // Act
            var result = Container.Resolve<Service>();

            // Assert
            Assert.AreEqual(result.Called, 8);
            Assert.AreNotEqual(result.Value, Service.DefaultString);
        }

        [TestMethod]
        public void Defaults_NoAttributeWithDefaultInt()
        {
            // Arrange
            Container.RegisterType<Service>(new InjectionMethod(nameof(Service.NoAttributeWithDefaultInt)));

            // Act
            var result = Container.Resolve<Service>();

            // Assert
            Assert.AreEqual(result.Called, 9);
            Assert.AreEqual(result.Value, Service.DefaultInt);
        }

        [TestMethod]
        public void Defaults_NoAttributeWithDefaultUnresolved()
        {
            // Arrange
            Container.RegisterType<Service>(new InjectionMethod(nameof(Service.NoAttributeWithDefaultUnresolved)));

            // Act
            var result = Container.Resolve<Service>();

            // Assert
            Assert.AreEqual(result.Called, 14);
            Assert.AreEqual((long)result.Value, 100);
        }

        [TestMethod]
        public void Defaults_NoAttributeWithDisposableUnresolved()
        {
            // Arrange
            Container.RegisterType<Service>(new InjectionMethod(nameof(Service.WithDefaultDisposableUnresolved)));

            // Act
            var result = Container.Resolve<Service>();

            // Assert
            Assert.AreEqual(result.Called, 15);
            Assert.IsNull(result.Value);
        }
    }
}
