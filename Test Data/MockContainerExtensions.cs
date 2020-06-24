#if NET45
using Microsoft.Practices.Unity;
#else
using Unity;
using Unity.Extension;
#endif

namespace Unity.Regression.Tests
{
    public interface IMockConfiguration : IUnityContainerExtensionConfigurator 
    {
#if NET45
        ExtensionContext Context { get; }
#else
        IExtensionContext Context { get; }
#endif
    }

    public interface IOtherConfiguration : IMockConfiguration
    { }

    public class MockContainerExtension : UnityContainerExtension, IMockConfiguration
    {
        public bool InitializeWasCalled { get; private set; } = false;

#if NET45
        ExtensionContext Context => Context;
#else
        IExtensionContext IMockConfiguration.Context => Context;
#endif

        protected override void Initialize() => InitializeWasCalled = true;
    }

    public class DerivedContainerExtension : MockContainerExtension
    {}

    public class OtherContainerExtension : UnityContainerExtension, IOtherConfiguration
    {
        public bool InitializeWasCalled { get; private set; } = false;

#if NET45
        ExtensionContext Context => Context;
#else
        IExtensionContext IMockConfiguration.Context => Context;
#endif

        protected override void Initialize() => InitializeWasCalled = true;
    }
}
