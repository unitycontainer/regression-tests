using System;
#if V4
using Microsoft.Practices.Unity;
#else
using Unity;
using Unity.Injection;
#endif

namespace Specification.Pattern
{
    public partial class Methods
    {
        #region No Default

        public class NoDefault_Value : PatternBase
        {
            [InjectionMethod]
            public void Method(int value) => Value = value;
        }

        public class NoDefault_Class : PatternBase
        {
            [InjectionMethod]
            public void Method(Unresolvable value) => Value = value;
        }

        public class NoDefault_Dynamic : PatternBase
        {
            [InjectionMethod]
            public void Method(dynamic value) => Value = value;
        }

        public class NoDefault_Struct : PatternBase
        {
            [InjectionMethod]
            public void Method(TestStruct value) => Value = value;
        }

        public class NoDefault_RefStruct : PatternBase
        {
            [InjectionMethod]
            public void Method(TestRefStruct value) { }
        }

        public class NoDefault_Ref : PatternBase
        {
            [InjectionMethod]
            public void Method(ref Unresolvable value) => Value = value;
        }

        public class NoDefault_Out : PatternBase
        {
            [InjectionMethod]
            public void Method(out Unresolvable value) => value = null;
        }

        public class NoDefault_Generic<T> : PatternBase
        {
            [InjectionMethod]
            public void Method(T value) => Value = value;
        }

        public class WithDefault_Value : PatternBase
        {
            [InjectionMethod]
            public void Method(int value = DefaultInt) => Value = value;
        }

        public class WithDefault_Class : PatternBase
        {
            [InjectionMethod]
            public void Method(string value = DefaultString) => Value = value;
        }

        #endregion


        #region Required

        public class Required_Dependency_Value : PatternBase
        {
            [InjectionMethod]
            public void Method([Dependency] int value) => Value = value;
        }

        public class Required_Dependency_Class : PatternBase
        {
            [InjectionMethod]
            public void Method([Dependency] Unresolvable value) => Value = value;
        }

        public class Required_Dependency_Dynamic : PatternBase
        {
            [InjectionMethod]
            public void Method([Dependency] dynamic value) => Value = value;
        }

        public class Required_Dependency_Struct : PatternBase
        {
            [InjectionMethod]
            public void Method([Dependency] TestStruct value) => Value = value;
        }

        public class Required_Dependency_RefStruct : PatternBase
        {
            [InjectionMethod]
            public void Method([Dependency] TestRefStruct value) { }
        }

        public class Required_Dependency_Ref : PatternBase
        {
            [InjectionMethod]
            public void Method([Dependency] ref Unresolvable value) => Value = value;
        }

        public class Required_Dependency_Out : PatternBase
        {
            [InjectionMethod]
            public void Method([Dependency] out Unresolvable value) => value = null;
        }

        public class Required_Dependency_Generic<T> : PatternBase
        {
            [InjectionMethod]
            public void Method([Dependency] T value) => Value = value;
        }

        public class Required_WithDefault_Value : PatternBase
        {
            [InjectionMethod]
            public void Method([Dependency] int value = DefaultInt) => Value = value;
        }

        public class Required_WithDefault_Class : PatternBase
        {
            [InjectionMethod]
            public void Method([Dependency] string value = DefaultString) => Value = value;
        }


        public class Required_Dependency_Value_Named : PatternBase
        {
            [InjectionMethod]
            public void Method([Dependency(Name)] int value) => Value = value;
        }

        public class Required_Dependency_Class_Named : PatternBase
        {
            [InjectionMethod]
            public void Method([Dependency(Name)] Unresolvable value) => Value = value;
        }

        #endregion


        #region Optional

        public class Optional_Dependency_Value : PatternBase
        {
            [InjectionMethod]
            public void Method([OptionalDependency] int value) => Value = value;
        }

        public class Optional_Dependency_Class : PatternBase
        {
            [InjectionMethod]
            public void Method([OptionalDependency] Unresolvable value) => Value = value;
        }

        public class Optional_Dependency_Dynamic : PatternBase
        {
            [InjectionMethod]
            public void Method([OptionalDependency] dynamic value) => Value = value;
        }

        public class Optional_Dependency_Struct : PatternBase
        {
            [InjectionMethod]
            public void Method([OptionalDependency] TestStruct value) => Value = value;
        }

        public class Optional_Dependency_RefStruct : PatternBase
        {
            [InjectionMethod]
            public void Method([OptionalDependency] TestRefStruct value) { }
        }

        public class Optional_Dependency_Ref : PatternBase
        {
            [InjectionMethod]
            public void Method([OptionalDependency] ref Unresolvable value) => Value = value;
        }

        public class Optional_Dependency_Out : PatternBase
        {
            [InjectionMethod]
            public void Method([OptionalDependency] out Unresolvable value) => value = null;
        }

        public class Optional_Dependency_Generic<T> : PatternBase
        {
            [InjectionMethod]
            public void Method([OptionalDependency] T value) => Value = value;
        }

        public class Optional_WithDefault_Value : PatternBase
        {
            [InjectionMethod]
            public void Method([OptionalDependency] int value = DefaultInt) => Value = value;
        }

        public class Optional_WithDefault_Class : PatternBase
        {
            [InjectionMethod]
            public void Method([OptionalDependency] string value = DefaultString) => Value = value;
        }


        public class Optional_Dependency_Value_Named : PatternBase
        {
            [InjectionMethod]
            public void Method([OptionalDependency(Name)] int value) => Value = value;
        }

        public class Optional_Dependency_Class_Named : PatternBase
        {
            [InjectionMethod]
            public void Method([OptionalDependency(Name)] Unresolvable value) => Value = value;
        }

        #endregion
    }
}
