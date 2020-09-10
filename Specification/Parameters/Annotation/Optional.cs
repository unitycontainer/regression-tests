using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
#if V4
using Microsoft.Practices.Unity;
#else
using Unity;
using Unity.Injection;
#endif

namespace Specification
{
    public partial class Parameters
    {
        [TestMethod]
        public void Annotation_Optional()
        {
            // Arrange
            Container.RegisterType<Service>(new InjectionMethod(nameof(Service.OptionalDependencyAttribute), typeof(object)));

            // Act
            var result = Container.Resolve<Service>();

            // Assert
            Assert.AreEqual(result.Called, 4);
            Assert.IsInstanceOfType(result.Value, typeof(object));
        }

        [TestMethod]
        public void Annotation_OptionalDependencyParameterIsResolvedIfRegisteredInContainer()
        {
            IService expectedSomeInterface = new Service1();
            Container.RegisterInstance<IService>(expectedSomeInterface);

            var result = Container.Resolve<ObjectWithOptionalConstructorParameter>();

            Assert.AreSame(expectedSomeInterface, result.SomeInterface);
        }

        [TestMethod]
        public void Annotation_OptionalDependencyParameterIsMissing()
        {
            var result = Container.Resolve<ObjectWithOptionalConstructorParameter>();

            Assert.IsNull(result.SomeInterface);
        }

        [TestMethod]
        public void Annotation_OptionalDependencyParameterIsResolvedByName()
        {
            IService namedSomeInterface = new Service1();
            IService defaultSomeInterface = new Service2();

            Container
                .RegisterInstance<IService>(defaultSomeInterface)
                .RegisterInstance<IService>(Name, namedSomeInterface);

            var result = Container.Resolve<ObjectWithNamedOptionalConstructorParameter>();

            Assert.AreSame(namedSomeInterface, result.SomeInterface);
        }

        [TestMethod]
        public void Annotation_OptionalNamedMissing()
        {
            var result = Container.Resolve<ObjectWithNamedOptionalConstructorParameter>();

            Assert.IsNull(result.SomeInterface);
        }
    }
}
