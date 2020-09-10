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
        #region Required 

        /// <summary>
        /// This test resolves type annotated for injection from empty container.
        /// The test covers named as well as anonymous dependencies
        /// </summary>
        /// <example>
        /// 
        /// public class TypeWithRequiredDependency
        /// {
        ///     [Dependency]
        ///     public int Field;
        /// 
        ///     [Dependency(name)]
        ///     public int Property { get; set; }
        /// 
        ///     [InjectionConstructor]
        ///     public TypeWithRequiredDependency([Dependency] int value) { }
        /// 
        ///     [InjectionMethod]
        ///     public void Method([Dependency(name)] int value) { }
        /// }
        /// 
        /// /////////////////////////////////////
        /// 
        ///  var result = new UnityContainer()
        ///      .Resolve(typeof(TypeWithRequiredDependency));
        ///      
        /// </example>
        /// <param name="name">Name of the <see cref="Type"/> to resolve</param>
        [DataTestMethod]
        [DataRow("Required_Dependency_Value")]
        [DataRow("Required_Dependency_Class")]
        [DataRow("Required_Dependency_Value_Named")]
        [DataRow("Required_Dependency_Class_Named")]
        [ExpectedException(typeof(ResolutionFailedException))]
        public void Unregistered_Required(string name)
        {
            // Arrange
            var type = TargetType(name);

            // Act
            _ = Container.Resolve(type);
        }

        /// <summary>
        /// This test resolves type annotated for injection from fully initialized container.
        /// The test covers named as well as anonymous dependencies
        /// </summary>
        /// <example>
        /// 
        /// public class TypeWithRequiredDependency
        /// {
        ///     [Dependency(name)]
        ///     public int Field;
        /// 
        ///     [Dependency]
        ///     public int Property { get; set; }
        /// 
        ///     [InjectionConstructor]
        ///     public TypeWithRequiredDependency([Dependency(name)] int value) { }
        /// 
        ///     [InjectionMethod]
        ///     public void Method([Dependency] int value) { }
        /// }
        /// 
        /// /////////////////////////////////////
        /// 
        /// var container = new UnityContainer()
        ///     .RegisterInstance(111);
        ///     
        ///  var result = new UnityContainer()
        ///      .Resolve(typeof(TypeWithRequiredDependency));
        ///      
        /// </example>
        /// <param name="name">Name of the <see cref="Type"/> to resolve</param>
        /// <param name="expected">Expected injected dependency value</param>
        [DataTestMethod]
        [DynamicData(nameof(Registered_Required_Data))]
        public void Registered_Required(string name, object expected)
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
        /// This test resolves type with default values and annotated for mandatory
        /// injection from empty container.
        /// </summary>
        /// <example>
        /// 
        /// public class RequiredDependencyWithDefaults
        /// {
        ///     [Dependency]
        ///     public int Field = 11;
        /// 
        ///     [Dependency]
        ///     public int Property { get; set; } = 22
        /// 
        ///     [InjectionConstructor]
        ///     public RequiredDependencyWithDefaults([Dependency] int value = 33) { }
        /// 
        ///     [InjectionMethod]
        ///     public void Method([Dependency] int value = 33) { }
        /// }
        /// 
        /// /////////////////////////////////////
        /// 
        ///  var result = new UnityContainer()
        ///      .Resolve(typeof(TypeWithRequiredDependency));
        ///      
        /// </example>
        /// <param name="name">Name of the <see cref="Type"/> to resolve</param>
        /// <param name="expected">Expected injected dependency value</param>
        [DataTestMethod]
        [DynamicData(nameof(Required_WithDefault_Data))]
        public virtual void Unregistered_Required_WithDefault(string name, object _, object expected)
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
        /// This test resolves type with default values and annotated for mandatory
        /// injection from fully initialized container.
        /// </summary>
        /// <example>
        /// 
        /// public class RequiredDependencyWithDefaults
        /// {
        ///     [Dependency]
        ///     public int Field = 11;
        /// 
        ///     [Dependency]
        ///     public int Property { get; set; } = 22
        /// 
        ///     [InjectionConstructor]
        ///     public RequiredDependencyWithDefaults([Dependency] int value = 33) { }
        /// 
        ///     [InjectionMethod]
        ///     public void Method([Dependency] int value = 33) { }
        /// }
        /// 
        /// /////////////////////////////////////
        /// 
        /// var container = new UnityContainer()
        ///     .RegisterInstance(111);
        ///     
        ///  var result = new UnityContainer()
        ///      .Resolve(typeof(RequiredDependencyWithDefaults));
        ///      
        /// </example>
        /// <param name="name">Name of the <see cref="Type"/> to resolve</param>
        /// <param name="expected">Expected injected dependency value</param>
        [DataTestMethod]
        [DynamicData(nameof(Required_WithDefault_Data))]
        public void Registered_Required_WithDefault(string name, object expected, object _)
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
