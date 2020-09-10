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
        /// Testing resolving implicitly injected dependencies from empty container.
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
        ///                                      new InjectionMethod("Method", typeof(int)));
        ///      
        /// var result = container.Resolve(typeof(PocoType));
        /// </example>
        /// <param name="target">Name of the <see cref="Type"/> to resolve</param>
        /// <param name="dependency"><see cref="Type"/> of dependency</param>
        [DataTestMethod]
        [DataRow("NoDefault_Value",                 typeof(int))]
        [DataRow("NoDefault_Class",                 typeof(Unresolvable))]
        [DataRow("Required_Dependency_Value",       typeof(int))]
        [DataRow("Required_Dependency_Class",       typeof(Unresolvable))]
        [DataRow("Required_Dependency_Value_Named", typeof(int))]
        [DataRow("Required_Dependency_Class_Named", typeof(Unresolvable))]
        [ExpectedException(typeof(ResolutionFailedException))]
        public virtual void Unregistered_Injected_ByType(string target, Type dependency)
        {
            var type = TargetType(target);

            // Arrange
            Container.RegisterType(type, GetInjectionMethodBase(dependency));

            // Act
            _ = Container.Resolve(type);
        }

#if !V4 && !NET461
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
        ///     public PocoTypeWithDefault(int value = 111) { }
        ///     
        ///     public void Method(int value = 222) { }
        /// }
        /// 
        /// /////////////////////////////////////
        /// 
        ///  var container = new UnityContainer()
        ///      .RegisterType(typeof(PocoTypeWithDefault), 
        ///                     new InjectionConstructor(typeof(int)),
        ///                     new InjectionMethod("Method", typeof(int)));
        ///      
        /// var result = container.Resolve(typeof(PocoType));
        ///      
        /// </example>
        /// <param name="name">Name of the <see cref="Type"/> to resolve</param>
        /// <param name="dependency"><see cref="Type"/> of dependency</param>
        /// <param name="expected">Expected default value</param>
        [DataTestMethod]
        [DynamicData(nameof(Injected_ByType_WithDefault_Data))]
        public virtual void Unregistered_Injected_ByType_WithDefault(string name, Type dependency, object expected)
        {
            var type = TargetType(name);

            // Arrange
            Container.RegisterType(type, GetInjectionMethodBase(dependency));

            // Act
            var instance = Container.Resolve(type) as PatternBase;

            // Validate
            Assert.IsNotNull(instance);
            Assert.AreEqual(expected, instance.Value);
        }


        // Test Data
        public static IEnumerable<object[]> Injected_ByType_WithDefault_Data
        {
            get
            {
                yield return new object[] { "WithDefault_Value",          typeof(int),    DefaultInt };
                yield return new object[] { "WithDefault_Class",          typeof(string), DefaultString };

                yield return new object[] { "Required_WithDefault_Value", typeof(int), DefaultInt };
                yield return new object[] { "Required_WithDefault_Class", typeof(string), DefaultString };
            }
        }

         
        /// <summary>
        /// Testing resolving implicitly injected optional dependencies from empty container.
        /// </summary>
        /// <example>
        /// 
        /// public class PocoType 
        /// {
        ///     [OptionalDependency] public int Field;
        /// 
        ///     [OptionalDependency] public int Property { get; set; }
        /// 
        ///     public PocoType([OptionalDependency] int value = xxx) { }
        ///     
        ///     public void Method([OptionalDependency] int value) { }
        /// }
        /// 
        /// /////////////////////////////////////
        /// 
        ///  var container = new UnityContainer()
        ///      .RegisterType(typeof(PocoType), new InjectionConstructor(typeof(int)),
        ///                                      new InjectionMethod("Method", typeof(int)));
        ///      
        /// var result = container.Resolve(typeof(PocoType));
        /// </example>
        /// <param name="name">Name of the <see cref="Type"/> to resolve</param>
        /// <param name="dependency"><see cref="Type"/> of dependency</param>
        [DataTestMethod]
        [DynamicData(nameof(Optional_Injected_ByType_Data))]
        public virtual void Unregistered_Optional_Injected_ByType(string name, Type dependency, object expected)
        {
            var type = TargetType(name);

            // Arrange
            Container.RegisterType(type, GetInjectionMethodBase(dependency));

            // Act
            var instance = Container.Resolve(type) as PatternBase;

            // Validate
            Assert.IsNotNull(instance);
            Assert.AreEqual(expected, instance.Value);
        }

        // Test data
        public static IEnumerable<object[]> Optional_Injected_ByType_Data
        {
            get
            {
                yield return new object[] { "Optional_Dependency_Value",       typeof(int),          0 };
                yield return new object[] { "Optional_Dependency_Class",       typeof(Unresolvable), null };
                yield return new object[] { "Optional_Dependency_Value_Named", typeof(int),          0 };
                yield return new object[] { "Optional_Dependency_Class_Named", typeof(Unresolvable), null };
                yield return new object[] { "Optional_WithDefault_Value",      typeof(int),          DefaultInt };
                yield return new object[] { "Optional_WithDefault_Class",      typeof(string),       DefaultString };

            }
        }
