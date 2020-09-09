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

        #region Baseline Validation

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
                yield return new object[] { "WithDefault_Class", Registeredtring, DefaultString };
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
                yield return new object[] { "Required_WithDefault_Class", Registeredtring, DefaultString };
            }
        }

        #endregion


        #region Optional

        public static IEnumerable<object[]> Optional_Data
        {
            get
            {
                yield return new object[] { "Optional_Dependency_Value",       RegisteredInt , 0};
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
                yield return new object[] { "Optional_WithDefault_Class", Registeredtring, DefaultString };
            }
        }

        #endregion


        #region Unresolvable

        public class Unresolvable
        {
            public readonly string ID = Guid.NewGuid().ToString();

            private Unresolvable() { }

            public static Unresolvable Create() => new Unresolvable();
        }

        #endregion
    }
}


