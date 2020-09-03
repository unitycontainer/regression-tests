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
        [TestMethod]
        public void Parameters_NoParameters()
        {
            // Act
            var result = Container.Resolve<NoParametersCtor>();

            // Assert
            Assert.AreEqual("none", result.Value);
        }


        [TestMethod]
        public void Parameters_NoAttribute()
        {
            // Arrange
            Container.RegisterInstance(Name);

            // Act
            var result = Container.Resolve<NoAttributeParameterCtor>();

            // Assert
            Assert.AreEqual(Name, result.Value);
        }

        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public void Parameters_NoAttributeNegative()
        {
            // Act
            var result = Container.Resolve<NoAttributeParameterCtor>();

            // Assert
            Assert.AreEqual(Name, result.Value);
        }


        [TestMethod]
        public void Parameters_NoAttributeWithDefault()
        {
            // Arrange
            Container.RegisterInstance(Name);

            // Act
            var result = Container.Resolve<NoAttributeWithDefaultCtor>();

            // Assert
            Assert.AreEqual(Name, result.Value);
        }

        [TestMethod]
        public void Parameters_NoAttributeWithDefaultNegative()
        {
            // Act
            var result = Container.Resolve<NoAttributeWithDefaultCtor>();

            // Assert
            Assert.AreEqual(DefaultString, result.Value);
        }


        [TestMethod]
        public void Parameters_NoAttributeWithDefaultValue()
        {
            // Arrange
            Container.RegisterInstance(typeof(int), 1);

            // Act
            var result = Container.Resolve<NoAttributeWithDefaultValueCtor>();

            // Assert
            Assert.AreEqual(1, result.Value);
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
            Container.RegisterInstance(typeof(string), null);

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
    }
}
