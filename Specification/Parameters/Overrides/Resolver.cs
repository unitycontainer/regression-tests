using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
#if NET45
using Microsoft.Practices.Unity;
#else
using Unity;
using Unity.Resolution;
#endif

namespace Specification
{
    public partial class Parameters
    {
        [TestMethod]
        public void Overrides_TypePassedToResolver()
        {
            // Arrange
            var resolver = new ValidatingResolver(1);
            Container.RegisterType<Service>(
                Invoke.Method(nameof(Service.NoAttributeParameter)));

            // Act
            var result = Container.Resolve<Service>(Override.Parameter(typeof(object), resolver));

            // Assert
            Assert.IsNotNull(result.Value);
            Assert.IsInstanceOfType(result.Value, typeof(object));
            Assert.AreEqual(1, result.Value);

            Assert.AreEqual(typeof(object), resolver.Type);
        }

        [TestMethod]
        public void Overrides_TypePassedToResolverAttr()
        {
            // Arrange
            var resolver = new ValidatingResolver(1);
            Container.RegisterType<Service>(
                Invoke.Method(nameof(Service.DependencyAttribute)));

            // Act
            var result = Container.Resolve<Service>(Override.Parameter(typeof(object), resolver));

            // Assert
            Assert.IsNotNull(result.Value);
            Assert.IsInstanceOfType(result.Value, typeof(object));
            Assert.AreEqual(1, result.Value);

            Assert.AreEqual(typeof(object), resolver.Type);
        }

        [TestMethod]
        public void Overrides_TypeAndNamePassedToResolverNamed()
        {
            // Arrange
            var value = "value";
            var resolver = new ValidatingResolver(value);
            Container.RegisterType<Service>(
                Invoke.Method(nameof(Service.NamedDependencyAttribute)));

            // Act
            var result = Container.Resolve<Service>(Override.Parameter(typeof(string), resolver));

            // Assert
            Assert.IsNotNull(result.Value);
            Assert.IsInstanceOfType(result.Value, typeof(string));
            Assert.AreEqual(value, result.Value);

            Assert.AreEqual(typeof(string), resolver.Type);
            Assert.AreEqual(Name, resolver.Name);
        }

        [TestMethod]
        public void Overrides_TypeAndNamePassedToResolverGeneric()
        {
            // Arrange
            var value = "value";
            var resolver = new ValidatingResolver(value);
            Container.RegisterType<Service>(
                Invoke.Method(nameof(Service.NamedDependencyAttribute)));

            // Act
            var result = Container.Resolve<Service>(Override.Parameter<string>(resolver));

            // Assert
            Assert.IsNotNull(result.Value);
            Assert.IsInstanceOfType(result.Value, typeof(string));
            Assert.AreEqual(value, result.Value);

            Assert.AreEqual(typeof(string), resolver.Type);
            Assert.AreEqual(Name, resolver.Name);
        }
    }
}
