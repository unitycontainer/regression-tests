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
        public void Annotation_Baseline()
        {
            // Act
            var result = Container.Resolve<NoAttributeType>();

            // Assert
            Assert.AreEqual(result.Called, 1);
            Assert.IsNull(result.Value);
        }

        [TestMethod]
        public void Annotation_Dependency()
        {
            // Act
            var result = Container.Resolve<DependencyAttributeType>();

            // Assert
            Assert.AreEqual(result.Called, 2);
            Assert.IsInstanceOfType(result.Value, typeof(object));
        }

        [TestMethod]
        public void Annotation_NamedDependency()
        {
            // Act
            var result = Container.Resolve<NamedDependencyAttributeType>();

            // Assert
            Assert.AreEqual(result.Called, 3);
            Assert.IsInstanceOfType(result.Value, typeof(string));
            Assert.AreEqual(result.Value, Name);
        }


        [TestMethod]
        public virtual void Annotation_DependencyOnPrivate()
        {
            // Act
            var result = Container.Resolve<DependencyAttributePrivateType>();

            // Assert
            Assert.AreEqual(result.Called, 8);
            Assert.IsNull(result.Value);
        }

        [TestMethod]
        public virtual void Annotation_DependencyOnProtected()
        {
            // Act
            var result = Container.Resolve<DependencyAttributeProtectedType>();

            // Assert
            Assert.AreEqual(result.Called, 9);
            Assert.IsNull(result.Value);
        }
    }

    public partial class Fields_Diagnostic : Fields
    {
        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public override void Annotation_DependencyOnPrivate() 
            => base.Annotation_DependencyOnPrivate();

        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public override void Annotation_DependencyOnProtected() 
            => base.Annotation_DependencyOnProtected();
    }
}
