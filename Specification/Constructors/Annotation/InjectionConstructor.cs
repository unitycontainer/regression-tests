using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
#if NET45
using Microsoft.Practices.Unity;
#else
using Unity;
using Unity.Resolution;
#endif

namespace Spec.Constructors
{
    public partial class Constructors
    {
        [TestMethod]
        public void Annotation_Constructor()
        {
            #region attribute_ctor

            // Act
            var instance = Container.Resolve<Service>();

            // 2 == instance.Ctor

            #endregion
            // Assert
            Assert.AreEqual(2, instance.Ctor);
        }


        [TestMethod]
#if NET45
        [ExpectedException(typeof(ResolutionFailedException))]
#endif
        public virtual void Annotation_MultipleConstructorsAnnotated()
        {
            // Act
            var instance = Container.Resolve<TypeWithAmbuguousAnnotations>();

            // Assert
            Assert.AreEqual(Container, instance.Container);
        }
    }

    public partial class Constructors_Diagnostic
    {
        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public override void Annotation_MultipleConstructorsAnnotated() 
            => base.Annotation_MultipleConstructorsAnnotated();
    }
}
