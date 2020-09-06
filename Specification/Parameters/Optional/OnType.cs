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
    public partial class Parameters
    {
        [TestMethod]
        public void Optional_AttributedWithDefaultOptional()
        {
            // Arrange
            Container.RegisterType<Service>(
                new InjectionMethod(nameof(Service.Method), new OptionalParameter(typeof(object))));

            // Act
            var result = Container.Resolve<Service>();

            // Assert
            Assert.IsNotNull(result.ValueOne);
            Assert.IsInstanceOfType(result.ValueOne, typeof(object));
        }

        [TestMethod]
        public void Optional_AttributedWithTypedOptional()
        {
            // Arrange
            Container.RegisterType<Service>(
                new InjectionMethod(nameof(Service.Method),
                    Resolve.Optional<string>()));

            // Act
            var result = Container.Resolve<Service>();

            // Assert
            Assert.IsNotNull(result.ValueOne);
            Assert.AreSame(result.ValueOne, Container.Resolve<string>());
        }

        [TestMethod]
        public void Optional_AttributedWithTypedMissingOptional()
        {
            // Arrange
            Container.RegisterType<Service>(
                new InjectionMethod(nameof(Service.Method),
                    Resolve.Optional<ICommand<int>>()));

            // Act
            var result = Container.Resolve<Service>();

            // Assert
            Assert.IsNull(result.Value);
        }

        [TestMethod]
        public void Optional_Method()
        {
            // Arrange
            Container.RegisterType<Service>(
                new InjectionMethod(nameof(Service.Method),
                    Resolve.Parameter<string>()));

            // Act
            var result = Container.Resolve<Service>();

            // Assert
            Assert.IsNotNull(result.ValueOne);
            Assert.AreEqual(result.ValueOne, Container.Resolve<string>());
        }
    }
}
