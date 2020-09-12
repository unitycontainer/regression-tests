﻿using System;
#if V4
using Microsoft.Practices.Unity;
#else
using Unity;
using Unity.Injection;
#endif

namespace Specification.Pattern
{
    // Tests in this file are not applicable to Properties
    public partial class Properties
    {
        // Properties do not support implicit injection
        public override void Implicit_Resolvable(string test, Type type, string name, Type dependency, object expected) { }
#if !V4
        public override void Implicit_WithDefault(string test, Type type, string name, Type dependency, object expected) { }
#endif
        public override void Implicit_Unregistered(string test, Type type, string name, Type dependency, object expected) { }


        // Properties do not support parameters
        public override void Implicit_Parameters(string name) { }
        public override void Annotated_Parameters(string target) { }
        public override void Injected_ByName_Parameter(string target, Type dependency) { }
        public override void Injected_Implicitly_Parameter(string target, Type dependency) { }
        public override void Injected_ByResolving_Parameter(string target, Type dependency) { }
        public override void Injected_ByOptional_Parameter(string target, Type dependency) { }
    }
}
