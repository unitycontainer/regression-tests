using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
#if NET45
using Microsoft.Practices.Unity;
#else
using Unity;
using Unity.Injection;
using Unity.Policy;
#endif

namespace Unity.Regression.Tests
{
    [TestClass]
    public class InjectionMethodFixture
    {
        [TestMethod]
        public void QualifyingInjectionMethodCanBeConfiguredAndIsCalled()
        {
            IUnityContainer container = new UnityContainer()
                .RegisterType<LegalInjectionMethod>(
                        new InjectionMethod("InjectMe"));

            LegalInjectionMethod result = container.Resolve<LegalInjectionMethod>();
            Assert.IsTrue(result.WasInjected);
        }

#if !UNITY_V6 // Starting with v6.0.0 input is no longer validated at registration

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CannotConfigureGenericInjectionMethod()
        {
            new UnityContainer()
                .RegisterType<OpenGenericInjectionMethod>(
                new InjectionMethod("InjectMe"));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CannotConfigureMethodWithOutParams()
        {
            new UnityContainer().RegisterType<OutParams>(
                new InjectionMethod("InjectMe", 12));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CannotConfigureMethodWithRefParams()
        {
            new UnityContainer()
                .RegisterType<RefParams>(
                new InjectionMethod("InjectMe", 15));
        }
#endif
        [TestMethod]
        public void CanInvokeInheritedMethod()
        {
            IUnityContainer container = new UnityContainer()
                          .RegisterType<InheritedClass>(
                                  new InjectionMethod("InjectMe"));

            InheritedClass result = container.Resolve<InheritedClass>();
            Assert.IsTrue(result.WasInjected);
        }

        public class LegalInjectionMethod
        {
            public bool WasInjected = false;

            public void InjectMe()
            {
                WasInjected = true;
            }
        }

        public class OpenGenericInjectionMethod
        {
            public void InjectMe<T>()
            {
            }
        }

        public class OutParams
        {
            public void InjectMe(out int x)
            {
                x = 2;
            }
        }

        public class RefParams
        {
            public void InjectMe(ref int x)
            {
                x *= 2;
            }
        }

        public class InheritedClass : LegalInjectionMethod
        {
        }
    }
}
