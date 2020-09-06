using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
#if NET45
using Microsoft.Practices.Unity;
#else
using Unity;
#endif

namespace Specification
{
    public partial class Parameters
    {
        [TestMethod]
        public void Annotation_NoAttribute()
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
        public void Annotation_NoAttributeWithDefault()
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
        public void Annotation_NoAttributeWithDefaultInt()
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
        public void Annotation_NoAttributeWithDefaultUnresolved()
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
        public void Annotation_NoAttributeWithDisposableUnresolved()
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
