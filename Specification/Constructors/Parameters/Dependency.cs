﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
#if V4
using Microsoft.Practices.Unity;
#else
using Unity;
using Unity.Resolution;
#endif

namespace Specification
{
    public partial class Constructors
    {
        #region Required

        [TestMethod]
        public void Parameters_Required()
        {
            // Arrange
            Container.RegisterInstance(Name);

            // Act
            var result = Container.Resolve<DependencyParameterCtor>();

            // Assert
            Assert.AreEqual(Name, result.Value);
        }

        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public void Parameters_RequiredNegative()
        {
            // Act
            var result = Container.Resolve<DependencyParameterCtor>();

            // Assert
            Assert.AreEqual(Name, result.Value);
        }

        [TestMethod]
        public void Parameters_RequiredWithDefault()
        {
            // Arrange
            Container.RegisterInstance(Name);

            // Act
            var result = Container.Resolve<DependencyWithDefaultCtor>();

            // Assert
            Assert.AreEqual(Name, result.Value);
        }

        [TestMethod]
        public void Parameters_RequiredWithDefaultValue()
        {
            // Arrange
            Container.RegisterInstance(typeof(int), 1);

            // Act
            var result = Container.Resolve<DependencyWithDefaultValueCtor>();

            // Assert
            Assert.AreEqual(1, result.Value);
        }

        #endregion

        #region Named

        [TestMethod]
        public void Parameters_RequiredNamed()
        {
            // Arrange
            Container.RegisterInstance(Name, Name);

            // Act
            var result = Container.Resolve<DependencyNamedParameterCtor>();

            // Assert
            Assert.AreEqual(Name, result.Value);
        }

        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public void Parameters_RequiredUnNamed()
        {
            // Arrange
            Container.RegisterInstance(Name);

            // Act
            var result = Container.Resolve<DependencyNamedParameterCtor>();

            // Assert
            Assert.AreEqual(Name, result.Value);
        }

        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public void Parameters_RequiredNamedNegative()
        {
            // Act
            var result = Container.Resolve<DependencyNamedParameterCtor>();

            // Assert
            Assert.AreEqual(Name, result.Value);
        }

        [TestMethod]
        public void Parameters_RequiredNamedWithDefault()
        {
            // Arrange
            Container.RegisterInstance(Name, Name);

            // Act
            var result = Container.Resolve<DependencyNamedWithDefaultCtor>();

            // Assert
            Assert.AreEqual(Name, result.Value);
        }

        #endregion
    }
}
