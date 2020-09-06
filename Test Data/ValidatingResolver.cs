using System;
#if NET45
using Microsoft.Practices.ObjectBuilder2;
#else
using Unity;
using Unity.Resolution;
#endif

namespace Unity.Regression.Tests
{
    public class ValidatingResolver
#if NET45
            : IDependencyResolverPolicy
#else
            : IResolve
#endif

    {
        private object _value;

        public ValidatingResolver(object value)
        {
            _value = value;
        }

#if NET45

        public object Resolve(IBuilderContext context)
        {
            Type = context.BuildKey.Type;
            Name = context.BuildKey.Name;

            return _value;
        }
#else
            public object Resolve<TContext>(ref TContext context) where TContext : IResolveContext
            {
                Type = context.Type;
                Name = context.Name;

                return _value;
            }
#endif

        public Type Type { get; private set; }

        public string Name { get; private set; }
    }
}
