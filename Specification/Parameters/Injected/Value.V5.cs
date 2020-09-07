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
        public void Injected_WithAttribute_v5()
        {
            // Arrange
            Container.RegisterType<InjectedType>(
                new InjectionMethod(nameof(InjectedType.Method)));

            // Act
            var result = Container.Resolve<InjectedType>();

            // Assert
            Assert.IsNotNull(result.Value);
            Assert.IsInstanceOfType(result.Value, typeof(object));
        }
    }
}
