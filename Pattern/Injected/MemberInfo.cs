using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
#if NET45
using Microsoft.Practices.Unity;
#else
using Unity;
using Unity.Injection;
#endif

namespace Specification
{
    public abstract partial class VerificationPattern
    {
        /// <summary>
        /// This test uses <see cref="InjectionMember"/> to configure and resolve 
        /// dependencies from empty container.
        /// </summary>
        /// <example>
        /// 
        /// public class PocoType 
        /// {
        ///     public int Field;
        /// 
        ///     public int Property { get; set; }
        /// 
        ///     public PocoType(int value) { }
        ///     
        ///     public void Method(int value) { }
        /// }
        /// 
        /// /////////////////////////////////////
        /// 
        ///  var container = new UnityContainer()
        ///      .RegisterType(typeof(PocoType), new InjectionConstructor(typeof(int)),
        ///                                   new InjectionField("Field", typeof(int)),
        ///                             new InjectionProperty("Property", typeof(int)),
        ///                                 new InjectionMethod("Method", typeof(int)));
        ///      
        /// var result = container.Resolve(typeof(PocoType));
        /// </example>
        /// <param name="name">Name of the <see cref="Type"/> to resolve</param>
        /// <param name="dependency"><see cref="Type"/> of dependency</param>
        [DataTestMethod]
        [DataRow("NoDefault_Value",                 typeof(int))]
        [DataRow("NoDefault_Class",                 typeof(Unresolvable))]
        [DataRow("WithDefault_Value",               typeof(int))]
        [DataRow("WithDefault_Class",               typeof(string))]

        [DataRow("Required_Dependency_Value",       typeof(int))]
        [DataRow("Required_Dependency_Class",       typeof(Unresolvable))]
        [DataRow("Required_Dependency_Value_Named", typeof(int))]
        [DataRow("Required_Dependency_Class_Named", typeof(Unresolvable))]
        [DataRow("Required_WithDefault_Value",      typeof(int))]
        [DataRow("Required_WithDefault_Class",      typeof(string))]

        [DataRow("Optional_Dependency_Value",       typeof(int))]
        [DataRow("Optional_Dependency_Class",       typeof(Unresolvable))]
        [DataRow("Optional_Dependency_Value_Named", typeof(int))]
        [DataRow("Optional_Dependency_Class_Named", typeof(Unresolvable))]
        [DataRow("Optional_WithDefault_Value",      typeof(int))]
        [DataRow("Optional_WithDefault_Class",      typeof(string))]
        [ExpectedException(typeof(ResolutionFailedException))]
        public virtual void Unregistered_Injected_Dependency(string name, Type dependency)
        {
            // Arrange
            var type = TargetType(name);
            Container.RegisterType(type, GetInjectionMember(dependency));

            // Act
            var result = Container.Resolve(type);
        }

        [DataTestMethod]
        [DynamicData(nameof(Optional_Injected_ByType_Data))]
        public virtual void Unregistered_Optional_Injected_Dependency(string name, Type dependency, object expected)
        {
            // Arrange
            var type = TargetType(name);
            Container.RegisterType(type, GetInjectionMember(dependency));

            // Act
           // Act
            var instance = Container.Resolve(type) as PatternBase;

            // Validate
            Assert.IsNotNull(instance);
            Assert.AreEqual(expected, instance.Value);
        }

        [DataTestMethod]
        [DynamicData(nameof(Injected_ByType_Data))]
        [DynamicData(nameof(Required_Injected_ByType_Data))]
        [DynamicData(nameof(Implicitly_Resolved_Registered_Optional_Data))]
        public virtual void Registered_Implicitly_Injected_Dependency(string name, object data, object expected)
        {
            // Arrange
            RegisterTypes();
            var type = TargetType(name);
            Container.RegisterType(type, GetInjectionMember(data));

            // Act
            var instance = Container.Resolve(type) as PatternBase;

            // Validate
            Assert.IsNotNull(instance);
            Assert.AreEqual(expected, instance.Value);
        }



        [DataTestMethod]
        [DynamicData(nameof(Injected_Data))]
        public virtual void Unregistered_Injected_Dependency_Dependency(string name, object data, object expected)
        {
            // Arrange
            var type = TargetType(name);
            Container.RegisterType(type, GetInjectionMember(data));
            // Act
            var instance = Container.Resolve(type) as PatternBase;

            // Validate
            Assert.IsNotNull(instance);
            Assert.AreEqual(expected, instance.Value);
        }

        [DataTestMethod]
        [DynamicData(nameof(Injected_Data))]
        public virtual void Registered_Overriden_ByInjected_Dependency(string name, object data, object expected)
        {
            // Arrange
            RegisterTypes();
            var type = TargetType(name);
            Container.RegisterType(type, GetInjectionMember(data));
            // Act
            var instance = Container.Resolve(type) as PatternBase;

            // Validate
            Assert.IsNotNull(instance);
            Assert.AreEqual(expected, instance.Value);
        }


        [DataTestMethod]
        [DynamicData(nameof(Injected_WithDefault_Data))]
        public virtual void Unregistered_Default_Overriden_ByInjected_Dependency(string name, object data, object expected)
        {
            // Arrange
            var type = TargetType(name);
            Container.RegisterType(type, GetInjectionMember(data));
            // Act
            var instance = Container.Resolve(type) as PatternBase;

            // Validate
            Assert.IsNotNull(instance);
            Assert.AreEqual(expected, instance.Value);
        }
    }
}
