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
    public partial class Constructors
    {
        #region Optional

        [TestMethod]
        public void Parameters_Optional()
        {
            // Arrange
            Container.RegisterInstance(Name);

            // Act
            var result = Container.Resolve<OptionalParameterCtor>();

            // Assert
            Assert.AreEqual(Name, result.Value);
        }

        [TestMethod]
        public void Parameters_OptionalNegative()
        {
            // Act
            var result = Container.Resolve<OptionalParameterCtor>();

            // Assert
            Assert.AreEqual(null, result.Value);
        }

        [TestMethod]
        public void Parameters_OptionalWithDefault()
        {
            // Arrange
            Container.RegisterInstance(Name);

            // Act
            var result = Container.Resolve<OptionalWithDefaultValueCtor>();

            // Assert
            Assert.AreEqual(Name, result.Value);
        }

        [TestMethod]
        public void Parameters_OptionalWithDefaultNullNegative()
        {
            // Act
            var result = Container.Resolve<OptionalWithDefaultNullCtor>();

            // Assert
            Assert.AreEqual(null, result.Value);
        }

        #endregion

        #region Named

        [TestMethod]
        public void Parameters_OptionalNamed()
        {
            // Arrange
            Container.RegisterInstance(Name, Name);

            // Act
            var result = Container.Resolve<OptionalNamedParameterCtor>();

            // Assert
            Assert.AreEqual(Name, result.Value);
        }

        [TestMethod]
        public void Parameters_OptionalUnNamed()
        {
            // Arrange
            Container.RegisterInstance(Name);

            // Act
            var result = Container.Resolve<OptionalNamedParameterCtor>();

            // Assert
            Assert.AreEqual(null, result.Value);
        }

        [TestMethod]
        public void Parameters_OptionalNamedNegative()
        {
            // Act
            var result = Container.Resolve<OptionalNamedParameterCtor>();

            // Assert
            Assert.AreEqual(null, result.Value);
        }

        [TestMethod]
        public void Parameters_OptionalNamedWithDefault()
        {
            // Arrange
            Container.RegisterInstance(Name, Name);

            // Act
            var result = Container.Resolve<OptionalNamedWithDefaultCtor>();

            // Assert
            Assert.AreEqual(Name, result.Value);
        }

        #endregion
    }
}
