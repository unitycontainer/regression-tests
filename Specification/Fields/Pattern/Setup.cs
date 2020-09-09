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
    public partial class Fields : VerificationPattern
    {
        protected override InjectionMember GetInjectedMember(object argument) => new InjectionField("Field", argument);


        #region Test Data


        #region Implicit

        public class NoDefault_Value : PatternBase
        {
            public int Field;

            public override object Value { get => Field; protected set => throw new NotSupportedException(); }
        }

        public class NoDefault_Class : PatternBase
        {
            public Unresolvable Field;

            public override object Value { get => Field; protected set => throw new NotSupportedException(); }
        }

        public class NoDefault_Generic<T> : PatternBase
        {
            public T Field;

            public override object Value { get => Field; protected set => throw new NotSupportedException(); }
        }

        public class WithDefault_Value : PatternBase
        {
            public int Field = DefaultInt;

            public override object Value { get => Field; protected set => throw new NotSupportedException(); }
        }

        public class WithDefault_Class : PatternBase
        {
            public string Field = DefaultString;

            public override object Value { get => Field; protected set => throw new NotSupportedException(); }
        }

        #endregion


        #region Required

        public class Required_Dependency_Value : PatternBase
        {
            [Dependency] public int Field;

            public override object Value { get => Field; protected set => throw new NotSupportedException(); }
        }

        public class Required_Dependency_Class : PatternBase
        {
            [Dependency] public Unresolvable Field;

            public override object Value { get => Field; protected set => throw new NotSupportedException(); }
        }

        public class Required_Dependency_Generic<T> : PatternBase
        {
            [Dependency] public T Field;

            public override object Value { get => Field; protected set => throw new NotSupportedException(); }
        }

        public class Required_WithDefault_Value : PatternBase
        {
            [Dependency] public int Field = DefaultInt;

            public override object Value { get => Field; protected set => throw new NotSupportedException(); }
        }

        public class Required_WithDefault_Class : PatternBase
        {
            [Dependency] public string Field = DefaultString;

            public override object Value { get => Field; protected set => throw new NotSupportedException(); }
        }

        public class Required_Dependency_Value_Named : PatternBase
        {
            [Dependency(Name)] public int Field;

            public override object Value { get => Field; protected set => throw new NotSupportedException(); }
        }

        public class Required_Dependency_Class_Named : PatternBase
        {
            [Dependency(Name)] public Unresolvable Field;

            public override object Value { get => Field; protected set => throw new NotSupportedException(); }
        }


        #endregion


        #region Optional

        public class Optional_Dependency_Value : PatternBase
        {
            [OptionalDependency] public int Field;

            public override object Value { get => Field; protected set => throw new NotSupportedException(); }
        }

        public class Optional_Dependency_Class : PatternBase
        {
            [OptionalDependency] public Unresolvable Field;

            public override object Value { get => Field; protected set => throw new NotSupportedException(); }
        }

        public class Optional_Dependency_Generic<T> : PatternBase
        {
            [OptionalDependency] public T Field;

            public override object Value { get => Field; protected set => throw new NotSupportedException(); }
        }

        public class Optional_WithDefault_Value : PatternBase
        {
            [OptionalDependency] public int Field = DefaultInt;

            public override object Value { get => Field; protected set => throw new NotSupportedException(); }
        }

        public class Optional_WithDefault_Class : PatternBase
        {
            [OptionalDependency] public string Field = DefaultString;

            public override object Value { get => Field; protected set => throw new NotSupportedException(); }
        }

        public class Optional_Dependency_Value_Named : PatternBase
        {
            [OptionalDependency(Name)] public int Field;

            public override object Value { get => Field; protected set => throw new NotSupportedException(); }
        }

        public class Optional_Dependency_Class_Named : PatternBase
        {
            [OptionalDependency(Name)] public Unresolvable Field;

            public override object Value { get => Field; protected set => throw new NotSupportedException(); }
        }


        #endregion


        #endregion
    }
}
