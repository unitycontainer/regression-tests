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
        public void Injected_WithString_v6()
        {
            // Arrange
            Container.RegisterType<InjectedType>(
                new InjectionMethod(nameof(InjectedType.MethodOne), new InjectionParameter(Name)));

            // Act
            var result = Container.Resolve<InjectedType>();

            // Assert
            Assert.IsNull(result.ValueOne);
            Assert.IsNotNull(result.ValueTwo);
            Assert.IsInstanceOfType(result.ValueTwo, typeof(string));
        }

        [TestMethod]
        public void Injected_WithStringAsString_v6()
        {
            // Arrange
            Container.RegisterType<InjectedType>(
                new InjectionMethod(nameof(InjectedType.MethodOne), new InjectionParameter(typeof(string), Name)));

            // Act
            var result = Container.Resolve<InjectedType>();

            // Assert
            Assert.IsNull(result.ValueOne);
            Assert.IsNotNull(result.ValueTwo);
            Assert.IsInstanceOfType(result.ValueTwo, typeof(string));
        }
    }
}
