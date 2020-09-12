#if V4
using Microsoft.Practices.Unity;
#else
using System;
using Unity;
using Unity.Injection;
#endif

namespace Specification.Pattern
{
    // Tests in this file are not applicable to Fields
    public partial class Fields
    {
        // Fields do not support implicit injection

        public override void Implicit_Resolvable(string test, Type type, string name, Type dependency, object expected) { }
#if !V4
        public override void Implicit_WithDefault(string test, Type type, string name, Type dependency, object expected) { }
#endif
        public override void Implicit_Unregistered(string test, Type type, string name, Type dependency, object expected) { }


        // Fields do not support parameters
        public override void Implicit_Parameters(string name) { }
        public override void Annotated_Parameters(string target) { }




        public override void Registered_Injected_MethodBase_ByType(string name, Type dependency, object expected) { }


        public override void Unregistered_Injected_ByType(string name, Type dependency) { }
#if !V4 && !NET461
        public override void Unregistered_Required_WithDefault(string name, object registered, object @default) { }

        public override void Unregistered_Optional_Injected_ByType(string name, Type dependency, object expected) { }

        public override void Unregistered_Injected_ByType_WithDefault(string name, Type dependency, object expected) { }

        public override void Unregistered_Injected_ByResolving_WithDefault(string name, Type dependency, object expected) { }
#endif
    }
}
