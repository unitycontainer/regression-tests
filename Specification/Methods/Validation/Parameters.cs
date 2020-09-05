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
        public void AnonymousTypeForGenericFails()
        {
            // Act
            Container.RegisterType(typeof(GenericService<,,>),
                Invoke.Method("Method", Resolve.Parameter()));
        }
    }
}
