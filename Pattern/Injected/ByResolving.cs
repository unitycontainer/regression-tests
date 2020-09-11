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
        /// Test use of <see cref="InjectionMember"/> to configure and resolve 
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
        ///                    new InjectionConstructor(new InjectionParameter(typeof(int))),
        ///                    new InjectionField("Field", typeof(int)),
        ///                    new InjectionProperty("Property", typeof(int)),
        ///                    new InjectionMethod("Method", new InjectionParameter(typeof(int))));
        ///      
        /// var result = container.Resolve(typeof(PocoType));
        /// </example>
        /// <param name="name">Name of the <see cref="Type"/> to resolve</param>
        /// <param name="dependency"><see cref="Type"/> of dependency</param>
        [DataTestMethod]
        [DataRow("Implicit_Dependency_Value",                 typeof(int))]
        [DataRow("Implicit_Dependency_Class",                 typeof(Unresolvable))]
        [DataRow("Required_Dependency_Value",       typeof(int))]
        [DataRow("Required_Dependency_Class",       typeof(Unresolvable))]
        [DataRow("Required_Dependency_Value_Named", typeof(int))]
        [DataRow("Required_Dependency_Class_Named", typeof(Unresolvable))]
        [DataRow("Optional_Dependency_Value",       typeof(int))]
        [DataRow("Optional_Dependency_Class",       typeof(Unresolvable))]
        [DataRow("Optional_Dependency_Value_Named", typeof(int))]
        [DataRow("Optional_Dependency_Class_Named", typeof(Unresolvable))]
        [ExpectedException(typeof(ResolutionFailedException))]
        public virtual void Unregistered_Injected_ByResolving(string name, Type dependency)
        {
            var type = TargetType(name);

            // Arrange
            Container.RegisterType(type, GetResolvedMember(dependency));

            // Act
            var result = Container.Resolve(type);
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
        [DynamicData(nameof(Injected_ByResolving_WithDefault_Data))]
        public virtual void Unregistered_Injected_ByResolving_WithDefault(string name, Type dependency, object expected)
        {
            var type = TargetType(name);

            // Arrange
            Container.RegisterType(type, GetResolvedMember(dependency));

            // Act
            var instance = Container.Resolve(type) as PatternBase;

            // Validate
            Assert.IsNotNull(instance);
            Assert.AreEqual(expected, instance.Value);
        }

        // Test Data
        public static IEnumerable<object[]> Injected_ByResolving_WithDefault_Data
        {
            get
            {
                yield return new object[] { "Implicit_WithDefault_Value",          typeof(int),    DefaultInt };
                yield return new object[] { "Implicit_WithDefault_Class",          typeof(string), DefaultString };

                yield return new object[] { "Required_WithDefault_Value", typeof(int),    DefaultInt };
                yield return new object[] { "Required_WithDefault_Class", typeof(string), DefaultString };
            }
        }
#endif


        /// <summary>
        /// Tests injecting dependencies by required resolution from empty container.
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
        ///      .RegisterInstance(111);
        ///      .RegisterType(typeof(PocoType), new InjectionConstructor(new ResolvedParameter(typeof(int), name)),
        ///                                   new InjectionField("Field", new ResolvedParameter(typeof(int), name)),
        ///                             new InjectionProperty("Property", new ResolvedParameter(typeof(int), name)),
        ///                                 new InjectionMethod("Method", new ResolvedParameter(typeof(int), name)));
        ///      
        /// var result = container.Resolve(typeof(PocoType));
        /// </example>
        /// <param name="target">Name of the <see cref="Type"/> to resolve</param>
        /// <param name="type"><see cref="Type"/> of dependency</param>
        /// <param name="name">Name of the contract</param>
        /// <param name="expected">Expected value</param>
        [DataTestMethod]
        [DynamicData(nameof(Injected_ByResolving_Data))]
        public virtual void Registered_Injected_ByResolving(string target, Type dependency, string name, object expected)
        {
            var type = TargetType(target);

            // Arrange
            RegisterTypes();
            Container.RegisterType(type, GetResolvedMember(dependency, name));

            // Act
            var instance = Container.Resolve(type) as PatternBase;

            // Validate
            Assert.IsNotNull(instance);
            Assert.AreEqual(expected, instance.Value);
        }


        // Test data
        public static IEnumerable<object[]> Injected_ByResolving_Data
        {
            get
            {
                yield return new object[] { "Implicit_Dependency_Value",                 typeof(int),          null,   RegisteredInt };
                yield return new object[] { "Implicit_Dependency_Class",                 typeof(Unresolvable), null,   Singleton };
                yield return new object[] { "Implicit_WithDefault_Value",               typeof(int),          null,   RegisteredInt };
                yield return new object[] { "Implicit_WithDefault_Class",               typeof(string),       null,   RegisteredString };
                                                                               
                yield return new object[] { "Required_Dependency_Value",       typeof(int),          null,   RegisteredInt };
                yield return new object[] { "Required_Dependency_Class",       typeof(Unresolvable), null,   Singleton };
                yield return new object[] { "Required_Dependency_Value_Named", typeof(int),          Name,   NamedInt };
                yield return new object[] { "Required_Dependency_Class_Named", typeof(Unresolvable), Name,   NamedSingleton };

                yield return new object[] { "Optional_Dependency_Value",       typeof(int),          null,   RegisteredInt };
                yield return new object[] { "Optional_Dependency_Class",       typeof(Unresolvable), null,   Singleton };
                yield return new object[] { "Optional_Dependency_Value_Named", typeof(int),          Name,   NamedInt };
                yield return new object[] { "Optional_Dependency_Class_Named", typeof(Unresolvable), Name,   NamedSingleton };
                yield return new object[] { "Optional_WithDefault_Value",      typeof(int),          null,   RegisteredInt };
                yield return new object[] { "Optional_WithDefault_Class",      typeof(string),       null,   RegisteredString };
            }                                                                                        
        }                                                                                            
    }
}
