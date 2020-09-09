using System;
using System.Collections.Generic;

namespace Specification
{

    public abstract partial class VerificationPattern
    {
        public abstract class PatternBase
        {
            public virtual object Value { get; protected set; }

        }

        #region Baseline

        public static IEnumerable<object[]> NoDefault_Data
        {
            get
            {
                yield return new object[] { "NoDefault_Value", RegisteredInt };
                yield return new object[] { "NoDefault_Class", Singleton };
            }
        }

        public static IEnumerable<object[]> WithDefault_Data
        {
            get
            {
                yield return new object[] { "WithDefault_Value", RegisteredInt, DefaultInt };
                yield return new object[] { "WithDefault_Class", RegisteredString, DefaultString };
            }
        }

        #endregion


        #region Required

        public static IEnumerable<object[]> Required_Data
        {
            get
            {
                yield return new object[] { "Required_Dependency_Value",       RegisteredInt };
                yield return new object[] { "Required_Dependency_Class",       Singleton };
                yield return new object[] { "Required_Dependency_Value_Named", NamedInt };
                yield return new object[] { "Required_Dependency_Class_Named", NamedSingleton };
            }
        }

        public static IEnumerable<object[]> Required_WithDefault_Data
        {
            get
            {
                yield return new object[] { "Required_WithDefault_Value", RegisteredInt,  DefaultInt };
                yield return new object[] { "Required_WithDefault_Class", RegisteredString, DefaultString };
            }
        }

        #endregion


        #region Optional

        public static IEnumerable<object[]> Optional_Data
        {
            get
            {
                yield return new object[] { "Optional_Dependency_Value",       RegisteredInt, 0};
                yield return new object[] { "Optional_Dependency_Class",       Singleton     , null};
                yield return new object[] { "Optional_Dependency_Value_Named", NamedInt      , 0};
                yield return new object[] { "Optional_Dependency_Class_Named", NamedSingleton, null};
            }
        }

        public static IEnumerable<object[]> Optional_WithDefault_Data
        {
            get
            {
                yield return new object[] { "Optional_WithDefault_Value", RegisteredInt,   DefaultInt };
                yield return new object[] { "Optional_WithDefault_Class", RegisteredString, DefaultString };
            }
        }

        #endregion



        #region Baseline

        public static IEnumerable<object[]> Implicitly_Resolved_Data
        {
            get
            {
                yield return new object[] { "NoDefault_Value",                 typeof(int),          RegisteredInt   };
                yield return new object[] { "NoDefault_Class",                 typeof(Unresolvable), Singleton       };
                yield return new object[] { "WithDefault_Value",               typeof(int),          RegisteredInt   };
                yield return new object[] { "WithDefault_Class",               typeof(string),       RegisteredString };
            }
        }


        public static IEnumerable<object[]> Implicitly_Resolved_Required_Data
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

        public static IEnumerable<object[]> Implicitly_Resolved_Optional_Data
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

            private Unresolvable(string id) { ID = id; }

            public static Unresolvable Create(string name) => new Unresolvable(name);

            public override string ToString()
            {
                return $"Unresolvable.{ID}";
            }
        }

        #endregion
    }
}


