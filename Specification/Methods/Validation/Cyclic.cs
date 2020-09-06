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
        [ExpectedException(typeof(ResolutionFailedException))]
        public void Cyclic_MethodToInterface()
        {
            // Arrange
            Container.RegisterType<I1, F1>();

            // Act
            Container.Resolve<F1>();
        }
    }
}
