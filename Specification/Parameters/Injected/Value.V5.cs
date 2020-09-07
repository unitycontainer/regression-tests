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
        public void Injected_Method_v5()
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
