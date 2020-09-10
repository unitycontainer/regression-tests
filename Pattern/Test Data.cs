using System;
using System.Collections.Generic;

namespace Specification
{

    public abstract partial class VerificationPattern
    {
        public struct TestStruct
        {
            public int Integer;
            public object Instance;

            public TestStruct(int value, object instance)
            {
                Integer = value;
                Instance = instance;
            }
        }

        public ref struct TestRefStruct
        {
            public int Integer;
            public object Object;
        }

        public abstract class PatternBase
        {
            public virtual object Value { get; protected set; }

        }


        #region Baseline

        public static IEnumerable<object[]> Injected_ByType_Data
        {
            get
            {
                yield return new object[] { "NoDefault_Value",                 typeof(int),          RegisteredInt   };
                yield return new object[] { "NoDefault_Class",                 typeof(Unresolvable), Singleton       };
                yield return new object[] { "WithDefault_Value",               typeof(int),          RegisteredInt   };
                yield return new object[] { "WithDefault_Class",               typeof(string),       RegisteredString };
            }
        }


        public static IEnumerable<object[]> Required_Injected_ByType_Data
        {
            get
            {
                yield return new object[] { "Required_Dependency_Value",       typeof(int),          RegisteredInt    };
                yield return new object[] { "Required_Dependency_Class",       typeof(Unresolvable), Singleton        };
                yield return new object[] { "Required_Dependency_Value_Named", typeof(int),          NamedInt         };
                yield return new object[] { "Required_Dependency_Class_Named", typeof(Unresolvable), NamedSingleton   };
                yield return new object[] { "Required_WithDefault_Value",      typeof(int),          RegisteredInt    };
                yield return new object[] { "Required_WithDefault_Class",      typeof(string),       RegisteredString };
            }
        }

        public static IEnumerable<object[]> Implicitly_Resolved_Registered_Optional_Data
        {
            get
            {
                yield return new object[] { "Optional_Dependency_Value",       typeof(int),          RegisteredInt };
                yield return new object[] { "Optional_Dependency_Class",       typeof(Unresolvable), Singleton        };
                yield return new object[] { "Optional_Dependency_Value_Named", typeof(int),          NamedInt         };
                yield return new object[] { "Optional_Dependency_Class_Named", typeof(Unresolvable), NamedSingleton   };
                yield return new object[] { "Optional_WithDefault_Value",      typeof(int),          RegisteredInt    };
                yield return new object[] { "Optional_WithDefault_Class",      typeof(string),       RegisteredString };
            }
        }

        public static IEnumerable<object[]> Injected_Data
        {
            get
            {
                yield return new object[] { "NoDefault_Value", InjectedInt,       InjectedInt };
                yield return new object[] { "NoDefault_Class", InjectedSingleton, InjectedSingleton };
                // TODO: non value
            }
        }

        public static IEnumerable<object[]> Resolved_WithDefault_Data
        {
            get
            {
                yield return new object[] { "WithDefault_Value", typeof(int),    DefaultInt };
                yield return new object[] { "WithDefault_Class", typeof(string), DefaultString };
                // TODO: non value
            }
        }

        public static IEnumerable<object[]> Injected_WithDefault_Data
        {
            get
            {
                yield return new object[] { "WithDefault_Value", InjectedInt,    InjectedInt };
                yield return new object[] { "WithDefault_Class", InjectedString, InjectedString };
                // TODO: non value
            }
        }

        #endregion


        #region Unresolvable

        public class Unresolvable
        {
            public readonly string ID;

            protected Unresolvable(string id) { ID = id; }

            public static Unresolvable Create(string name) => new Unresolvable(name);

            public override string ToString()
            {
                return $"Unresolvable.{ID}";
            }
        }

        public class SubUnresolvable : Unresolvable
        {
            private SubUnresolvable(string id)
                : base(id)
            {
            }
            public override string ToString()
            {
                return $"SubUnresolvable.{ID}";
            }
            public new static SubUnresolvable Create(string name) => new SubUnresolvable(name);
        }

        #endregion
    }
}


