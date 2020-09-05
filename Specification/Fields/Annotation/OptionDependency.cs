using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
#if NET45
using Microsoft.Practices.Unity;
#else
using Unity;
#endif

namespace Specification
{
    public partial class Fields
    {
        [TestMethod]
        public void Annotation_OptionalDependencyAttribute()
        {
            // Act
            var result = Container.Resolve<OptionalDependencyAttributeType>();

            // Assert
            Assert.AreEqual(result.Called, 4);
            Assert.IsInstanceOfType(result.Value, typeof(object));
        }

        [TestMethod]
        public void Annotation_OptionalNamedDependencyAttribute()
        {
            // Act
            var result = Container.Resolve<OptionalNamedDependencyAttributeType>();

            // Assert
            Assert.AreEqual(result.Called, 5);
            Assert.IsInstanceOfType(result.Value, typeof(string));
            Assert.AreEqual(result.Value, Name);
        }

        [TestMethod]
        public void Annotation_OptionalDependencyAttributeMissing()
        {
            // Act
            var result = Container.Resolve<OptionalDependencyAttributeMissingType>();

            // Assert
            Assert.AreEqual(result.Called, 6);
            Assert.IsNull(result.Value);
        }

        [TestMethod]
        public void Annotation_OptionalNamedDependencyAttributeMissing()
        {
            // Act
            var result = Container.Resolve<OptionalNamedDependencyAttributeMissingType>();

            // Assert
            Assert.AreEqual(result.Called, 7);
            Assert.IsNull(result.Value);
        }
    }
}
