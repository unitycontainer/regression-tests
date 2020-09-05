using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
#if NET45
using Microsoft.Practices.Unity;
#else
using Unity;
#endif

namespace Specification
{
    public partial class Methods_Diagnostic
    {
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GenericInjectionMethod()
        {
            // Act
            Container.RegisterType<OpenGenericInjectionMethod>(
                Invoke.Method(nameof(OpenGenericInjectionMethod.InjectMe)));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void MethodWithRefParameter()
        {
            // Act
            Container.RegisterType<TypeWithMethodWithInvalidParameter>(
                Invoke.Method(nameof(TypeWithMethodWithInvalidParameter.MethodWithRefParameter)));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void MethodWithOutParameter()
        {
            // Act
            Container.RegisterType<TypeWithMethodWithInvalidParameter>(
                Invoke.Method(nameof(TypeWithMethodWithInvalidParameter.MethodWithOutParameter)));
        }

    }
}
