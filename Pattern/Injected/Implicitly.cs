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
        [DynamicData(nameof(Implicitly_Resolved_Data))]
        [DynamicData(nameof(Implicitly_Resolved_Required_Data))]
        [ExpectedException(typeof(ResolutionFailedException))]
        public virtual void Unregistered_Implicitly_Injected(string name, Type dependency, object expected)
        {
            // Arrange
            var type = TargetType(name);
            Container.RegisterType(type, GetInjectedMember(dependency));

            // Act
            var result = Container.Resolve(type);
        }

        [DataTestMethod]
        [DynamicData(nameof(Implicitly_Resolved_Optional_Data))]
        public virtual void Unregistered_Implicitly_Injected_Optional(string name, Type dependency, object expected)
        {
            // Arrange
            var type = TargetType(name);
            Container.RegisterType(type, GetInjectedMember(dependency));

            // Act
           // Act
            var instance = Container.Resolve(type) as PatternBase;

            // Validate
            Assert.IsNotNull(instance);
            Assert.AreEqual(expected, instance.Value);
        }

        [DataTestMethod]
        [DynamicData(nameof(Implicitly_Resolved_Data))]
        [DynamicData(nameof(Implicitly_Resolved_Required_Data))]
        [DynamicData(nameof(Implicitly_Resolved_Registered_Optional_Data))]
        public virtual void Registered_Implicitly_Injected(string name, object data, object expected)
        {
            // Arrange
            RegisterTypes();
            var type = TargetType(name);
            Container.RegisterType(type, GetInjectedMember(data));

            // Act
            var instance = Container.Resolve(type) as PatternBase;

            // Validate
            Assert.IsNotNull(instance);
            Assert.AreEqual(expected, instance.Value);
        }



        [DataTestMethod]
        [DynamicData(nameof(Injected_Data))]
        public virtual void Unregistered_Injected_Dependency(string name, object data, object expected)
        {
            // Arrange
            var type = TargetType(name);
            Container.RegisterType(type, GetInjectedMember(data));
            // Act
            var instance = Container.Resolve(type) as PatternBase;

            // Validate
            Assert.IsNotNull(instance);
            Assert.AreEqual(expected, instance.Value);
        }

        [DataTestMethod]
        [DynamicData(nameof(Injected_Data))]
        public virtual void Registered_Overriden_ByInjected(string name, object data, object expected)
        {
            // Arrange
            RegisterTypes();
            var type = TargetType(name);
            Container.RegisterType(type, GetInjectedMember(data));
            // Act
            var instance = Container.Resolve(type) as PatternBase;

            // Validate
            Assert.IsNotNull(instance);
            Assert.AreEqual(expected, instance.Value);
        }


        [DataTestMethod]
        [DynamicData(nameof(Injected_WithDefault_Data))]
        public virtual void Unregistered_Default_Overriden_ByInjected(string name, object data, object expected)
        {
            // Arrange
            var type = TargetType(name);
            Container.RegisterType(type, GetInjectedMember(data));
            // Act
            var instance = Container.Resolve(type) as PatternBase;

            // Validate
            Assert.IsNotNull(instance);
            Assert.AreEqual(expected, instance.Value);
        }
    }
}
