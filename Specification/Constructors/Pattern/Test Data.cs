using System;
#if V4
using Microsoft.Practices.Unity;
#else
using Unity;
using Unity.Injection;
#endif

namespace Specification.Pattern
{
    public partial class Constructors
    {
        #region Implicit

        public class NoDefault_Value : PatternBase
        {
            public NoDefault_Value(int value) => Value = value;
        }

        public class NoDefault_Class : PatternBase
        {
            public NoDefault_Class(Unresolvable value) => Value = value;
        }

        public class NoDefault_Dynamic : PatternBase
        {
            public NoDefault_Dynamic(dynamic value) => Value = value;
        }

        public class NoDefault_Struct : PatternBase
        {
            public NoDefault_Struct(TestStruct value) => Value = value;
        }

        public class NoDefault_RefStruct : PatternBase
        {
            public NoDefault_RefStruct(TestRefStruct value) { }
        }

        public class NoDefault_Ref : PatternBase
        {
            public NoDefault_Ref(ref Unresolvable value) => Value = value;
        }

        public class NoDefault_Out : PatternBase
        {
            public NoDefault_Out(out Unresolvable value) => value = null;
        }

        public class NoDefault_Generic<T> : PatternBase
        {
            public NoDefault_Generic(T value) => Value = value;
        }

        public class WithDefault_Value : PatternBase
        {
            public WithDefault_Value(int value = DefaultInt) => Value = value;
        }

        public class WithDefault_Class : PatternBase
        {
            public WithDefault_Class(string value = DefaultString) => Value = value;
        }

        #endregion


        #region Required

        public class Required_Dependency_Value : PatternBase 
        {
            public Required_Dependency_Value([Dependency] int value) => Value = value;
        }

        public class Required_Dependency_Class : PatternBase 
        {
            public Required_Dependency_Class([Dependency] Unresolvable value) => Value = value;
        }

        public class Required_Dependency_Dynamic : PatternBase
        {
            public Required_Dependency_Dynamic([Dependency] dynamic value) => Value = value;
        }

        public class Required_Dependency_Struct : PatternBase
        {
            public Required_Dependency_Struct([Dependency] TestStruct value) => Value = value;
        }

        public class Required_Dependency_RefStruct : PatternBase
        {
            public Required_Dependency_RefStruct([Dependency] TestRefStruct value) { }
        }

        public class Required_Dependency_Ref : PatternBase
        {
            public Required_Dependency_Ref([Dependency] ref Unresolvable value) => Value = value;
        }

        public class Required_Dependency_Out : PatternBase
        {
            public Required_Dependency_Out([Dependency] out Unresolvable value) => value = null;
        }

        public class Required_Dependency_Generic<T> : PatternBase 
        {
            public Required_Dependency_Generic([Dependency] T value) => Value = value;
        }

        public class Required_WithDefault_Value : PatternBase
        {
            public Required_WithDefault_Value([Dependency] int value = DefaultInt) => Value = value;
        }

        public class Required_WithDefault_Class : PatternBase
        {
            public Required_WithDefault_Class([Dependency] string value = DefaultString) => Value = value;
        }

        public class Required_Dependency_Value_Named : PatternBase
        {
            public Required_Dependency_Value_Named([Dependency(Name)] int value) => Value = value;
        }

        public class Required_Dependency_Class_Named : PatternBase
        {
            public Required_Dependency_Class_Named([Dependency(Name)] Unresolvable value) => Value = value;
        }

        #endregion


        #region Optional

        public class Optional_Dependency_Value : PatternBase
        {
            public Optional_Dependency_Value([OptionalDependency] int value) => Value = value;
        }

        public class Optional_Dependency_Class : PatternBase
        {
            public Optional_Dependency_Class([OptionalDependency] Unresolvable value) => Value = value;
        }

        public class Optional_Dependency_Dynamic : PatternBase
        {
            public Optional_Dependency_Dynamic([OptionalDependency] dynamic value) => Value = value;
        }

        public class Optional_Dependency_Struct : PatternBase
        {
            public Optional_Dependency_Struct([OptionalDependency] TestStruct value) => Value = value;
        }

        public class Optional_Dependency_RefStruct : PatternBase
        {
            public Optional_Dependency_RefStruct([OptionalDependency] TestRefStruct value) { }
        }

        public class Optional_Dependency_Ref : PatternBase
        {
            public Optional_Dependency_Ref([OptionalDependency] ref Unresolvable value) => Value = value;
        }

        public class Optional_Dependency_Out : PatternBase
        {
            public Optional_Dependency_Out([OptionalDependency] out Unresolvable value) => value = null;
        }

        public class Optional_Dependency_Generic<T> : PatternBase
        {
            public Optional_Dependency_Generic([OptionalDependency] T value) => Value = value;
        }

        public class Optional_WithDefault_Value : PatternBase
        {
            public Optional_WithDefault_Value([OptionalDependency] int value = DefaultInt) => Value = value;
        }

        public class Optional_WithDefault_Class : PatternBase
        {
            public Optional_WithDefault_Class([OptionalDependency] string value = DefaultString) => Value = value;
        }

        public class Optional_Dependency_Value_Named : PatternBase
        {
            public Optional_Dependency_Value_Named([OptionalDependency(Name)] int value) => Value = value;
        }

        public class Optional_Dependency_Class_Named : PatternBase
        {
            public Optional_Dependency_Class_Named([OptionalDependency(Name)] Unresolvable value) => Value = value;
        }

        #endregion
    }
}
