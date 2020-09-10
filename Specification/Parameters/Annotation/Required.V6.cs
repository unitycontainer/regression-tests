using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
#if V4
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
        public void Annotation_WithDefaultInt_Legacy()
        {
            // Arrange
            Container.RegisterType<Service>(new InjectionMethod(nameof(Service.DependencyAttributeWithDefaultInt), typeof(int)));

            // Act
            var result = Container.Resolve<Service>();

            // Assert
            Assert.AreEqual(result.Called, 10);
            Assert.AreEqual(result.Value, Service.DefaultInt);
        }

        [TestMethod]
        public void Annotation_NamedWithDefaultInt_Legacy()
        {
            // Arrange
            Container.RegisterType<Service>(new InjectionMethod(nameof(Service.NamedDependencyAttributeWithDefaultInt), typeof(int)));

            // Act
            var result = Container.Resolve<Service>();

            // Assert
            Assert.AreEqual(result.Called, 11);
            Assert.AreEqual(result.Value, Service.DefaultInt);
        }

        [TestMethod]
        public void Annotation_WithDefaultNullUnresolved_Legacy()
        {
            // Arrange
            Container.RegisterType<Service>(new InjectionMethod(nameof(Service.DependencyAttributeWithDefaultNullUnresolved), typeof(IDisposable)));

            // Act
            var result = Container.Resolve<Service>();

            // Assert
            Assert.AreEqual(result.Called, 16);
            Assert.IsNull(result.Value);
        }
    }
}
