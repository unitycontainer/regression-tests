using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
#if NET45
using Microsoft.Practices.Unity;
#else
using Unity;
using Unity.Lifetime;
using Unity.Resolution;
#endif

namespace Specification
{
    public partial class Constructors
    {
        [TestMethod]
        public void Annotation_WithDefaultCtor()
        {
            // Act
            var instance = Container.Resolve<WithDefaultCtor>();

            // Validate
            Assert.AreSame(instance, instance.Data);
        }

        [TestMethod]
        public void Annotation_WithDependency()
        {
            // Arrange
            Container.RegisterInstance(_data)
                     .RegisterInstance(Name, Name);

            // Act
            var instance = Container.Resolve<CtorWithDependency>();

            // Validate
            Assert.AreEqual(_data, instance.Data);
        }

        [TestMethod]
        public void Annotation_WithNamedDependency()
        {
            // Arrange
            Container.RegisterInstance(_data)
                     .RegisterInstance(Name, Name);

            // Act
            var instance = Container.Resolve<CtorWithNamedDependency>();

            // Validate
            Assert.AreEqual(Name, instance.Data);
        }

        [TestMethod]
        public void Annotation_ChecksForDependencyName()
        {
            var value = "instance";

            // Arrange
            Container.RegisterInstance(typeof(string), null,       _data, new ContainerControlledLifetimeManager())
                     .RegisterInstance(typeof(string), Name,       value, new ContainerControlledLifetimeManager())
                     .RegisterInstance(typeof(string), "OtherName", Name, new ContainerControlledLifetimeManager());
            // Act
            var instance = Container.Resolve<CtorWithNamedDependency>();

            // Validate
            Assert.AreEqual(value, instance.Data);
        }

    }
}
