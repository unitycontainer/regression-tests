using System;
#if V4
using Microsoft.Practices.Unity;
#else
using Unity;
using Unity.Injection;
#endif

namespace Specification.Pattern
{
    public partial class Constructors
    {
#if !V4
        public override void Unregistered_Injected_ByName(string target) { }
        
        public override void Unregistered_Optional_Injected_ByName(string target, object expected) { }

        public override void Unregistered_Injected_ByName_WithDefault(string target, object expected) { }
#endif
    }
}
