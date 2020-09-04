using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
#if NET45
using Microsoft.Practices.Unity;
#else
using Unity;
#endif

namespace Specification.Variants
{
    [TestClass]
    public partial class Fields_BuiltUp : Specification.Fields
    {
#if !NET45
        public override void TestInitialize()
        {
            base.TestInitialize();
        }
#endif
    }

    [TestClass]
    public partial class Fields_Compiled : Specification.Fields
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
    public partial class Fields_Activated : Specification.Fields
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
    public partial class Fields_BuiltUp_Diagnostic : Specification.Fields_Diagnostic
    {
#if !NET45
        public override void TestInitialize()
        {
            base.TestInitialize();
        }
#endif
    }

    [TestClass]
    public partial class Fields_Compiled_Diagnostic : Specification.Fields_Diagnostic
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
    public partial class Fields_Activated_Diagnostic : Specification.Fields_Diagnostic
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
