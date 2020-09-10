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
        #region POCO

        /// <summary>
        /// This test resolves regular POCO type from empty container.
        /// </summary>
        /// <example>
        /// 
        /// public class PocoType 
        /// {
        ///     public int Field;
        /// 
        ///     public int Property { get; set; }
        /// 
        ///     [InjectionConstructor]
        ///     public PocoType(int value) { }
        ///     
        ///     [InjectionMethod]
        ///     public void Method(int value) { }
        /// }
        /// 
        /// /////////////////////////////////////
        /// 
        ///  var result = new UnityContainer()
        ///      .Resolve(typeof(PocoType));
        ///      
        /// </example>
        /// <param name="name">Name of the <see cref="Type"/> to resolve</param>
        [DataTestMethod]
        [DataRow("NoDefault_Value")]
        [DataRow("NoDefault_Class")]
        [ExpectedException(typeof(ResolutionFailedException))]
        public virtual void Unregistered_Implicit(string name)
        {
            // Arrange
            var type = TargetType(name);

            // Act
            _ = Container.Resolve(type);
        }

        /// <summary>
        /// This test resolves POCO type with all dependencies registered.
        /// </summary>
        /// <example>
        /// 
        /// public class PocoType 
        /// {
        ///     public int Field;
        /// 
        ///     public int Property { get; set; }
        /// 
        ///     [InjectionConstructor]
        ///     public PocoType(int value) { }
        ///     
        ///     [InjectionMethod]
        ///     public void Method(int value) { }
        /// }
        /// 
        /// /////////////////////////////////////
        /// 
        /// var container = new UnityContainer()
        ///     .RegisterInstance(111);
        ///     
        /// var result = container.Resolve(typeof(PocoType));
        /// </example>
        /// <param name="name">Name of the <see cref="Type"/> to resolve</param>
        /// <param name="expected">Expected injected dependency value</param>
        [DataTestMethod]
        [DynamicData(nameof(NoDefault_Data))]
        public virtual void Registered_Implicit(string name, object expected)
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

        // Test Data
        public static IEnumerable<object[]> NoDefault_Data
        {
            get
            {
                yield return new object[] { "NoDefault_Value", RegisteredInt };
                yield return new object[] { "NoDefault_Class", Singleton };
            }
        }

        #endregion


        #region POCO with defaults

#if !V4
        /// <summary>
        /// This test resolves POCO type with default values from empty container.
        /// </summary>
        /// <example>
        /// 
        /// public class PocoTypeWithDefault
        /// {
        ///     public int Field = 555;
        /// 
        ///     public int Property { get; set; } = 444
        /// 
        ///     [InjectionConstructor]
        ///     public PocoTypeWithDefault(int value = 111) { }
        ///     
        ///     [InjectionMethod]
        ///     public void Method(int value = 222) { }
        /// }
        /// 
        /// /////////////////////////////////////
        /// 
        ///  var result = new UnityContainer()
        ///      .Resolve(typeof(PocoTypeWithDefault));
        ///      
        /// </example>
        /// <param name="name">Name of the <see cref="Type"/> to resolve</param>
        /// <param name="expected">Expected default value</param>
        [DataTestMethod]
        [DynamicData(nameof(WithDefault_Data))]
        public virtual void Unregistered_Implicit_WithDefault(string name, object _, object expected)
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
        /// This test resolves POCO type with default values from initialized container. 
        /// All dependecies are successfully registered and are available.
        /// </summary>
        /// <example>
        /// 
        /// // POCO type
        /// 
        /// public class PocoTypeWithDefault 
        /// {
        ///     public int Field = 555;
        /// 
        ///     public int Property { get; set; } = 444
        /// 
        ///     [InjectionConstructor]
        ///     public WithDefault_Value(int value = 111) { }
        ///     
        ///     [InjectionMethod]
        ///     public void Method(int value = 222) { }
        /// }
        /// 
        /// /////////////////////////////////////
        /// 
        /// var container = new UnityContainer()
        ///     .RegisterInstance(111);
        ///     
        /// var result = container.Resolve(typeof(PocoTypeWithDefault));
        ///      
        /// </example>
        /// <param name="name">Name of the <see cref="Type"/> to resolve</param>
        /// <param name="expected">Value registered with the container</param>
        [DataTestMethod]
        [DynamicData(nameof(WithDefault_Data))]
        public virtual void Registered_Implicit_WithDefault(string name, object expected, object _)
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

        // Test Data
        public static IEnumerable<object[]> WithDefault_Data
        {
            get
            {
                yield return new object[] { "WithDefault_Value", RegisteredInt,    DefaultInt };
                yield return new object[] { "WithDefault_Class", RegisteredString, DefaultString };
            }
        }

        #endregion
    }
}
