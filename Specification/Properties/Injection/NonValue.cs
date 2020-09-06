using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity.Regression.Tests;
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
        public void Injection_Dependency()
        {
            // Act
            var result = Container.Resolve<ObjectWithNamedDependency>();

            // Verify
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Container);
            Assert.AreEqual(Name, result.Property);
        }

        [TestMethod]
        public void Injection_Dependency_Injected()
        {
            var injected = "injected";

            // Setup
            Container.RegisterType<ObjectWithNamedDependency>(
                Inject.Property(nameof(ObjectWithNamedDependency.Property), injected));

            // Act
            var result = Container.Resolve<ObjectWithNamedDependency>();

            // Verify
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Container);
            Assert.AreEqual(injected, result.Property);
        }


        [TestMethod]
        public void Injection_Dependency_Resolver()
        {
            var injected = "injected";
            var resolver = new ValidatingResolver(injected);

            // Setup
            Container.RegisterType<ObjectWithNamedDependency>(
                Inject.Property(nameof(ObjectWithNamedDependency.Property), resolver));

            // Act
            var result = Container.Resolve<ObjectWithNamedDependency>();

            // Verify
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Container);
            Assert.AreEqual(injected, result.Property);
            Assert.AreEqual(typeof(string), resolver.Type);
            Assert.AreEqual(Name, resolver.Name);
        }

        [TestMethod]
        public void Injection_Dependency_Factory()
        {
            // Setup
            var injected = "injected";
            var resolver = new ValidatingResolverFactory(injected);
            Container.RegisterType<ObjectWithNamedDependency>(
                Inject.Property(nameof(ObjectWithNamedDependency.Property), resolver));

            // Act
            var result = Container.Resolve<ObjectWithNamedDependency>();

            // Verify
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Container);
            Assert.AreEqual(injected, result.Property);
            Assert.AreEqual(typeof(string), resolver.Type);
            Assert.AreEqual(Name, resolver.Name);
        }
    }
}
