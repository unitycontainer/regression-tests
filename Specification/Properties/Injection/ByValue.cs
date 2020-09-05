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
        public void Injection_ByNameValue()
        {
            // Setup
            var test = "test";
            Container.RegisterType<ObjectWithThreeProperties>(
                Inject.Property(nameof(ObjectWithThreeProperties.Property), test));

            // Act
            var result = Container.Resolve<ObjectWithThreeProperties>();

            // Verify
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Property);
            Assert.AreSame(result.Property, test);
            Assert.AreEqual(result.Name, Name);
            Assert.IsNotNull(result.Container);
        }

        [TestMethod]
        public void Injection_ValueNull()
        {
            // Setup
            Container.RegisterType<ObjectWithThreeProperties>(
                Inject.Property(nameof(ObjectWithThreeProperties.Name), null));

            // Act
            var result = Container.Resolve<ObjectWithThreeProperties>();

            // Verify
            Assert.IsNotNull(result);
            Assert.IsNull(result.Property);
            Assert.IsNull(result.Name);
            Assert.IsNotNull(result.Container);
        }

        [TestMethod]
        public void Injection_ByNameValueInDerived()
        {
            // Setup
            var test = "test";
            Container.RegisterType<ObjectWithFourProperties>(
                Inject.Property(nameof(ObjectWithFourProperties.Property), test));

            // Act
            var result = Container.Resolve<ObjectWithFourProperties>();

            // Verify
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Property);
            Assert.AreSame(result.Property, test);
            Assert.AreEqual(result.Name, Name);
            Assert.IsNotNull(result.Container);
        }
    }
}
