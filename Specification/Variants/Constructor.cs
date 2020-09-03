using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
#if NET45
using Microsoft.Practices.Unity;
#else
using Unity;
#endif

namespace Spec.Constructors
{
    [TestClass]
    public partial class BuiltUp : Constructors
    {
#if !NET45
        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();
        }
#endif
    }

    [TestClass]
    public partial class Compiled : Constructors
    {
        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();
#if !NET45
            Container.AddExtension(new ForceCompillation());
#endif
        }
    }

    [TestClass]
    public partial class Activated : Constructors
    {
        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();
#if !NET45
            Container.AddExtension(new ForceActivation());
#endif
        }
    }

    [TestClass]
    public partial class BuiltUp_Diagnostic : Constructors_Diagnostic
    {
#if !NET45
        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();
        }
#endif
    }

    [TestClass]
    public partial class Compiled_Diagnostic : Constructors_Diagnostic
    {
        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();
#if !NET45
            Container.AddExtension(new ForceCompillation());
#endif
        }
    }

    [TestClass]
    public partial class Activated_Diagnostic : Constructors_Diagnostic
    {
        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();
#if !NET45
            Container.AddExtension(new ForceActivation());
#endif
        }
    }
}
