using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity;
using Unity.Injection;
using Unity.Lifetime;

namespace Container.Resolution
{
    [TestClass]
    public class Basics
    {
        protected IUnityContainer Container;

        [TestInitialize]
        public void InitializeTest() => Container = new UnityContainer();


        [TestMethod]
        public void ObjectFromEmptyContainer()
        {
            // Act/Verify
            Assert.IsNotNull(Container.Resolve<object>());
        }

        [TestMethod]
        public void ObjectRegistered()
        {
            Container.RegisterType<object>(new ContainerControlledLifetimeManager(), new InjectionConstructor());

            // Act/Verify
            Assert.IsNotNull(Container.Resolve<object>());
        }

    }
}
