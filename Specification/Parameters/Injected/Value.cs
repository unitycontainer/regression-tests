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
        public void Injected_Baseline()
        {
            // Arrange
            Container.RegisterType<InjectedType>(
                new InjectionMethod(nameof(InjectedType.Method), typeof(object)));

            // Act
            var result = Container.Resolve<InjectedType>();

            // Assert
            Assert.IsNotNull(result.Value);
            Assert.IsInstanceOfType(result.Value, typeof(object));
        }

        [TestMethod]
        public void Injected_WithObject()
        {
            var data = "value";

            // Arrange
            Container.RegisterType<InjectedType>(
                new InjectionMethod(nameof(InjectedType.Method), new InjectionParameter(data)));

            // Act
            var result = Container.Resolve<InjectedType>();

            // Assert
            Assert.IsNotNull(result.Value);
            Assert.AreSame(result.Value, data);
        }


        [TestMethod]
        public void Injected_WithObject_ByType()
        {
            var data = "value";

            // Arrange
            Container.RegisterType<InjectedType>(
                new InjectionMethod(nameof(InjectedType.Method), new InjectionParameter(typeof(object), data)));

            // Act
            var result = Container.Resolve<InjectedType>();

            // Assert
            Assert.IsNotNull(result.Value);
            Assert.AreSame(result.Value, data);
        }

        [TestMethod]
        public void Injected_WithInt()
        {
            // Arrange
            Container.RegisterType<InjectedType>(
                new InjectionMethod(nameof(InjectedType.MethodOne), new InjectionParameter(1)));

            // Act
            var result = Container.Resolve<InjectedType>();

            // Assert
            Assert.IsNotNull(result.ValueOne);
            Assert.IsInstanceOfType(result.ValueOne, typeof(int));
        }

        [TestMethod]
        public void Injected_WithStringAsObject()
        {
            // Arrange
            Container.RegisterType<InjectedType>(
                new InjectionMethod(nameof(InjectedType.MethodOne), new InjectionParameter(typeof(object), Name)));

            // Act
            var result = Container.Resolve<InjectedType>();

            // Assert
            Assert.IsNotNull(result.ValueOne);
            Assert.IsNull(result.ValueTwo);
            Assert.IsInstanceOfType(result.ValueOne, typeof(string));
        }

        [TestMethod]
        public void Injected_WithIntAndString()
        {
            // Arrange
            Container.RegisterType<InjectedType>(
                new InjectionMethod(nameof(InjectedType.MethodTwo), new InjectionParameter(1),
                                                                    new InjectionParameter("test")));

            // Act
            var result = Container.Resolve<InjectedType>();

            // Assert
            Assert.IsNotNull(result.ValueOne);
            Assert.IsInstanceOfType(result.ValueOne, typeof(int));
            Assert.IsNotNull(result.ValueTwo);
            Assert.IsInstanceOfType(result.ValueTwo, typeof(string));
        }

        [TestMethod]
        public void Injected_WithStringAndInt()
        {
            // Arrange
            Container.RegisterType<InjectedType>(
                new InjectionMethod(nameof(InjectedType.MethodTwo), new InjectionParameter("test"),
                                                                    new InjectionParameter(1)));
            // Act
            var result = Container.Resolve<InjectedType>();

            // Assert
            Assert.IsNotNull(result.ValueOne);
            Assert.IsInstanceOfType(result.ValueOne, typeof(string));
            Assert.IsNotNull(result.ValueTwo);
            Assert.IsInstanceOfType(result.ValueTwo, typeof(int));
        }
    }
}
