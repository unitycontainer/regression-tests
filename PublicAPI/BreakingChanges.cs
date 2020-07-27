using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
#if NET45
using Microsoft.Practices.Unity;
#else
using Unity.Injection;
using Unity.Lifetime;
using Unity;
#endif

namespace Breaking.Changes
{
    [TestClass]
    public class BreakingChangesAPI
    {
        IUnityContainer Container;
        IService Instance = new Service();

        [TestInitialize]
        public void TestInitialize() => Container = new UnityContainer();

        [TestMethod]
        public void Registrations_Manager_Never_Null()
        {
            // Arrange
            Container.RegisterType<Service>();

            // Act
            var registration = Container.Registrations.First(r => typeof(Service) == r.RegisteredType);

            // Validate
            Assert.IsNotNull(registration.LifetimeManager);
        }

        [TestMethod]
        public virtual void Registrations_InstanceType_MappedTo()
        {
            // Arrange
            Container.RegisterInstance(typeof(IService), Instance);

            // Act
            var registration = Container.Registrations.First(r => typeof(IService) == r.RegisteredType);

            // Validate
            Assert.AreEqual(Instance.GetType(), registration.MappedToType);
        }
    }

    #region Test Data

    public interface IService
    { }

    public class Service : IService
    { }

    #endregion
}
