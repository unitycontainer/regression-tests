using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
#if NET45
using Microsoft.Practices.Unity;
#else
using Unity;
#endif

namespace Specification
{
    [TestClass]
    public partial class Properties
    {
        protected const string Name = "name";
        protected IUnityContainer Container;

        [TestInitialize]
        public virtual void TestInitialize() => Container = new UnityContainer();

        [TestMethod]
        public void Baseline()
        { }
    }

    [TestClass]
    public partial class Properties_Diagnostic : Properties
    {
        [TestInitialize]
#if NET45
        public override void TestInitialize() => Container = new UnityContainer();
#else
        public override void TestInitialize() => Container = new UnityContainer()
            .AddExtension(new Unity.Diagnostic());
#endif

        [TestMethod]
        public void Baseline_Diagnostic()
        { }
    }

    #region Test Data


    #endregion
}
