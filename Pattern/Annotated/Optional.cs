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
        [DynamicData(nameof(Optional_Data))]
        public virtual void Unregistered_Optional_Dependency(string name, object registered, object @default)
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
        [DynamicData(nameof(Optional_Data))]
        public virtual void Registered_Optional_Dependency(string name, object registered, object @default)
        {
            // Arrange
            var type = TargetType(name);
            Container.RegisterInstance(Name, NamedInt)
                     .RegisterInstance(RegisteredInt)
                     .RegisterInstance(Registeredtring)
                     .RegisterInstance(Singleton)
                     .RegisterInstance(Name, NamedSingleton);
            // Act
            var instance = Container.Resolve(type) as PatternBase;

            // Validate
            Assert.IsNotNull(instance);
            Assert.AreEqual(registered, instance.Value);
        }


        [DataTestMethod]
        [DynamicData(nameof(Optional_WithDefault_Data))]
        public virtual void Unregistered_Optional_WithDefault(string name, object registered, object @default)
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
        [DynamicData(nameof(Optional_WithDefault_Data))]
        public virtual void Registered_Optional_WithDefault(string name, object registered, object @default)
        {
            // Arrange
            var type = TargetType(name);
            Container.RegisterInstance(RegisteredInt)
                     .RegisterInstance(Registeredtring)
                     .RegisterInstance(Singleton);
            // Act
            var instance = Container.Resolve(type) as PatternBase;

            // Validate
            Assert.IsNotNull(instance);
            Assert.AreEqual(registered, instance.Value);
        }
    }
}
