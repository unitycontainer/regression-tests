using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
#if V4
using Microsoft.Practices.Unity;
#else
using Unity;
using Unity.Injection;
using Unity.Resolution;
#endif

namespace Specification
{
    public partial class Parameters
    {
        [TestMethod]
        public void Resolved_AttributedMethodWithValue()
        {
            // Arrange
            Container.RegisterInstance(10);
            Container.RegisterInstance("1", 1);
            Container.RegisterInstance("2", 2);
            Container.RegisterInstance("1", "1");
            Container.RegisterInstance("2", "2");
            Container.RegisterType<Service>(
                new InjectionMethod(nameof(Service.Method),
                    Resolve.Parameter<string>()));

            // Act
            var result = Container.Resolve<Service>();

            // Assert
            Assert.IsNotNull(result.ValueOne);
            Assert.AreSame(result.ValueOne, Container.Resolve<string>());
        }

        [TestMethod]
        public void Resolved_MethodWithResolvedInt()
        {
            // Arrange
            Container.RegisterInstance(10);
            Container.RegisterInstance("1", 1);
            Container.RegisterInstance("2", 2);
            Container.RegisterInstance("1", "1");
            Container.RegisterInstance("2", "2");
            Container.RegisterType<Service>(
                new InjectionMethod(nameof(Service.Method),
                    Resolve.Parameter<int>()));

            // Act
            var result = Container.Resolve<Service>();

            // Assert
            Assert.IsNotNull(result.ValueOne);
            Assert.AreEqual(result.ValueOne, Container.Resolve<int>());
        }

        [TestMethod]
        public void Resolved_MethodWithResolvedNamedInt()
        {
            // Arrange
            Container.RegisterInstance(10);
            Container.RegisterInstance("1", 1);
            Container.RegisterInstance("2", 2);
            Container.RegisterInstance("1", "1");
            Container.RegisterInstance("2", "2");
            Container.RegisterType<Service>(
                new InjectionMethod(nameof(Service.Method),
                    Resolve.Parameter<int>("1")));

            // Act
            var result = Container.Resolve<Service>();

            // Assert
            Assert.IsNotNull(result.ValueOne);
            Assert.AreEqual(result.ValueOne, Container.Resolve<int>("1"));
        }

        [TestMethod]
        public void Resolved_MethodWithResolvedString()
        {
            // Arrange
            Container.RegisterInstance(10);
            Container.RegisterInstance("1", 1);
            Container.RegisterInstance("2", 2);
            Container.RegisterInstance("1", "1");
            Container.RegisterInstance("2", "2");
            Container.RegisterType<Service>(
                new InjectionMethod(nameof(Service.Method),
                    Resolve.Parameter<string>()));

            // Act
            var result = Container.Resolve<Service>();

            // Assert
            Assert.IsNotNull(result.ValueOne);
            Assert.AreSame(result.ValueOne, Container.Resolve<string>());
        }

        [TestMethod]
        public void Resolved_MethodWithResolvedNamedString()
        {
            // Arrange
            Container.RegisterInstance(10);
            Container.RegisterInstance("1", 1);
            Container.RegisterInstance("2", 2);
            Container.RegisterInstance("1", "1");
            Container.RegisterInstance("2", "2");
            Container.RegisterType<Service>(
                new InjectionMethod(nameof(Service.Method),
                    Resolve.Parameter<string>("1")));

            // Act
            var result = Container.Resolve<Service>();

            // Assert
            Assert.IsNotNull(result.ValueOne);
            Assert.AreSame(result.ValueOne, Container.Resolve<string>("1"));
        }
    }
}
