using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
#if NET45
using Microsoft.Practices.Unity;
#else
using Unity;
using Unity.Injection;
#endif

namespace Specification
{
    public partial class Parameters
    {
        [TestMethod]
        public void Annotation_OptionalDependencyAttribute()
        {
            // Arrange
            Container.RegisterType<Service>(new InjectionMethod(nameof(Service.OptionalDependencyAttribute)));

            // Act
            var result = Container.Resolve<Service>();

            // Assert
            Assert.AreEqual(result.Called, 4);
            Assert.IsInstanceOfType(result.Value, typeof(object));
        }

        [TestMethod]
        public void Annotation_OptionalNamedDependencyAttribute()
        {
            // Arrange
            Container.RegisterType<Service>(new InjectionMethod(nameof(Service.OptionalNamedDependencyAttribute)));

            // Act
            var result = Container.Resolve<Service>();

            // Assert
            Assert.AreEqual(result.Called, 5);
            Assert.IsInstanceOfType(result.Value, typeof(string));
            Assert.AreEqual(result.Value, Name);
        }

        [TestMethod]
        public void Annotation_OptionalDependencyAttributeMissing()
        {
            // Arrange
            Container.RegisterType<Service>(new InjectionMethod(nameof(Service.OptionalDependencyAttributeMissing)));

            // Act
            var result = Container.Resolve<Service>();

            // Assert
            Assert.AreEqual(result.Called, 6);
            Assert.IsNull(result.Value);
        }

        [TestMethod]
        public void Annotation_OptionalNamedDependencyAttributeMissing()
        {
            // Arrange
            Container.RegisterType<Service>(new InjectionMethod(nameof(Service.OptionalNamedDependencyAttributeMissing)));

            // Act
            var result = Container.Resolve<Service>();

            // Assert
            Assert.AreEqual(result.Called, 7);
            Assert.IsNull(result.Value);
        }

        [TestMethod]
        public void Annotation_OptionalDependencyAttributeWithDefaultInt()
        {
            // Arrange
            Container.RegisterType<Service>(new InjectionMethod(nameof(Service.OptionalDependencyAttributeWithDefaultInt)));

            // Act
            var result = Container.Resolve<Service>();

            // Assert
            Assert.AreEqual(result.Called, 12);
            Assert.AreEqual(result.Value, Service.DefaultInt);
        }

        [TestMethod]
        public void Annotation_OptionalNamedDependencyAttributeWithDefaultInt()
        {
            // Arrange
            Container.RegisterType<Service>(new InjectionMethod(nameof(Service.OptionalNamedDependencyAttributeWithDefaultInt)));

            // Act
            var result = Container.Resolve<Service>();

            // Assert
            Assert.AreEqual(result.Called, 13);
            Assert.AreEqual(result.Value, Service.DefaultInt);
        }
    }
}
