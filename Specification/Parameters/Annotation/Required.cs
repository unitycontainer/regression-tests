﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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
        public void Annotation_Dependency()
        {
            // Arrange
            Container.RegisterType<Service>(new InjectionMethod(nameof(Service.DependencyAttribute), typeof(object)));

            // Act
            var result = Container.Resolve<Service>();

            // Assert
            Assert.AreEqual(result.Called, 2);
            Assert.IsInstanceOfType(result.Value, typeof(object));
        }
    }
}