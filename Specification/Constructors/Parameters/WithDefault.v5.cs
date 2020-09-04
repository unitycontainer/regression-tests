using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity;

namespace Specification
{
    /// <summary>
    /// Tests in this file are only supported in Unity v5 and up
    /// </summary>
    public partial class Constructors
    {
        [TestMethod]
        public void Parameters_NoAttributeWithDefaultNegative()
        {
            // Act
            var result = Container.Resolve<NoAttributeWithDefaultCtor>();

            // Assert
            Assert.AreEqual(DefaultString, result.Value);
        }

        [TestMethod]
        public void Parameters_NoAttributeWithDefaultValueNegative()
        {
            // Act
            var result = Container.Resolve<NoAttributeWithDefaultValueCtor>();

            // Assert
            Assert.AreEqual(DefaultInt, result.Value);
        }

        [TestMethod]
        public void Parameters_NoAttributeWithDefaultNull()
        {
            // Arrange
            Container.RegisterInstance(typeof(string), (object)null);

            // Act
            var result = Container.Resolve<NoAttributeWithDefaultNullCtor>();

            // Assert
            Assert.AreEqual(null, result.Value);
        }

        [TestMethod]
        public void Parameters_NoAttributeWithDefaultNullNegative()
        {
            // Act
            var result = Container.Resolve<NoAttributeWithDefaultNullCtor>();

            // Assert
            Assert.AreEqual(null, result.Value);
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
            Container.RegisterInstance(typeof(string), (object)null);

            // Act
            var result = Container.Resolve<OptionalWithDefaultNullCtor>();

            // Assert
            Assert.AreEqual(null, result.Value);
        }

        [TestMethod]
        public void Parameters_RequiredWithDefaultNegative()
        {
            // Act
            var result = Container.Resolve<DependencyWithDefaultCtor>();

            // Assert
            Assert.AreEqual(DefaultString, result.Value);
        }

        [TestMethod]
        public void Parameters_RequiredWithDefaultValueNegative()
        {
            // Act
            var result = Container.Resolve<DependencyWithDefaultValueCtor>();

            // Assert
            Assert.AreEqual(DefaultInt, result.Value);
        }

        [TestMethod]
        public void Parameters_RequiredWithDefaultNull()
        {
            // Arrange
            Container.RegisterInstance(typeof(string), (object)null);

            // Act
            var result = Container.Resolve<DependencyWithDefaultNullCtor>();

            // Assert
            Assert.AreEqual(null, result.Value);
        }

        [TestMethod]
        public void Parameters_RequiredWithDefaultNullNegative()
        {
            // Act
            var result = Container.Resolve<DependencyWithDefaultNullCtor>();

            // Assert
            Assert.AreEqual(null, result.Value);
        }

        [TestMethod]
        public void Parameters_RequiredUnNamedWithDefault()
        {
            // Arrange
            Container.RegisterInstance(Name);

            // Act
            var result = Container.Resolve<DependencyNamedWithDefaultCtor>();

            // Assert
            Assert.AreEqual(DefaultString, result.Value);
        }

        [TestMethod]
        public void Parameters_RequiredNamedWithDefaultNegative()
        {
            // Act
            var result = Container.Resolve<DependencyNamedWithDefaultCtor>();

            // Assert
            Assert.AreEqual(DefaultString, result.Value);
        }
    }
}
