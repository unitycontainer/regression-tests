using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
#if NET45
using Microsoft.Practices.Unity;
#else
using Unity;
#endif

namespace Spec.Properties
{
    [TestClass]
    public partial class BuiltUp : Properties
    {
#if !NET45
        public override void TestInitialize()
        {
            base.TestInitialize();
        }
#endif
    }

    [TestClass]
    public partial class Compiled : Properties
    {
#if !NET45
        public override void TestInitialize()
        {
            base.TestInitialize();
            Container.AddExtension(new ForceCompillation());
        }
#endif
    }

    [TestClass]
    public partial class Activated : Properties
    {
#if !NET45
        public override void TestInitialize()
        {
            base.TestInitialize();
            Container.AddExtension(new ForceActivation());
        }
#endif
    }

    [TestClass]
    public partial class BuiltUp_Diagnostic : Properties_Diagnostic
    {
#if !NET45
        public override void TestInitialize()
        {
            base.TestInitialize();
        }
#endif
    }

    [TestClass]
    public partial class Compiled_Diagnostic : Properties_Diagnostic
    {
#if !NET45
        public override void TestInitialize()
        {
            base.TestInitialize();
            Container.AddExtension(new ForceCompillation());
        }
#endif
    }

    [TestClass]
    public partial class Activated_Diagnostic : Properties_Diagnostic
    {
#if !NET45
        public override void TestInitialize()
        {
            base.TestInitialize();
            Container.AddExtension(new ForceActivation());
        }
#endif
    }
}
