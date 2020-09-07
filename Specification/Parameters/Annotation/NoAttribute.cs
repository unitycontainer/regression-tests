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
#if !NET45
        [TestMethod]
        public void Annotation_Baseline()
        {
            // Arrange
            Container.RegisterType<Service>(new InjectionMethod(nameof(Service.NoAttributeParameter)));

            // Act
            var result = Container.Resolve<Service>();

            // Assert
            Assert.AreEqual(result.Called, 1);
            Assert.IsInstanceOfType(result.Value, typeof(object));
        }
#endif

        [TestMethod]
        public void Annotation_Baseline_WithTypes()
        {
            // Arrange
            Container.RegisterType<Service>(new InjectionMethod(nameof(Service.NoAttributeParameter), typeof(object)));

            // Act
            var result = Container.Resolve<Service>();

            // Assert
            Assert.AreEqual(result.Called, 1);
            Assert.IsInstanceOfType(result.Value, typeof(object));
        }
    }
}
