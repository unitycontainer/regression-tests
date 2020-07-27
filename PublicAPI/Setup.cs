using Microsoft.VisualStudio.TestTools.UnitTesting;
#if NET45
using Microsoft.Practices.Unity;
#else
using Unity.Injection;
using Unity.Lifetime;
using Unity;
#endif

namespace Container.Interfaces
{
    [TestClass]
    public partial class UnityContainerAPI
    {
        protected const string Name = "name";
        IUnityContainer Container;

        [TestInitialize]
        public void TestInitialize() => Container = new UnityContainer();
    }

    #region Test Data

    public interface IService
    { }

    public class Service : IService
    { }

    #endregion
}
