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
        /// Tests injecting dependencies by member name
        /// </summary>
        /// <example>
        /// Container.RegisterType(target, new InjectionMethod("Method") , 
        ///                                new InjectionField("Field"), 
        ///                                new InjectionProperty("Property"));
        /// </example>
        /// <param name="test">Test name</param>
        /// <param name="type">Resolved type</param>
        /// <param name="name">Contract name</param>
        /// <param name="dependency">Dependency type</param>
        /// <param name="expected">Expected value</param>
        [DataTestMethod]
        [DynamicData(nameof(Injection_Data))]
        public virtual void Injected_ByName(string test, Type type, string name, Type dependency, object expected)
        {
            Type target = type.IsGenericTypeDefinition
                        ? type.MakeGenericType(dependency)
                        : type;
            // Arrange
            Container.RegisterType(target, GetMemberByName());
            
            RegisterTypes();

            // Act
            var instance = Container.Resolve(target, name) as PatternBase;

            // Validate
            Assert.IsNotNull(instance);
            Assert.AreEqual(expected, instance.Value);
        }




        /// <summary>
        /// Tests injecting required dependencies by type from empty container
        /// </summary>
        /// <param name="test">Test name</param>
        /// <param name="type">Resolved type</param>
        /// <param name="name">Contract name</param>
        /// <param name="dependency">Dependency type</param>
        /// <param name="expected">Expected value</param>
        [DataTestMethod]
        [DynamicData(nameof(Injected_ByName_Required_Data))]
        [ExpectedException(typeof(ResolutionFailedException))]
        public virtual void Injected_ByName_Required(string test, Type type, string name, Type dependency)
        {
            Type target = type.IsGenericTypeDefinition
                        ? type.MakeGenericType(dependency)
                        : type;
            // Arrange
            Container.RegisterType(target, GetMemberByName());

            // Act
            _ = Container.Resolve(target, name) as PatternBase;
        }

        // Test Data
        public static IEnumerable<object[]> Injected_ByName_Required_Data
        {
            get
            {
                //////////////////////////////////////////////////////////////////////////////////////////////////////
                //                          Test Name                   Type            Name    Dependency          
                                                                        
                yield return new object[] { "Value",                    PocoType,       null,   typeof(int)          };
                yield return new object[] { "Class",                    PocoType,       null,   typeof(Unresolvable) };
                yield return new object[] { "Struct",                   PocoType,       null,   typeof(TestStruct)   };

                yield return new object[] { "Value_Type_Name",          PocoType,       Name,   typeof(int)          };
                yield return new object[] { "Class_Type_Name",          PocoType,       Name,   typeof(Unresolvable) };

                yield return new object[] { "Required_Value",           Required,       null,   typeof(int)          };
                yield return new object[] { "Required_Class",           Required,       null,   typeof(Unresolvable) };
                yield return new object[] { "Required_Struct",          Required,       null,   typeof(TestStruct)   };

                yield return new object[] { "Required_Value_Named",     Required_Named, null,   typeof(int)          };
                yield return new object[] { "Required_Class_Named",     Required_Named, null,   typeof(Unresolvable) };
            }
        }


        /// <summary>
        /// Tests injecting optional dependencies by type from empty container
        /// </summary>
        /// <param name="test">Test name</param>
        /// <param name="type">Resolved type</param>
        /// <param name="name">Contract name</param>
        /// <param name="dependency">Dependency type</param>
        /// <param name="expected">Expected value</param>
        [DataTestMethod]
        [DynamicData(nameof(Injected_ByName_Optional_Data))]
        public virtual void Injected_ByName_Optional(string test, Type type, string name, Type dependency, object expected)
        {
            Type target = type.IsGenericTypeDefinition
                        ? type.MakeGenericType(dependency)
                        : type;
            // Arrange
            Container.RegisterType(target, GetMemberByName());

            // Act
            var instance = Container.Resolve(target, name) as PatternBase;

            // Validate
            Assert.IsNotNull(instance);
            Assert.AreEqual(expected, instance.Value);
        }

        // Test Data
        public static IEnumerable<object[]> Injected_ByName_Optional_Data
        {
            get
            {
                /////////////////////////////////////////////////////////////////////////////////////////////////////////
                //                          Test Name               Type            Name  Dependency            Expected
                                                                    
                yield return new object[] { "Optional_Value",       Optional,       null, typeof(int),          0       };
                yield return new object[] { "Optional_Class",       Optional,       null, typeof(Unresolvable), null    };

                yield return new object[] { "Optional_Value_Named", Optional_Named, null, typeof(int),          0       };
                yield return new object[] { "Optional_Class_Named", Optional_Named, null, typeof(Unresolvable), null    };
            }
        }


        /// <summary>
        /// Tests injecting optional dependencies by type from empty container
        /// </summary>
        /// <param name="test">Test name</param>
        /// <param name="type">Resolved type</param>
        /// <param name="name">Contract name</param>
        /// <param name="dependency">Dependency type</param>
        /// <param name="expected">Expected value</param>
        [DataTestMethod]
        [DynamicData(nameof(Injected_ByName_WithDefault_Data))]
        public virtual void Injected_ByName_WithDefault(string test, Type type, string name, Type dependency, object expected)
        {
            Type target = type.IsGenericTypeDefinition
                        ? type.MakeGenericType(dependency)
                        : type;
            // Arrange
            Container.RegisterType(target, GetMemberByName());

            // Act
            var instance = Container.Resolve(target, name) as PatternBase;

            // Validate
            Assert.IsNotNull(instance);
            Assert.AreEqual(expected, instance.Value);
        }

        // Test Data
        public static IEnumerable<object[]> Injected_ByName_WithDefault_Data
        {
            get
            {
                /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                //                          Test Name                 Type                     Name  Dependency     Expected

                yield return new object[] { "Default_Value",          PocoType_Default_Value,  null, typeof(int),    DefaultInt     };
                yield return new object[] { "Default_Class",          PocoType_Default_Class,  null, typeof(string), DefaultString  };

                yield return new object[] { "Required_Default_Value", Required_Default_Value,  null, typeof(int),    DefaultInt     };
                yield return new object[] { "Required_Default_Class", Required_Default_String, null, typeof(string), DefaultString  };

                yield return new object[] { "Optional_Default_Value", Optional_Default_Value,  null, typeof(int),    DefaultInt     };
                yield return new object[] { "Optional_Default_Class", Optional_Default_Class,  null, typeof(string), DefaultString  };
            }
        }
    }
}
