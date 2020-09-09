using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
#if NET45
using Microsoft.Practices.Unity;
#else
using Unity;
#endif

namespace Specification
{
    public abstract partial class VerificationPattern
    {
        [DataTestMethod]
        [DynamicData(nameof(Required_Data))]
        [ExpectedException(typeof(ResolutionFailedException))]
        public virtual void Unregistered_Required_Dependency(string name, object expected)
        {
            // Arrange
            var type = TargetType(name);

            // Act
            _ = Container.Resolve(type);
        }

        [DataTestMethod]
        [DynamicData(nameof(Required_Data))]
        public virtual void Registered_Required_Dependency(string name, object expected)
        {
            // Arrange
            RegisterTypes();
            var type = TargetType(name);

            // Act
            var instance = Container.Resolve(type) as PatternBase;

            // Validate
            Assert.IsNotNull(instance);
            Assert.AreEqual(expected, instance.Value);
        }


        [DataTestMethod]
        [DynamicData(nameof(Required_WithDefault_Data))]
        public virtual void Unregistered_Required_WithDefault(string name, object registered, object @default)
        {
            // Arrange
            var type = TargetType(name);
            // Act
            var instance = Container.Resolve(type) as PatternBase;

            // Validate
            Assert.IsNotNull(instance);
            Assert.AreEqual(@default, instance.Value);
        }

        [DataTestMethod]
        [DynamicData(nameof(Required_WithDefault_Data))]
        public virtual void Registered_Required_WithDefault(string name, object registered, object @default)
        {
            // Arrange
            RegisterTypes();
            var type = TargetType(name);

            // Act
            var instance = Container.Resolve(type) as PatternBase;

            // Validate
            Assert.IsNotNull(instance);
            Assert.AreEqual(registered, instance.Value);
        }
    }
}
