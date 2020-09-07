using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
#if NET45
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
        public void Injected_InjectedAttributedBaseline()
        {
            // Act
            var result = Container.Resolve<OtherService>();

            // Assert
            Assert.IsNull(result.ValueOne);
            Assert.IsNotNull(result.Value);
            Assert.IsInstanceOfType(result.Value, typeof(object));
        }

        [TestMethod]
        public void Injected_InjectedAttributedMethod()
        {
            // Arrange
            Container.RegisterType<OtherService>(
                new InjectionMethod(nameof(OtherService.Method)));

            // Act
            var result = Container.Resolve<OtherService>();

            // Assert
            Assert.IsNotNull(result.Value);
            Assert.IsInstanceOfType(result.Value, typeof(object));
        }

        [TestMethod]
        public void Injected_InjectedAttributedMethodWithValue()
        {
            var data = "value";

            // Arrange
            Container.RegisterType<OtherService>(
                new InjectionMethod(nameof(OtherService.Method),
                    Inject.Parameter(data)));

            // Act
            var result = Container.Resolve<OtherService>();

            // Assert
            Assert.IsNotNull(result.Value);
            Assert.AreSame(result.Value, data);
        }

        [TestMethod]
        public void Injected_WithInjectedInt()
        {
            // Arrange
            Container.RegisterType<OtherService>(
                new InjectionMethod(nameof(OtherService.MethodOne), 
                    Inject.Parameter(1)));

            // Act
            var result = Container.Resolve<OtherService>();

            // Assert
            Assert.IsNotNull(result.Value);
            Assert.IsInstanceOfType(result.Value, typeof(object));
            Assert.IsNotNull(result.ValueOne);
            Assert.IsInstanceOfType(result.ValueOne, typeof(int));
        }

        [TestMethod]
        public void Injected_WithInjectedString()
        {
            // Arrange
            Container.RegisterType<OtherService>(
                new InjectionMethod(nameof(OtherService.MethodOne),
                    Inject.Parameter("test")));

            // Act
            var result = Container.Resolve<OtherService>();

            // Assert
            Assert.IsNotNull(result.Value);
            Assert.IsInstanceOfType(result.Value, typeof(object));
            Assert.IsNotNull(result.ValueOne);
            Assert.IsInstanceOfType(result.ValueOne, typeof(string));
        }

        [TestMethod]
        public void Injected_WithInjectedIntString()
        {
            // Arrange
            Container.RegisterType<OtherService>(
                new InjectionMethod(nameof(OtherService.MethodTwo),
                    Inject.Parameter(1),
                    Inject.Parameter("test")));

            // Act
            var result = Container.Resolve<OtherService>();

            // Assert
            Assert.IsNotNull(result.Value);
            Assert.IsInstanceOfType(result.Value, typeof(object));
            Assert.IsNotNull(result.ValueOne);
            Assert.IsInstanceOfType(result.ValueOne, typeof(int));
            Assert.IsNotNull(result.ValueTwo);
            Assert.IsInstanceOfType(result.ValueTwo, typeof(string));
        }

        [TestMethod]
        public void Injected_WithInjectedStringInt()
        {
            // Arrange
            Container.RegisterType<OtherService>(
                new InjectionMethod(nameof(OtherService.MethodTwo),
                    Inject.Parameter("test"),
                    Inject.Parameter(1)));

            // Act
            var result = Container.Resolve<OtherService>();

            // Assert
            Assert.IsNotNull(result.Value);
            Assert.IsInstanceOfType(result.Value, typeof(object));
            Assert.IsNotNull(result.ValueOne);
            Assert.IsInstanceOfType(result.ValueOne, typeof(string));
            Assert.IsNotNull(result.ValueTwo);
            Assert.IsInstanceOfType(result.ValueTwo, typeof(int));
        }
    }
}
