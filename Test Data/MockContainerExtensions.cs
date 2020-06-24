﻿#if NET45
using Microsoft.Practices.Unity;
#else
using Unity;
using Unity.Extension;
#endif

namespace Unity.Regression.Tests
{
    public interface IMockConfiguration : IUnityContainerExtensionConfigurator 
    {
        ExtensionContext Context { get; }
    }

    public interface IOtherConfiguration : IMockConfiguration
    { }

    public class MockContainerExtension : UnityContainerExtension, IMockConfiguration
    {
        public bool InitializeWasCalled { get; private set; } = false;

        ExtensionContext IMockConfiguration.Context => Context;


        protected override void Initialize() => InitializeWasCalled = true;
    }

    public class DerivedContainerExtension : MockContainerExtension
    {}

    public class OtherContainerExtension : UnityContainerExtension, IOtherConfiguration
    {
        public bool InitializeWasCalled { get; private set; } = false;

        ExtensionContext IMockConfiguration.Context => Context;

        protected override void Initialize() => InitializeWasCalled = true;
    }
}
