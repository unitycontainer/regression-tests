using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
#if NET45
using Microsoft.Practices.Unity;
#else
using Unity;
#endif

namespace Constructors.Diagnostic
{
    [TestClass]
    public partial class Specification : Constructors.Specification
    {
        [TestInitialize]
#if NET45
        public override void TestInitialize() => Container = new UnityContainer();
#else
        public override void TestInitialize() => Container = new UnityContainer()
            .AddExtension(new Unity.Diagnostic());
#endif
    }


    #region Test Data


    #endregion
}
