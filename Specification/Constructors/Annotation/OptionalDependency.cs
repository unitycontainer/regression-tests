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
        public void Annotation_WithOptionalDependency()
        {
            // Arrange
            Container.RegisterInstance(_data)
                     .RegisterInstance(Name, Name);

            // Act
            var instance = Container.Resolve<CtorWithOptionalDependency>();

            // Validate
            Assert.AreEqual(_data, instance.Data);
        }


        [TestMethod]
        public void Annotation_WithOptionalNamedDependency()
        {
            // Arrange
            Container.RegisterInstance(_data)
                     .RegisterInstance(Name, Name);

            // Act
            var instance = Container.Resolve<CtorWithOptionalNamedDependency>();

            // Validate
            Assert.AreEqual(Name, instance.Data);
        }
    }
}
