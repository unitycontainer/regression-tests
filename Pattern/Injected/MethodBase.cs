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
        [ExpectedException(typeof(ResolutionFailedException))]
        public virtual void Unregistered_Injected_ByType(string name, Type dependency)
        {
            // Arrange
            var type = TargetType(name);
            Container.RegisterType(type, GetInjectionMethodBase(dependency));

            // Act
            var result = Container.Resolve(type);
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
            // Arrange
            var type = TargetType(name);
            Container.RegisterType(type, GetInjectionMethodBase(dependency));

            // Act
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
#if V6 && DEBUG
                yield return new object[] { "Optional_WithDefault_Value",      typeof(int),          DefaultInt };
                yield return new object[] { "Optional_WithDefault_Class",      typeof(string),       DefaultString };
#endif
            }
        }

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
        [DynamicData(nameof(Optional_Injected_Implicitly_Data))]
        public virtual void Registered_Injected_ByType(string name, Type dependency, object expected)
        {
            // Arrange
            var type = TargetType(name);
            RegisterTypes();
            Container.RegisterType(type, GetInjectionMethodBase(dependency));

            // Act
            var instance = Container.Resolve(type) as PatternBase;

            // Validate
            Assert.IsNotNull(instance);
            Assert.AreEqual(expected, instance.Value);
        }

        // Test data
        public static IEnumerable<object[]> Optional_Injected_Implicitly_Data
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
