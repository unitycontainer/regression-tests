using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity.Regression.Tests;
#if NET45
using Microsoft.Practices.Unity;
#else
using Unity.Injection;
using Unity;
#endif

namespace Specification
{
    public partial class Parameters
    {
        [TestMethod]
        public void Injected_ByResolver()
        {
            var injected = "injected";
            var resolver = new ValidatingResolver(injected);

            // Arrange
            Container.RegisterType<InjectedType>(
                new InjectionMethod(nameof(InjectedType.Method),
                    new InjectionParameter(resolver)));

            // Act
            var result = Container.Resolve<InjectedType>();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(injected, result.Value);
        }

        [TestMethod]
        public void Injected_ByResolver_WithType()
        {
            var injected = "injected";
            var resolver = new ValidatingResolver(injected);

            // Arrange
            Container.RegisterType<InjectedType>(
                new InjectionMethod(nameof(InjectedType.Method),
                    new InjectionParameter(typeof(string), resolver)));

            // Act
            var result = Container.Resolve<InjectedType>();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(injected, result.Value);
        }

        [TestMethod]
        public void Injected_NamedDependencyByFactory()
        {
            var injected = "injected";
            var resolver = new ValidatingResolverFactory(injected);

            // Arrange
            Container.RegisterType<Service>(
                new InjectionMethod(nameof(Service.NamedDependencyAttribute), 
                    Inject.Parameter(typeof(string), resolver)));

            // Act
            var result = Container.Resolve<Service>();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(injected, result.Value);
        }
    }
}
