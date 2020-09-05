using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
#if NET45
using Microsoft.Practices.Unity;
#else
using Unity;
#endif

namespace Specification
{
    public partial class Properties
    {
        [TestMethod]
        public void Injection_BaseLine()
        {
            // Act
            var result = Container.Resolve<ObjectWithThreeProperties>();

            // Verify
            Assert.IsNotNull(result);
            Assert.IsNull(result.Property);
            Assert.AreEqual(result.Name, Name);
            Assert.IsNotNull(result.Container);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Injection_None()
        {
            // Act
            Container.RegisterType<ObjectWithThreeProperties>(
                Resolve.Property("Bogus Name"));
        }

        [TestMethod]
        public void Injection_ByName()
        {
            // Setup
            Container.RegisterType<ObjectWithThreeProperties>(
                Resolve.Property(nameof(ObjectWithThreeProperties.Property)));

            // Act
            var result = Container.Resolve<ObjectWithThreeProperties>();

            // Verify
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Property);
            Assert.IsInstanceOfType(result.Property, typeof(object));
            Assert.AreEqual(result.Name, Name);
            Assert.IsNotNull(result.Container);
        }

        [TestMethod]
        public void Injection_ByNameInDerived()
        {
            // Setup
            Container.RegisterType<ObjectWithFourProperties>(
                Resolve.Property(nameof(ObjectWithFourProperties.Property)));

            // Act
            var result = Container.Resolve<ObjectWithFourProperties>();

            // Verify
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Property);
            Assert.IsInstanceOfType(result.Property, typeof(object));
            Assert.AreEqual(result.Name, Name);
            Assert.IsNotNull(result.Container);
        }

        [TestMethod]
        public void Injection_NoneAsDependency()
        {
            // Act
            var result = Container.Resolve<ObjectWithDependency>();

            // Verify
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Dependency);
            Assert.IsNull(result.Dependency.Property);
            Assert.AreEqual(result.Dependency.Name, Name);
            Assert.IsNotNull(result.Dependency.Container);
        }

    }
}
