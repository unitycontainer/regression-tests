using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
#if NET45
using Microsoft.Practices.Unity;
#else
using Unity;
#endif

namespace Specification
{
    public partial class Properties
    {
        [TestMethod]
        public void Annotation_Baseline()
        {
            // Act
            var result = Container.Resolve<NoAttributeType>();

            // Assert
            Assert.AreEqual(result.Called, 1);
            Assert.IsNull(result.Value);
        }

        [TestMethod]
        public void Annotation_DependencyAttribute()
        {
            // Act
            var result = Container.Resolve<DependencyAttributeType>();

            // Assert
            Assert.AreEqual(result.Called, 2);
            Assert.IsInstanceOfType(result.Value, typeof(object));
        }

        [TestMethod]
        public void Annotation_NamedDependencyAttribute()
        {
            // Act
            var result = Container.Resolve<NamedDependencyAttributeType>();

            // Assert
            Assert.AreEqual(result.Called, 3);
            Assert.IsInstanceOfType(result.Value, typeof(string));
            Assert.AreEqual(result.Value, Name);
        }

        [TestMethod]
        public virtual void Annotation_DependencyAttributeOnPrivate()
        {
            // Act
            var result = Container.Resolve<DependencyAttributePrivateType>();

            // Assert
            Assert.AreEqual(result.Called, 8);
            Assert.IsNull(result.Value);
        }

        [TestMethod]
        public virtual void Annotation_DependencyAttributeOnProtected()
        {
            // Act
            var result = Container.Resolve<DependencyAttributeProtectedType>();

            // Assert
            Assert.AreEqual(result.Called, 9);
            Assert.IsNull(result.Value);
        }
    }

    public partial class Properties_Diagnostic : Properties
    {
        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public override void Annotation_DependencyAttributeOnPrivate()
        {
            base.Annotation_DependencyAttributeOnPrivate();
        }

        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public override void Annotation_DependencyAttributeOnProtected()
        {
            base.Annotation_DependencyAttributeOnProtected();
        }
    }
}
