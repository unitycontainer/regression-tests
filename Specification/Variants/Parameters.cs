using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
#if NET45
using Microsoft.Practices.Unity;
#else
using Unity;
#endif

namespace Spec.Parameters
{
    [TestClass]
    public partial class BuiltUp : Parameters
    {
#if !NET45
        public override void TestInitialize()
        {
            base.TestInitialize();
        }
#endif
    }

    [TestClass]
    public partial class Compiled : Parameters
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
    public partial class Activated : Parameters
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
    public partial class BuiltUp_Diagnostic : Parameters_Diagnostic
    {
#if !NET45
        public override void TestInitialize()
        {
            base.TestInitialize();
        }
#endif
    }

    [TestClass]
    public partial class Compiled_Diagnostic : Parameters_Diagnostic
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
    public partial class Activated_Diagnostic : Parameters_Diagnostic
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
