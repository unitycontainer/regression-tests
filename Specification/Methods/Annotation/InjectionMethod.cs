using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
#if V4
using Microsoft.Practices.Unity;
#else
using Unity;
#endif

namespace Specification
{
    public partial class Methods
    {
        [TestMethod]
        public void Annotation_NoParameters()
        {
            // Act
            var result = Container.Resolve<TypeNoParameters>();

            // Verify
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
        }

        [TestMethod]
        public void Annotation_WithParameters()
        {
            // Act
            var result = Container.Resolve<TypeWithParameter>();

            // Verify
            Assert.IsNotNull(result);
            Assert.AreEqual(Name, result.Data);
        }

        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public void Annotation_WithRefParameters()
        {
            // Act
            var result = Container.Resolve<TypeWithRefParameter>();

            // Verify
            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public void Annotation_WithOutParameters()
        {
            // Act
            var result = Container.Resolve<TypeWithOutParameter>();

            // Verify
            Assert.Fail();
        }
    }
}
