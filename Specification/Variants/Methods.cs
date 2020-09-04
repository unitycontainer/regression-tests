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
    public partial class Methods_BuiltUp : Specification.Methods
    {
#if !NET45
        public override void TestInitialize()
        {
            base.TestInitialize();
        }
#endif
    }

    [TestClass]
    public partial class Methods_Compiled : Specification.Methods
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
    public partial class Methods_Activated : Specification.Methods
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
    public partial class Methods_BuiltUp_Diagnostic : Specification.Methods_Diagnostic
    {
#if !NET45
        public override void TestInitialize()
        {
            base.TestInitialize();
        }
#endif
    }

    [TestClass]
    public partial class Methods_Compiled_Diagnostic : Specification.Methods_Diagnostic
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
    public partial class Methods_Activated_Diagnostic : Specification.Methods_Diagnostic
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
