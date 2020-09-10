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
    public abstract partial class VerificationPattern
    {
        #region Optional

        /// <summary>
        /// This test resolves type annotated for optional injection from empty container.
        /// The test covers named as well as anonymous dependencies
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
        ///  var result = new UnityContainer()
        ///      .Resolve(typeof(TypeWithOptionalDependency));
        ///      
        /// </example>
        /// <param name="name">Name of the <see cref="Type"/> to resolve</param>
        /// <param name="expected">Expected injected dependency value</param>
        [DataTestMethod]
        [DynamicData(nameof(Optional_Data))]
        public virtual void Unregistered_Optional(string name, object _, object expected)
        {
            // Arrange
            var type = TargetType(name);

            // Act
            var instance = Container.Resolve(type) as PatternBase;

            // Validate
            Assert.IsNotNull(instance);
            Assert.AreEqual(expected, instance.Value);
        }

        /// <summary>
        /// This test resolves type annotated for optional injection from fully initialized container.
        /// The test covers named as well as anonymous dependencies
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
        /// 
        /// var container = new UnityContainer()
        ///     .RegisterInstance(111);
        ///     
        ///  var result = new UnityContainer()
        ///      .Resolve(typeof(TypeWithOptionalDependency));
        ///      
        /// </example>
        /// <param name="name">Name of the <see cref="Type"/> to resolve</param>
        /// <param name="expected">Expected injected dependency value</param>
        [DataTestMethod]
        [DynamicData(nameof(Optional_Data))]
        public virtual void Registered_Optional(string name, object expected, object _)
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


        public static IEnumerable<object[]> Optional_Data
        {
            get
            {
                yield return new object[] { "Optional_Dependency_Class",       Singleton, null };
#if !V4
                yield return new object[] { "Optional_Dependency_Value",       RegisteredInt, 0 };
                yield return new object[] { "Optional_Dependency_Value_Named", NamedInt, 0 };
                yield return new object[] { "Optional_Dependency_Class_Named", NamedSingleton, null };
#endif
            }
        }

        #endregion


        #region With Defaults

#if !V4
        /// <summary>
        /// This test resolves type with default values and annotated for optional
        /// injection from empty container.
        /// </summary>
        /// <example>
        /// 
        /// public class TypeWithOptionalDependency
        /// {
        ///     [OptionalDependency]
        ///     public int Field;
        /// 
        ///     [OptionalDependency]
        ///     public int Property { get; set; }
        /// 
        ///     [InjectionConstructor]
        ///     public TypeWithOptionalDependency([OptionalDependency] int value) { }
        /// 
        ///     [InjectionMethod]
        ///     public void Method([OptionalDependency] int value) { }
        /// }
        /// 
        /// /////////////////////////////////////
        /// 
        ///  var result = new UnityContainer()
        ///      .Resolve(typeof(TypeWithOptionalDependency));
        ///      
        /// </example>
        /// <param name="name">Name of the <see cref="Type"/> to resolve</param>
        /// <param name="expected">Expected injected dependency value</param>
        [DataTestMethod]
        [DynamicData(nameof(Optional_WithDefault_Data))]
        public virtual void Unregistered_Optional_WithDefault(string name, object _, object expected)
        {
            // Arrange
            var type = TargetType(name);
            // Act
            var instance = Container.Resolve(type) as PatternBase;

            // Validate
            Assert.IsNotNull(instance);
            Assert.AreEqual(expected, instance.Value);
        }
#endif

        /// <summary>
        /// This test resolves type with default values and annotated for optional
        /// injection from fully initialized container.
        /// </summary>
        /// <example>
        /// 
        /// public class TypeWithOptionalDependency
        /// {
        ///     [OptionalDependency]
        ///     public int Field;
        /// 
        ///     [OptionalDependency]
        ///     public int Property { get; set; }
        /// 
        ///     [InjectionConstructor]
        ///     public TypeWithOptionalDependency([OptionalDependency] int value) { }
        /// 
        ///     [InjectionMethod]
        ///     public void Method([OptionalDependency] int value) { }
        /// }
        /// 
        /// /////////////////////////////////////
        /// 
        /// var container = new UnityContainer()
        ///     .RegisterInstance(111);
        ///     
        ///  var result = new UnityContainer()
        ///      .Resolve(typeof(TypeWithOptionalDependency));
        ///      
        /// </example>
        /// <param name="name">Name of the <see cref="Type"/> to resolve</param>
        /// <param name="expected">Expected injected dependency value</param>
        [DataTestMethod]
        [DynamicData(nameof(Optional_WithDefault_Data))]
        public virtual void Registered_Optional_WithDefault(string name, object expected, object _)
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

        public static IEnumerable<object[]> Optional_WithDefault_Data
        {
            get
            {
#if !V4
                yield return new object[] { "Optional_WithDefault_Value", RegisteredInt,    DefaultInt };
#endif
                yield return new object[] { "Optional_WithDefault_Class", RegisteredString, DefaultString };
            }
        }

        #endregion
    }
}
