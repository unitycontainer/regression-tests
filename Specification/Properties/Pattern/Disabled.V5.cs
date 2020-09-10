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
        public override void Unregistered_Optional_WithDefault(string name, object registered, object @default)
        {
        }
    }
}
