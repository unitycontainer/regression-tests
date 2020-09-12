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
        // Test Data
        public static IEnumerable<object[]> Injection_Data
        {
            get
            {
                /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                //                          Test Name                   Type                        Name    Dependency              Expected
                                                                        
                yield return new object[] { "Value",                    PocoType,                   null,   typeof(int),            RegisteredInt       };
                yield return new object[] { "Class",                    PocoType,                   null,   typeof(Unresolvable),   Singleton           };
                yield return new object[] { "Struct",                   PocoType,                   null,   typeof(TestStruct),     RegisteredStruct    };

                yield return new object[] { "Value_Type_Name",          PocoType,                   Name,   typeof(int),            RegisteredInt       };
                yield return new object[] { "Class_Type_Name",          PocoType,                   Name,   typeof(Unresolvable),   Singleton           };

                yield return new object[] { "Default_Value",            PocoType_Default_Value,     null,   typeof(int),            RegisteredInt       };
                yield return new object[] { "Default_Class",            PocoType_Default_Class,     null,   typeof(string),         RegisteredString    };

                // Required

                yield return new object[] { "Required_Value",           Required,                   null,   typeof(int),            RegisteredInt       };
                yield return new object[] { "Required_Class",           Required,                   null,   typeof(Unresolvable),   Singleton           };
                yield return new object[] { "Required_Struct",          Required,                   null,   typeof(TestStruct),     RegisteredStruct    };

                yield return new object[] { "Required_Value_Named",     Required_Named,             null,   typeof(int),            NamedInt            };
                yield return new object[] { "Required_Class_Named",     Required_Named,             null,   typeof(Unresolvable),   NamedSingleton      };

                yield return new object[] { "Required_Default_Value",   Required_Default_Value,     null,   typeof(int),            RegisteredInt       };
                yield return new object[] { "Required_Default_Class",   Required_Default_String,    null,   typeof(string),         RegisteredString    };

                //// Optional

                yield return new object[] { "Optional_Value",           Optional,                   null,   typeof(int),            RegisteredInt       };
                yield return new object[] { "Optional_Class",           Optional,                   null,   typeof(Unresolvable),   Singleton           };
                yield return new object[] { "Optional_Struct",          Optional,                   null,   typeof(TestStruct),     RegisteredStruct    };

                yield return new object[] { "Optional_Value_Named",     Optional_Named,             null,   typeof(int),            NamedInt            };
                yield return new object[] { "Optional_Class_Named",     Optional_Named,             null,   typeof(Unresolvable),   NamedSingleton      };

                yield return new object[] { "Optional_Default_Value",   Optional_Default_Value,     null,   typeof(int),            RegisteredInt       };
                yield return new object[] { "Optional_Default_Class",   Optional_Default_Class,     null,   typeof(string),         RegisteredString    };
            }
        }
    }
}
