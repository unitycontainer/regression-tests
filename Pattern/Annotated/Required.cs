using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
#if V4
using Microsoft.Practices.Unity;
#else
using Unity;
#endif

namespace Specification
{
    /// <summary>
    /// Testing resolution of required dependencies.
    /// </summary>
    /// <example>
    /// 
    /// public class TypeWithRequiredDependency
    /// {
    ///     [Dependency]
    ///     public int Field = 44;
    /// 
    ///     [Dependency(name)]
    ///     public int Property { get; set; }
    /// 
    ///     [InjectionConstructor]
    ///     public TypeWithRequiredDependency([Dependency] int value = 55) { }
    /// 
    ///     [InjectionMethod]
    ///     public void Method([Dependency(name)] int value) { }
    /// }
    ///      
    /// </example>
    public abstract partial class VerificationPattern
    {
        #region Required 

        /// <summary>
        /// Test resolving type with required dependencies from empty container.
        /// </summary>
        /// <param name="target">Name of the <see cref="Type"/> to resolve</param>
        [DataTestMethod]
        [DataRow("Required_Dependency_Value")]
        [DataRow("Required_Dependency_Class")]
        [DataRow("Required_Dependency_Value_Named")]
        [DataRow("Required_Dependency_Class_Named")]
        [ExpectedException(typeof(ResolutionFailedException))]
        public void Unregistered_Required(string target)
        {
            // Arrange
            var type = TargetType(target);

            // Act
            _ = Container.Resolve(type);
        }

        /// <summary>
        /// Test resolving type with required dependencies from  fully initialized container.
        /// </summary>
        /// <param name="target">Name of the <see cref="Type"/> to resolve</param>
        /// <param name="expected">Expected injected dependency value</param>
        [DataTestMethod]
        [DynamicData(nameof(Registered_Required_Data))]
        public void Registered_Required(string target, object expected)
        {
            var type = TargetType(target);

            // Arrange
            RegisterTypes();

            // Act
            var instance = Container.Resolve(type) as PatternBase;

            // Validate
            Assert.IsNotNull(instance);
            Assert.AreEqual(expected, instance.Value);
        }

        public static IEnumerable<object[]> Registered_Required_Data
        {
            get
            {
                yield return new object[] { "Required_Dependency_Value",       RegisteredInt };
                yield return new object[] { "Required_Dependency_Class",       Singleton };
                yield return new object[] { "Required_Dependency_Value_Named", NamedInt };
                yield return new object[] { "Required_Dependency_Class_Named", NamedSingleton };
            }
        }

        #endregion


        #region Required with defaults

#if !V4 && !NET461
        /// <summary>
        /// Test resolving type with required dependencies and default values from empty container.
        /// </summary>
        /// <param name="target">Name of the <see cref="Type"/> to resolve</param>
        /// <param name="expected">Expected injected dependency value</param>
        [DataTestMethod]
        [DynamicData(nameof(Required_WithDefault_Data))]
        public virtual void Unregistered_Required_WithDefault(string target, object _, object expected)
        {
            // Arrange
            var type = TargetType(target);

            // Act
            var instance = Container.Resolve(type) as PatternBase;

            // Validate
            Assert.IsNotNull(instance);
            Assert.AreEqual(expected, instance.Value);
        }
#endif

        /// <summary>
        /// Test resolving type with required dependencies and default values from fully initialized container.
        /// </summary>
        /// <param name="target">Name of the <see cref="Type"/> to resolve</param>
        /// <param name="expected">Expected injected dependency value</param>
        [DataTestMethod]
        [DynamicData(nameof(Required_WithDefault_Data))]
        public void Registered_Required_WithDefault(string target, object expected, object _)
        {
            var type = TargetType(target);

            // Arrange
            RegisterTypes();

            // Act
            var instance = Container.Resolve(type) as PatternBase;

            // Validate
            Assert.IsNotNull(instance);
            Assert.AreEqual(expected, instance.Value);
        }


        public static IEnumerable<object[]> Required_WithDefault_Data
        {
            get
            {
                yield return new object[] { "Required_WithDefault_Value", RegisteredInt,    DefaultInt };
                yield return new object[] { "Required_WithDefault_Class", RegisteredString, DefaultString };
            }
        }

        #endregion
    }
}
