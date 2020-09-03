#if NET45
using Microsoft.Practices.Unity;
#else
using Unity;
#endif

namespace Spec.Constructors
{
    public partial class Constructors
    {
        public const string DefaultString = "default";
        public const int DefaultInt = 111;

        #region Test Data

        public class NoParametersCtor : BaseType
        {
            public NoParametersCtor() { }
        }

        public class NoAttributeParameterCtor : BaseType
        {
            public NoAttributeParameterCtor(string value)
            {
                Value = value;
            }
        }

        public class NoAttributeWithDefaultCtor : BaseType
        {
            public NoAttributeWithDefaultCtor(string value = DefaultString)
            {
                Value = value;
            }
        }

        public class NoAttributeWithDefaultValueCtor : BaseType
        {
            public NoAttributeWithDefaultValueCtor(int value = DefaultInt)
            {
                Value = value;
            }
        }

        public class NoAttributeWithDefaultNullCtor : BaseType
        {
            public NoAttributeWithDefaultNullCtor(string value = null)
            {
                Value = value;
            }
        }

        public class DependencyParameterCtor : BaseType
        {
            public DependencyParameterCtor([Dependency]string value)
            {
                Value = value;
            }
        }

        public class DependencyNamedParameterCtor : BaseType
        {
            public DependencyNamedParameterCtor([Dependency(Name)]string value)
            {
                Value = value;
            }
        }

        public class DependencyWithDefaultCtor : BaseType
        {
            public DependencyWithDefaultCtor([Dependency]string value = DefaultString)
            {
                Value = value;
            }
        }

        public class DependencyNamedWithDefaultCtor : BaseType
        {
            public DependencyNamedWithDefaultCtor([Dependency(Name)]string value = DefaultString)
            {
                Value = value;
            }
        }

        public class DependencyWithDefaultValueCtor : BaseType
        {
            public DependencyWithDefaultValueCtor([Dependency]int value = DefaultInt)
            {
                Value = value;
            }
        }

        public class DependencyWithDefaultNullCtor : BaseType
        {
            public DependencyWithDefaultNullCtor([Dependency]string value = null)
            {
                Value = value;
            }
        }

        public class OptionalParameterCtor : BaseType
        {
            public OptionalParameterCtor([OptionalDependency]string value)
            {
                Value = value;
            }
        }

        public class OptionalWithDefaultValueCtor : BaseType
        {
            public OptionalWithDefaultValueCtor([OptionalDependency]string value = DefaultString)
            {
                Value = value;
            }
        }

        public class OptionalNamedParameterCtor : BaseType
        {
            public OptionalNamedParameterCtor([OptionalDependency(Name)]string value)
            {
                Value = value;
            }
        }

        public class OptionalNamedWithDefaultCtor : BaseType
        {
            public OptionalNamedWithDefaultCtor([OptionalDependency(Name)]string value = DefaultString)
            {
                Value = value;
            }
        }

        public class OptionalWithDefaultNullCtor : BaseType
        {
            public OptionalWithDefaultNullCtor([OptionalDependency]string value = null)
            {
                Value = value;
            }
        }

        public class BaseType
        {
            public object Value { get; protected set; } = "none";
        }

        public struct TestStruct
        {
        }

        public class TypeWithStructParameter
        {
            public TypeWithStructParameter(TestStruct data)
            {
                Data = data;
            }

            public TestStruct Data { get; set; }
        }

        public class TypeWithDynamicParameter
        {
            public TypeWithDynamicParameter(dynamic data)
            {
                Data = data;
            }

            public dynamic Data { get; set; }
        }

        public class NamedTypeWithDynamicParameter
        {
            public NamedTypeWithDynamicParameter([Dependency(Name)]dynamic data)
            {
                Data = data;
            }

            public dynamic Data { get; set; }
        }

        public class TypeWithRefParameter
        {
            public TypeWithRefParameter(ref string ignored)
            {
            }

            public int Property { get; set; }
        }

        public class TypeWithOutParameter
        {
            public TypeWithOutParameter(out string ignored)
            {
                ignored = null;
            }

            public int Property { get; set; }
        }

        public class TypeWithUnresolvableParameter
        {
            public TypeWithUnresolvableParameter(Unresolvable data)
            {
                Data = data;
            }
            public dynamic Data { get; set; }
        }

        public class Unresolvable
        {
            private Unresolvable() { }

            public static Unresolvable Create() => new Unresolvable();
        }

        #endregion
    }
}
