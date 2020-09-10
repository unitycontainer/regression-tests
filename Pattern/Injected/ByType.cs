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
        /// <param name="target">Name of the <see cref="Type"/> to resolve</param>
        /// <param name="dependency"><see cref="Type"/> of dependency</param>
        /// <param name="expected">Value registered with the container</param>
        [DataTestMethod]
        [DynamicData(nameof(Injected_ByType_Data))]
        public virtual void Registered_Injected_ByType(string target, Type dependency, object expected)
        {
            var type = TargetType(target);

            // Arrange
            RegisterTypes();
            Container.RegisterType(type, GetInjectionMember(dependency));

            // Act
            var instance = Container.Resolve(type) as PatternBase;

            // Validate
            Assert.IsNotNull(instance);
            Assert.AreEqual(expected, instance.Value);
        }

        // Test data
        public static IEnumerable<object[]> Injected_ByType_Data
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


        public static IEnumerable<object[]> _Injected_ByType_Data
        {
            get
            {
                yield return new object[] { "NoDefault_Value", typeof(int), RegisteredInt };
                yield return new object[] { "NoDefault_Class", typeof(Unresolvable), Singleton };
                yield return new object[] { "WithDefault_Value", typeof(int), RegisteredInt };
                yield return new object[] { "WithDefault_Class", typeof(string), RegisteredString };
            }
        }

    }
}
