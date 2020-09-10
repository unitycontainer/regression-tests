﻿using System;
#if V4
using Microsoft.Practices.Unity;
#else
using Unity;
using Unity.Injection;
#endif

namespace Specification.Pattern
{
    public partial class Properties
    {
        // Tests in this file are not applicable to Properties
        public override void Registered_Implicit(string name, object expected) { }
        
        public override void Registered_Injected_ByType(string name, Type dependency, object expected) { }

        public override void Registered_Implicit_WithDefault(string name, object registered, object @default) { }


        public override void Unregistered_Implicit(string name) { }

        public override void Unregistered_Injected_ByType(string name, Type dependency) { }

#if !V4
        public override void Unregistered_Required_WithDefault(string name, object registered, object @default) { }

        public override void Unregistered_Optional_Injected_ByType(string name, Type dependency, object expected) { }

        public override void Unregistered_Injected_ByType_WithDefault(string name, Type dependency, object expected) { }

        public override void Unregistered_Injected_ByResolving_WithDefault(string name, Type dependency, object expected) { }
#endif
    }
}
