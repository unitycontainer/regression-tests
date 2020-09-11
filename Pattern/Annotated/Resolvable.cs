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
        [Ignore]
        [DataTestMethod]
        [DynamicData(nameof(Resolvable_Data))]
        public virtual void Resolvable(string test, Type type, string name, Type dependency, object expected)
        {
            // Arrange
            RegisterTypes();

            // Act
            var instance = Container.Resolve(type, name) as PatternBase;

            // Validate
            Assert.IsNotNull(instance);
            Assert.AreEqual(expected, instance.Value);
        }

        // Test Data
        public static IEnumerable<object[]> Resolvable_Data
        {
            get
            {
                var Poco_Value      = PocoType.MakeGenericType(typeof(int));
                var Poco_Ref        = PocoType.MakeGenericType(typeof(Unresolvable));
                var Poco_Struct     = PocoType.MakeGenericType(typeof(TestStruct));

                var Required_Value  = Required.MakeGenericType(typeof(int));
                var Required_Ref    = Required.MakeGenericType(typeof(Unresolvable));
                var Required_Struct = Required.MakeGenericType(typeof(TestStruct));

                var Optional_Value  = Optional.MakeGenericType(typeof(int));
                var Optional_Ref    = Optional.MakeGenericType(typeof(Unresolvable));
                var Optional_Struct = Optional.MakeGenericType(typeof(TestStruct));

                /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                //                          Test Name                   Type                    Name    Dependency              Expected

                // Simple poco type

                yield return new object[] { "Generic_Value",            Poco_Value,             null,   typeof(int),            RegisteredInt       };
                yield return new object[] { "Generic_Class",            Poco_Ref,               null,   typeof(Unresolvable),   Singleton           };
                yield return new object[] { "Generic_Struct",           Poco_Struct,            null,   typeof(TestStruct),     RegisteredStruct    };
                
                yield return new object[] { "Value_Named",              Poco_Value,             Name,   typeof(int),            RegisteredInt       };
                yield return new object[] { "Class_Named",              Poco_Ref,               Name,   typeof(Unresolvable),   Singleton           };
                yield return new object[] { "Class_Null",               Poco_Ref,               Null,   typeof(Unresolvable),   Singleton           };

                yield return new object[] { "Default_Value",            PocoType_Default_Value, null,   typeof(int),            RegisteredInt       };
                yield return new object[] { "Default_Class",            PocoType_Default_Class, null,   typeof(Unresolvable),   RegisteredString    };


                // Required

                yield return new object[] { "Required_Value",           Required_Value,         null,   typeof(int),            RegisteredInt       };
                yield return new object[] { "Required_Class",           Required_Ref,           null,   typeof(Unresolvable),   Singleton           };
                yield return new object[] { "Required_Struct",          Required_Struct,        null,   typeof(TestStruct),     RegisteredStruct    };

                yield return new object[] { "Required_Value_Named",     Required_Value,         Name,   typeof(int),            RegisteredInt       };
                yield return new object[] { "Required_Class_Named",     Required_Ref,           Name,   typeof(Unresolvable),   Singleton           };
                yield return new object[] { "Required_Class_Null",      Required_Ref,           Null,   typeof(Unresolvable),   Singleton           };

                yield return new object[] { "Required_Default_Value",   Required_Default_Value, null,   typeof(int),            RegisteredInt       };
                yield return new object[] { "Required_Default_Class",   Required_Default_Class, null,   typeof(Unresolvable),   RegisteredString    };

                // Optional

                yield return new object[] { "Optional_Value",           Optional_Value,         null,   typeof(int),            RegisteredInt       };
                yield return new object[] { "Optional_Class",           Optional_Ref,           null,   typeof(Unresolvable),   Singleton           };
                yield return new object[] { "Optional_Struct",          Optional_Struct,        null,   typeof(TestStruct),     RegisteredStruct    };

                yield return new object[] { "Optional_Value_Named",     Optional_Value,         Name,   typeof(int),            RegisteredInt       };
                yield return new object[] { "Optional_Class_Named",     Optional_Ref,           Name,   typeof(Unresolvable),   Singleton           };
                yield return new object[] { "Optional_Class_Null",      Optional_Ref,           Null,   typeof(Unresolvable),   Singleton           };

                yield return new object[] { "Optional_Default_Value",   Optional_Default_Value, null,   typeof(int),            RegisteredInt       };
                yield return new object[] { "Optional_Default_Class",   Optional_Default_Class, null,   typeof(Unresolvable),   RegisteredString    };
            }
        }
    }
}