#endif

        /// <summary>
        /// Testing resolving implicit dependencies from fully initialized container.
        /// </summary>
        /// <example>
        /// 
        /// public class PocoType 
        /// {
        ///     [Dependency]
        ///     public int Field;
        /// 
        ///     [xxxDependency]
        ///     public int Property { get; set; }
        /// 
        ///     public PocoType([OptionalDependency] int value) { }
        ///     
        ///     public void Method(int value) { }
        /// }
        /// 
        /// /////////////////////////////////////
        /// 
        ///  var container = new UnityContainer()
        ///      .RegisterInstance(111);
        ///      .RegisterType(typeof(PocoType), new InjectionConstructor(typeof(int)),
        ///                                      new InjectionMethod("Method", typeof(int)));
        ///      
        /// var result = container.Resolve(typeof(PocoType));
        /// </example>
        /// <param name="name">Name of the <see cref="Type"/> to resolve</param>
        /// <param name="dependency"><see cref="Type"/> of dependency</param>
        /// <param name="expected">Value registered with the container</param>
        [DataTestMethod]
        [DynamicData(nameof(Injected_MethodBase_ByType_Data))]
        public virtual void Registered_Injected_MethodBase_ByType(string name, Type dependency, object expected)
        {
            var type = TargetType(name);

            // Arrange
            RegisterTypes();
            Container.RegisterType(type, GetInjectionMethodBase(dependency));

            // Act
            var instance = Container.Resolve(type) as PatternBase;

            // Validate
            Assert.IsNotNull(instance);
            Assert.AreEqual(expected, instance.Value);
        }

        // Test data
        public static IEnumerable<object[]> Injected_MethodBase_ByType_Data
        {
            get
            {
                yield return new object[] { "NoDefault_Value",                 typeof(int),          RegisteredInt    };
                yield return new object[] { "NoDefault_Class",                 typeof(Unresolvable), Singleton        };
                yield return new object[] { "WithDefault_Value",               typeof(int),          RegisteredInt    };
                yield return new object[] { "WithDefault_Class",               typeof(string),       RegisteredString };

                yield return new object[] { "Required_Dependency_Value",       typeof(int),          RegisteredInt    };
                yield return new object[] { "Required_Dependency_Class",       typeof(Unresolvable), Singleton        };
#if !V4
                yield return new object[] { "Required_Dependency_Value_Named", typeof(int),          NamedInt         };
                yield return new object[] { "Required_Dependency_Class_Named", typeof(Unresolvable), NamedSingleton   };
#endif
                yield return new object[] { "Optional_Dependency_Value",       typeof(int),          RegisteredInt    };
                yield return new object[] { "Optional_Dependency_Class",       typeof(Unresolvable), Singleton        };
#if !V4
                yield return new object[] { "Optional_Dependency_Value_Named", typeof(int),          NamedInt         };
                yield return new object[] { "Optional_Dependency_Class_Named", typeof(Unresolvable), NamedSingleton   };
#endif
                yield return new object[] { "Optional_WithDefault_Value",      typeof(int),          RegisteredInt    };
                yield return new object[] { "Optional_WithDefault_Class",      typeof(string),       RegisteredString };
            }
        }
    }
}
