using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity.Regression.Tests;
using System.Linq;
using System.Collections.Generic;
using System;
#if NET45
using Microsoft.Practices.Unity;
#else
using Unity.Injection;
using Unity.Lifetime;
using Unity;
#endif

namespace Container.Interfaces
{
    public partial class UnityContainerAPI
    {
        #region IUnityContainer

        [TestMethod]
        public void ResolveTest()
        {
            //object Resolve(Type t, string name, params ResolverOverride[] resolverOverrides);
        }

        [TestMethod]
        public void ResolveAllTest()
        {
            //IEnumerable<object> ResolveAll(Type t, params ResolverOverride[] resolverOverrides);
        }

        [TestMethod]
        public void BuildUpTest()
        {
            //object BuildUp(Type t, object existing, string name, params ResolverOverride[] resolverOverrides);
        }

        [TestMethod]
        public void TeardownTest()
        {
            //void Teardown(object o);
        }

        [TestMethod]
        public void AddExtensionTest()
        {
            //IUnityContainer AddExtension(UnityContainerExtension extension);
        }

        [TestMethod]
        public void ConfigureTest()
        {
            //object Configure(Type configurationInterface);
        }

        [TestMethod]
        public void RemoveAllExtensionsTest()
        {
            //IUnityContainer RemoveAllExtensions();
        }

        [TestMethod]
        public void ParentTest()
        {
            //IUnityContainer Parent { get; }
        }

        [TestMethod]
        public void CreateChildContainerTest()
        {
            //IUnityContainer CreateChildContainer();
        }

        #endregion
    }
}
