using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
#if V4
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
        /// Test use of <see cref="InjectionMember"/> with just a name to configure and resolve 
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
        ///      .RegisterType(typeof(PocoType), 
        ///                    new InjectionField("Field"),
        ///                    new InjectionProperty("Property"),
        ///                    new InjectionMethod("Method"));
        ///      
        /// var result = container.Resolve(typeof(PocoType));
        /// </example>
        /// <param name="target">Name of the <see cref="Type"/> to resolve</param>
        /// <param name="dependency"><see cref="Type"/> of dependency</param>
        [DataTestMethod]
        [DataRow("Implicit_Dependency_Value")]
        [DataRow("Implicit_Dependency_Class")]
        [DataRow("Required_Dependency_Value")]
        [DataRow("Required_Dependency_Class")]
        [DataRow("Required_Dependency_Value_Named")]
        [DataRow("Required_Dependency_Class_Named")]
        [ExpectedException(typeof(ResolutionFailedException))]
        public virtual void Unregistered_Injected_ByName(string target)
        {
            var type = TargetType(target);

            // Arrange
            Container.RegisterType(type, GetMemberByName());

            // Act
            _ = Container.Resolve(type);
        }

        [DataTestMethod]
        [DynamicData(nameof(Injected_ByName_WithDefault_Data))]
        public virtual void Unregistered_Injected_ByName_WithDefault(string target, object expected)
        {
            var type = TargetType(target);

            // Arrange
            Container.RegisterType(type, GetMemberByName());

            // Act
            var instance = Container.Resolve(type) as PatternBase;

            // Validate
            Assert.IsNotNull(instance);
            Assert.AreEqual(expected, instance.Value);
        }

        // Test Data
        public static IEnumerable<object[]> Injected_ByName_WithDefault_Data
        {
            get
            {
                yield return new object[] { "Implicit_WithDefault_Value",               DefaultInt };
                yield return new object[] { "Implicit_WithDefault_Class",               DefaultString };
                yield return new object[] { "Required_WithDefault_Value",      DefaultInt };
                yield return new object[] { "Required_WithDefault_Class",      DefaultString };
            }
        }

        /// <summary>
        /// Test optional injection from empty container.
        /// </summary>
        /// <example>
        /// 
        /// public class TypeWithOptionalDependency
        /// {
        ///     [OptionalDependency]
        ///     public int Field;
        /// 
        ///     [OptionalDependency(name)]
        ///     public int Property { get; set; }
        /// 
        ///     [InjectionConstructor]
        ///     public TypeWithOptionalDependency([OptionalDependency] int value) { }
        /// 
        ///     [InjectionMethod]
        ///     public void Method([OptionalDependency(name)] int value) { }
        /// }
        /// 
        /// /////////////////////////////////////
        /// 
        ///  var container = new UnityContainer()
        ///      .RegisterType(typeof(PocoType), 
        ///                    new InjectionField("Field"),
        ///                    new InjectionProperty("Property"),
        ///                    new InjectionMethod("Method"));
        ///      
        /// var result = container.Resolve(typeof(PocoType));
        ///      
        /// </example>
        /// <param name="name">Name of the <see cref="Type"/> to resolve</param>
        /// <param name="expected">Expected injected dependency value</param>
        [DataTestMethod]
        [DynamicData(nameof(Optional_Injected_ByName_Data))]
        public virtual void Unregistered_Optional_Injected_ByName(string target, object expected)
        {
            var type = TargetType(target);

            // Arrange
            Container.RegisterType(type, GetMemberByName());

            // Act
            var instance = Container.Resolve(type) as PatternBase;

            // Validate
            Assert.IsNotNull(instance);
            Assert.AreEqual(expected, instance.Value);
        }

        // Test Data
        public static IEnumerable<object[]> Optional_Injected_ByName_Data
        {
            get
            {
                yield return new object[] { "Optional_Dependency_Value",       0 };
                yield return new object[] { "Optional_Dependency_Class",       null };
                yield return new object[] { "Optional_Dependency_Value_Named", 0 };
                yield return new object[] { "Optional_Dependency_Class_Named", null };
            }
        }
    }
}
