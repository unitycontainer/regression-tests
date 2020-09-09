using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
#if NET45
using Microsoft.Practices.Unity;
#else
using Unity;
using Unity.Injection;
#endif

namespace Specification.Pattern
{
    [TestClass]
    public partial class Properties : VerificationPattern
    {
        protected override InjectionMember GetInjectionMember(object argument) => new InjectionProperty("Property", argument);


        #region Test Data


        #region No Default

        public class NoDefault_Value : PatternBase
        {
            public int Property { get; set; }

            public override object Value { get => Property; protected set => throw new NotSupportedException(); }
        }

        public class NoDefault_Class : PatternBase
        {
            public Unresolvable Property { get; set; }

            public override object Value { get => Property; protected set => throw new NotSupportedException(); }
        }

        public class NoDefault_Generic<T> : PatternBase
        {
            public T Property { get; set; }

            public override object Value { get => Property; protected set => throw new NotSupportedException(); }
        }

        public class WithDefault_Value : PatternBase
        {
            public int Property { get; set; } = DefaultInt;

            public override object Value { get => Property; protected set => throw new NotSupportedException(); }
        }

        public class WithDefault_Class : PatternBase
        {
            public string Property { get; set; } = DefaultString;

            public override object Value { get => Property; protected set => throw new NotSupportedException(); }
        }

        #endregion


        #region Required

        public class Required_Dependency_Value : PatternBase
        {
            [Dependency] public int Property { get; set; }

            public override object Value { get => Property; protected set => throw new NotSupportedException(); }
        }

        public class Required_Dependency_Class : PatternBase
        {
            [Dependency] public Unresolvable Property { get; set; }

            public override object Value { get => Property; protected set => throw new NotSupportedException(); }
        }

        public class Required_Dependency_Generic<T> : PatternBase
        {
            [Dependency] public T Property { get; set; }

            public override object Value { get => Property; protected set => throw new NotSupportedException(); }
        }

        public class Required_WithDefault_Value : PatternBase
        {
            [Dependency] public int Property { get; set; } = DefaultInt;

            public override object Value { get => Property; protected set => throw new NotSupportedException(); }
        }

        public class Required_WithDefault_Class : PatternBase
        {
            [Dependency] public string Property { get; set; } = DefaultString;

            public override object Value { get => Property; protected set => throw new NotSupportedException(); }
        }

        public class Required_Dependency_Value_Named : PatternBase
        {
            [Dependency(Name)] public int Property { get; set; }

            public override object Value { get => Property; protected set => throw new NotSupportedException(); }
        }

        public class Required_Dependency_Class_Named : PatternBase
        {
            [Dependency(Name)] public Unresolvable Property { get; set; }

            public override object Value { get => Property; protected set => throw new NotSupportedException(); }
        }

        #endregion


        #region Optional

        public class Optional_Dependency_Value : PatternBase
        {
            [OptionalDependency] public int Property { get; set; }

            public override object Value { get => Property; protected set => throw new NotSupportedException(); }
        }

        public class Optional_Dependency_Class : PatternBase
        {
            [OptionalDependency] public Unresolvable Property { get; set; }

            public override object Value { get => Property; protected set => throw new NotSupportedException(); }
        }

        public class Optional_Dependency_Generic<T> : PatternBase
        {
            [OptionalDependency] public T Property { get; set; }

            public override object Value { get => Property; protected set => throw new NotSupportedException(); }
        }

        public class Optional_WithDefault_Value : PatternBase
        {
            [OptionalDependency] public int Property { get; set; } = DefaultInt;

            public override object Value { get => Property; protected set => throw new NotSupportedException(); }
        }

        public class Optional_WithDefault_Class : PatternBase
        {
            [OptionalDependency] public string Property { get; set; } = DefaultString;

            public override object Value { get => Property; protected set => throw new NotSupportedException(); }
        }

        public class Optional_Dependency_Value_Named : PatternBase
        {
            [OptionalDependency(Name)] public int Property { get; set; }

            public override object Value { get => Property; protected set => throw new NotSupportedException(); }
        }

        public class Optional_Dependency_Class_Named : PatternBase
        {
            [OptionalDependency(Name)] public Unresolvable Property { get; set; }

            public override object Value { get => Property; protected set => throw new NotSupportedException(); }
        }

        #endregion


        #endregion
    }
}
