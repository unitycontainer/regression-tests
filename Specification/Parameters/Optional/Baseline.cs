using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
#if NET45
using Microsoft.Practices.Unity;
#else
using Unity;
using Unity.Injection;
using Unity.Resolution;
#endif

namespace Specification
{
    public partial class Parameters
    {
        [TestMethod]
        public void Optional_Baseline()
        {
            // Act
            Container.RegisterInstance(10);
            Container.RegisterInstance("1", 1);
            Container.RegisterInstance("2", 2);
            Container.RegisterInstance("1", "1");
            Container.RegisterInstance("2", "2");
            var result = Container.Resolve<Service>();

            // Assert
            Assert.IsNull(result.Value);
            Assert.IsNotNull(result.ValueOne);
            Assert.IsInstanceOfType(result.ValueOne, typeof(object));

            Assert.IsNotNull(Container.Resolve<int>());
            Assert.IsNotNull(Container.Resolve<int>("1"));

            Assert.IsNotNull(Container.Resolve<string>());
            Assert.IsNotNull(Container.Resolve<string>("1"));
        }

        [TestMethod]
        public void Optional_AttributedMethodBaseline()
        {
            // Arrange
            Container.RegisterType<Service>(
                new InjectionMethod(nameof(Service.Method)));

            // Act
            var result = Container.Resolve<Service>();

            // Assert
            Assert.IsNotNull(result.ValueOne);
            Assert.IsInstanceOfType(result.ValueOne, typeof(object));
        }
    }
}
