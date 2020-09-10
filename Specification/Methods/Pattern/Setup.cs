using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
#if V4
using Microsoft.Practices.Unity;
#else
using Unity;
using Unity.Injection;
#endif

namespace Specification.Pattern
{
    [TestClass]
    public partial class Methods : VerificationPattern
    {
        protected override InjectionMember GetInjectionMethodBase(object argument) 
            => new InjectionMethod("Method", argument);

        protected override InjectionMember GetResolvedMember(Type argument) 
            => new InjectionMethod("Method", new ResolvedParameter(argument));

        protected override InjectionMember GetResolvedMember(Type argument, string name) 
            => new InjectionMethod("Method", new ResolvedParameter(argument, name));

        protected override InjectionMember GetInjectionMember(object argument) 
            => new InjectionMethod("Method", new InjectionParameter(argument));


        #region Test Data


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


        #endregion
    }
}
