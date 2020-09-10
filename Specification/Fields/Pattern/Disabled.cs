#if NET45
using Microsoft.Practices.Unity;
#else
using Unity;
using Unity.Injection;
#endif

namespace Specification.Pattern
{
    public partial class Fields
    {
        // Tests in this file are not applicable to Fields

        public override void Unregistered_Implicit(string name) { }

        public override void Registered_Implicit(string name, object expected) { }

        public override void Registered_Implicit_WithDefault(string name, object registered, object @default) { }

        public override void Unregistered_Required_WithDefault(string name, object registered, object @default) { }
    }
}
