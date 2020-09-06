using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Unity.Regression.Tests;
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
        public void Injected_NamedDependencyBaseline()
        {
            // Arrange
            Container.RegisterType<Service>(
                new InjectionMethod(nameof(Service.NamedDependencyAttribute)));

            // Act
            var result = Container.Resolve<Service>();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(Name, result.Value);
        }

        [TestMethod]
        public void Injected_NamedDependencyInjected()
        {
            var injected = "injected";

            // Arrange
            Container.RegisterType<Service>(
                new InjectionMethod(nameof(Service.NamedDependencyAttribute), Inject.Parameter(typeof(string), injected)));

            // Act
            var result = Container.Resolve<Service>();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(injected, result.Value);
        }

        [TestMethod]
        public void Injected_NamedDependencyByResolver()
        {
            var injected = "injected";
            var resolver = new ValidatingResolver(injected);

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
            Assert.AreEqual(typeof(string), resolver.Type);
            Assert.AreEqual(Name, resolver.Name);
        }

#if !NET45
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
            Assert.AreEqual(typeof(string), resolver.Type);
            Assert.AreEqual(Name, resolver.Name);
        }
#endif
    }
}
