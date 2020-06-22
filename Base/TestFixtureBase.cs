#if NET45
using Microsoft.Practices.Unity;
#else
using Unity;
#endif

namespace Unity.Regression.Tests
{
    public abstract class TestFixtureBase
    {
        public virtual IUnityContainer GetContainer() => new UnityContainer();
    }
}
