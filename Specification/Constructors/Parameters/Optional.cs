using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
#if NET45
using Microsoft.Practices.Unity;
#else
using Unity;
using Unity.Resolution;
#endif

namespace Spec.Constructors
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
        public void Parameters_OptionalWithDefaultNegative()
        {
            // Act
            var result = Container.Resolve<OptionalWithDefaultValueCtor>();

            // Assert
            Assert.AreEqual(DefaultString, result.Value);
        }

        [TestMethod]
        public void Parameters_OptionalWithDefaultValue()
        {
            // Arrange
            Container.RegisterInstance(typeof(int), 1);

            // Act
            var result = Container.Resolve<OptionalWithDefaultValueCtor>();

            // Assert
            Assert.AreEqual(DefaultString, result.Value);
        }

        [TestMethod]
        public void Parameters_OptionalWithDefaultValueNegative()
        {
            // Act
            var result = Container.Resolve<OptionalWithDefaultValueCtor>();

            // Assert
            Assert.AreEqual(DefaultString, result.Value);
        }

        [TestMethod]
        public void Parameters_OptionalWithDefaultNull()
        {
            // Arrange
            Container.RegisterInstance(typeof(string), null);

            // Act
            var result = Container.Resolve<OptionalWithDefaultNullCtor>();

            // Assert
            Assert.AreEqual(null, result.Value);
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

        [TestMethod]
        public void Parameters_OptionalUnNamedWithDefault()
        {
            // Arrange
            Container.RegisterInstance(Name);

            // Act
            var result = Container.Resolve<OptionalNamedWithDefaultCtor>();

            // Assert
            Assert.AreEqual(DefaultString, result.Value);
        }

        [TestMethod]
        public void Parameters_OptionalNamedWithDefaultNegative()
        {
            // Act
            var result = Container.Resolve<OptionalNamedWithDefaultCtor>();

            // Assert
            Assert.AreEqual(DefaultString, result.Value);
        }

        #endregion
    }
}
