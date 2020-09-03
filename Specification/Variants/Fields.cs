using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
#if NET45
using Microsoft.Practices.Unity;
#else
using Unity;
#endif

namespace Spec.Fields
{
    [TestClass]
    public partial class BuiltUp : Fields
    {
#if !NET45
        public override void TestInitialize()
        {
            base.TestInitialize();
        }
#endif
    }

    [TestClass]
    public partial class Compiled : Fields
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
    public partial class Activated : Fields
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
    public partial class BuiltUp_Diagnostic : Fields_Diagnostic
    {
#if !NET45
        public override void TestInitialize()
        {
            base.TestInitialize();
        }
#endif
    }

    [TestClass]
    public partial class Compiled_Diagnostic : Fields_Diagnostic
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
    public partial class Activated_Diagnostic : Fields_Diagnostic
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
