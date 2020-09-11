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
    /// Testing optional dependencies.
    /// </summary>
    /// <example>
    /// 
    /// public class TypeWithOptionalDependencies
    /// {
    ///     [InjectionConstructor]
    ///     public TypeWithOptionalDependencies([OptionalDependency] int value)
    /// 
    ///     [OptionalDependency]
    ///     public int Field = 22;
    /// 
    ///     [OptionalDependency(name)]
    ///     public int Property { get; set; }
    /// 
    ///     [InjectionMethod]
    ///     public void Method([OptionalDependency(name)] int value = 55)
    /// }
    /// 
    /// </example>
    public abstract partial class VerificationPattern
    {
        #region Optional

        /// <summary>
        /// Test injection from empty container.
        /// </summary>
        /// <param name="target">A <see cref="Type"/> to resolve</param>
        /// <param name="expected">Expected injected value</param>
        [DataTestMethod]
        [DynamicData(nameof(Unregistered_Optional_Data))]
        public virtual void Unregistered_Optional(string target, object expected)
        {
            // Arrange
            var type = TargetType(target);

            // Act
            var instance = Container.Resolve(type) as PatternBase;

            // Validate
            Assert.IsNotNull(instance);
            Assert.AreEqual(expected, instance.Value);
        }

        public static IEnumerable<object[]> Unregistered_Optional_Data
        {
            get
            {
                yield return new object[] { "Optional_Dependency_Class",        null };
#if !V4
                yield return new object[] { "Optional_Dependency_Value",        0 };
                yield return new object[] { "Optional_Dependency_Value_Named",  0 };
                yield return new object[] { "Optional_Dependency_Class_Named",  null };
#endif
            }
        }


        /// <summary>
        /// Test optional injection from fully initialized container.
        /// </summary>
        /// <param name="target">A <see cref="Type"/> to resolve</param>
        /// <param name="expected">Expected injected value</param>
        [DataTestMethod]
        [DynamicData(nameof(Registered_Optional_Data))]
        public virtual void Registered_Optional(string target, object expected)
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

        public static IEnumerable<object[]> Registered_Optional_Data
        {
            get
            {
                yield return new object[] { "Optional_Dependency_Class",       Singleton      };
#if !V4
                yield return new object[] { "Optional_Dependency_Value",       RegisteredInt  };
                yield return new object[] { "Optional_Dependency_Value_Named", NamedInt       };
                yield return new object[] { "Optional_Dependency_Class_Named", NamedSingleton };
#endif
            }
        }

        #endregion


        #region With Defaults

#if !V4
        /// <summary>
        /// Test optional injection with defaults from empty container.
        /// </summary>
        /// <param name="target">A <see cref="Type"/> to resolve</param>
        /// <param name="expected">Expected injected value</param>
        [DataTestMethod]
#if !V4
        [DataRow("Optional_WithDefault_Value", DefaultInt)]
#endif
        [DataRow("Optional_WithDefault_Class", DefaultString)]
        public virtual void Unregistered_Optional_WithDefault(string target, object expected)
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
        /// Test optional injection with defaults from fully initialized container.
        /// </summary>
        /// <param name="target">A <see cref="Type"/> to resolve</param>
        /// <param name="expected">Expected injected value</param>
        [DataTestMethod]
#if !V4
        [DataRow("Optional_WithDefault_Value", RegisteredInt   )]
#endif
        [DataRow("Optional_WithDefault_Class", RegisteredString)]
        public virtual void Registered_Optional_WithDefault(string target, object expected)
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

        #endregion
    }
}
