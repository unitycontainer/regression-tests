﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
#if NET45
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
        public void Overrides_DependencyAttribute()
        {
            // Arrange
            Container.RegisterType<Service>(new InjectionMethod(nameof(Service.DependencyAttribute)));

            // Act
            var result = Container.Resolve<Service>();

            // Assert
            Assert.AreEqual(result.Called, 2);
            Assert.IsInstanceOfType(result.Value, typeof(object));
        }

        [TestMethod]
        public void Overrides_NamedDependencyAttribute()
        {
            // Arrange
            Container.RegisterType<Service>(new InjectionMethod(nameof(Service.NamedDependencyAttribute)));

            // Act
            var result = Container.Resolve<Service>();

            // Assert
            Assert.AreEqual(result.Called, 3);
            Assert.IsInstanceOfType(result.Value, typeof(string));
            Assert.AreEqual(result.Value, Name);
        }

        [TestMethod]
        public void Overrides_DependencyAttributeWithDefaultInt()
        {
            // Arrange
            Container.RegisterType<Service>(new InjectionMethod(nameof(Service.DependencyAttributeWithDefaultInt)));

            // Act
            var result = Container.Resolve<Service>();

            // Assert
            Assert.AreEqual(result.Called, 10);
            Assert.AreEqual(result.Value, Service.DefaultInt);
        }

        [TestMethod]
        public void Overrides_NamedDependencyAttributeWithDefaultInt()
        {
            // Arrange
            Container.RegisterType<Service>(new InjectionMethod(nameof(Service.NamedDependencyAttributeWithDefaultInt)));

            // Act
            var result = Container.Resolve<Service>();

            // Assert
            Assert.AreEqual(result.Called, 11);
            Assert.AreEqual(result.Value, Service.DefaultInt);
        }

        [TestMethod]
        public void Overrides_DependencyAttributeWithDefaultNullUnresolved()
        {
            // Arrange
            Container.RegisterType<Service>(new InjectionMethod(nameof(Service.DependencyAttributeWithDefaultNullUnresolved)));

            // Act
            var result = Container.Resolve<Service>();

            // Assert
            Assert.AreEqual(result.Called, 16);
            Assert.IsNull(result.Value);
        }

    }
}