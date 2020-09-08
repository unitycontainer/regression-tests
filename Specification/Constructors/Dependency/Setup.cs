using Microsoft.VisualStudio.TestTools.UnitTesting;
#if NET45
using Microsoft.Practices.Unity;
#else
using Unity;
using Unity.Injection;
#endif

namespace Specification
{
    [TestClass]
    public partial class Constructor_Parameters : DependencyPattern
    {
    }
}
