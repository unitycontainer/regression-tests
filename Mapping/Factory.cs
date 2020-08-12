using Microsoft.VisualStudio.TestTools.UnitTesting;
#if NET45
using Microsoft.Practices.Unity;
#else
using Unity.Injection;
using Unity.Lifetime;
using Unity;
#endif

namespace Mapping
{
    [TestClass]
    public class Factory
    {
        #region Scaffolding

        IUnityContainer Container;
        const string Name = "name";
        const string Other = "other";

        [TestInitialize]
        public void TestInitialize() => Container = new UnityContainer();

        #endregion


        [TestMethod]
        public void Open_Generic_Build()
        {
            var instance = new Service<object>();

            // Arrange
            Container.RegisterFactory(typeof(IService<>), (c, t, n) => instance);

            // Act
            var value = Container.Resolve<IService<object>>();

            // Validate
            Assert.IsNotNull(value);
            Assert.AreSame(instance, value);
        }


        [TestMethod]
        public void Factory_Never_Maps()
        {
            var instance = new Service<object>();

            // Arrange
            Container.RegisterFactory(typeof(IService<>), (c, t, n) => new OtherService<object>())
                     .RegisterInstance(instance);

            // Act
            var value = Container.Resolve<IService<object>>();

            // Validate
            Assert.IsNotNull(value);
            Assert.AreNotSame(instance, value);
        }
    }
}
