using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
#if V4
using Microsoft.Practices.Unity;
#else
using Unity;
#endif

namespace Specification
{
    public partial class Fields
    {
        [TestMethod]
        public void Injection_ValueOverAttribute()
        {
            // Setup
            Container.RegisterType<ObjectWithAttributes>(
                Inject.Field(nameof(ObjectWithAttributes.Dependency), Name2));

            // Act
            var result = Container.Resolve<ObjectWithAttributes>();

            // Verify
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Dependency);
            Assert.AreEqual(result.Dependency, Name2);
            Assert.IsNull(result.Optional);
        }

        [TestMethod]
        public void Injection_ResolveOverAttribute()
        {
            // Setup
            Container.RegisterType<ObjectWithAttributes>(
                Resolve.Field(nameof(ObjectWithAttributes.Dependency)));

            // Act
            var result = Container.Resolve<ObjectWithAttributes>();

            // Verify
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Dependency);
            Assert.AreEqual(result.Dependency, Name);
            Assert.IsNull(result.Optional);
        }

        [TestMethod]
        public void Injection_ResolverOverAttribute()
        {
            // Setup
            Container.RegisterType<ObjectWithAttributes>(
                Inject.Field(nameof(ObjectWithAttributes.Dependency), Resolve.Parameter(Name1)));

            // Act
            var result = Container.Resolve<ObjectWithAttributes>();

            // Verify
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Dependency);
            Assert.AreEqual(result.Dependency, Name1);
            Assert.IsNull(result.Optional);
        }
    }
}
