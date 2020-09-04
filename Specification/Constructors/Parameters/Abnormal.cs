using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
#if NET45
using Microsoft.Practices.Unity;
#else
using Unity;
using Unity.Resolution;
#endif

namespace Specification
{
    public partial class Constructors
    {

        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public void Parameters_Unresolvable()
        {
            // Act
            var instance = Container.Resolve<Unresolvable>();

            // Validate
            Assert.IsNotNull(instance);
        }

        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public void Parameters_Ref()
        {
            Container.Resolve<TypeWithRefParameter>();
        }

        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public void Parameters_Out()
        {
            Container.Resolve<TypeWithOutParameter>();
        }

        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public void Parameters_Struct()
        {
            // Act
            var instance = Container.Resolve<TypeWithStructParameter>();

            // Validate
            Assert.IsNotNull(instance);
        }

        [TestMethod]
        public void Parameters_Dynamic()
        {
            // Act
            var instance = Container.Resolve<TypeWithDynamicParameter>();

            // Validate
            Assert.IsNotNull(instance);
        }

        [TestMethod]
        public void Parameters_NamedDynamic()
        {
            // Act
            var instance = Container.Resolve<NamedTypeWithDynamicParameter>();

            // Validate
            Assert.IsNotNull(instance);
        }
    }
}
