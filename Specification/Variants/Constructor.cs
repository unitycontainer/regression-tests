using Microsoft.VisualStudio.TestTools.UnitTesting;
using Specification;
using System;
#if NET45
using Microsoft.Practices.Unity;
#else
using Unity;
#endif

namespace Specification.Variants
{
    [TestClass]
    public partial class Constructors_BuiltUp : Specification.Constructors
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
    public partial class Constructors_Compiled : Specification.Constructors
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
    public partial class Constructors_Activated : Specification.Constructors
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
    public partial class Constructors_BuiltUp_Diagnostic : Specification.Constructors_Diagnostic
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
    public partial class Constructors_Compiled_Diagnostic : Specification.Constructors_Diagnostic
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
    public partial class Constructors_Activated_Diagnostic : Specification.Constructors_Diagnostic
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
