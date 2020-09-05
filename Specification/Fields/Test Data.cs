using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
#if NET45
using Microsoft.Practices.Unity;
#else
using Unity;
using Unity.Resolution;
#endif

namespace Specification
{
    public partial class Fields
    {
        #region Test Data

        public class ValidatingResolverFactory : IResolverFactory<Type>
        {
            private object _value;

            public ValidatingResolverFactory(object value)
            {
                _value = value;
            }

            public Type Type { get; private set; }
            public string Name { get; private set; }

            public ResolveDelegate<TContext> GetResolver<TContext>(Type info)
                where TContext : IResolveContext
            {
                return (ref TContext context) =>
                {
                    Type = context.Type;
                    Name = context.Name;

                    return _value;
                };
            }
        }

        public class ValidatingResolver : IResolve
        {
            private object _value;

            public ValidatingResolver(object value)
            {
                _value = value;
            }

            public object Resolve<TContext>(ref TContext context) where TContext : IResolveContext
            {
                Type = context.Type;
                Name = context.Name;

                return _value;
            }

            public Type Type { get; private set; }

            public string Name { get; private set; }
        }

        public class ObjectWithThreeFields
        {
            [Dependency]
            public string Name;

            public object Field;

            [Dependency]
            public IUnityContainer Container;
        }

        public class ObjectWithNamedDependency
        {
            [Dependency(Name)]
            public string Field;

            [Dependency]
            public IUnityContainer Container;
        }

        public interface IFoo { }
        public class Foo : IFoo { }

        public class ObjectWithOptionalFields
        {
            [Dependency]
            public string Name;

            public IFoo Field;

            [Dependency]
            public IUnityContainer Container;
        }


        public class ObjectWithAttributes
        {
            [Dependency("name1")]
            public string Dependency;

            [OptionalDependency("other")]
            public string Optional;
        }

        public class ObjectWithFourFields : ObjectWithThreeFields
        {
            public object SubField;

            public readonly object ReadOnlyField;
        }

        public class ObjectWithDependency
        {
            public ObjectWithDependency(ObjectWithThreeFields obj)
            {
                Dependency = obj;
            }

            public ObjectWithThreeFields Dependency { get; }

        }

        public class NoAttributeType
        {
            public object Value;
            public int Called = 1;
        }

        public class DependencyAttributeType
        {
            [Dependency]
            public object Value;
            public int Called = 2;
        }

        public class NamedDependencyAttributeType
        {
            [Dependency(Name)]
            public string Value;
            public int Called = 3;
        }

        public class OptionalDependencyAttributeType
        {
            [OptionalDependency]
            public object Value;
            public int Called = 4;
        }

        public class OptionalNamedDependencyAttributeType
        {
            [OptionalDependency(Name)]
            public string Value;
            public int Called = 5;
        }

        public class OptionalDependencyAttributeMissingType
        {
            [OptionalDependency]
            public IDisposable Value;
            public int Called = 6;
        }

        public class OptionalNamedDependencyAttributeMissingType
        {
            [OptionalDependency(Name)]
            public IDisposable Value;
            public int Called = 7;
        }

#pragma warning disable 649
        public class DependencyAttributePrivateType
        {
            [Dependency]
            private object Dependency;

            public object Value => Dependency;
            public int Called = 8;
        }
#pragma warning restore 649

        public class DependencyAttributeProtectedType
        {
            [Dependency] protected object Dependency;

            public object Value => Dependency;
            public int Called = 9;
        }

        #endregion
    }
}
