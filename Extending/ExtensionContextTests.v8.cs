using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity.Regression.Tests;
#if NET45
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
#else
using Unity;
#endif


namespace Container.Extending
{
    [TestClass]
    public partial class ExtensionContextTests
    {
        ExtensionContext context;   
        UnityContainer   container;

        [TestInitialize]
        public void TestInitialize()
        {
            container = new UnityContainer();
            var mock = new MockContainerExtension();
            container.AddExtension(mock);
            context = ((IMockConfiguration)mock).Context;
        }

        [TestMethod]
        public void BaselineTest() => Assert.IsNotNull(context);

        [TestMethod]
        public void ContainerTest()
        {
            // Validate
            Assert.IsNotNull(context.Container);
            Assert.IsInstanceOfType(context.Container, typeof(UnityContainer));
        }

        [TestMethod]
        public void PoliciesTest()
        {
            // Validate
            Assert.IsNotNull(context.Policies);
            Assert.IsInstanceOfType(context.Policies, typeof(IPolicyList));
        }

    }
}
