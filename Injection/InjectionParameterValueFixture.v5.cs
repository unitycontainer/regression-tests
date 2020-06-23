using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
#if NET45
using Microsoft.Practices.Unity;
#else
using Unity;
using Unity.Injection;
using Unity.Policy;
#endif

namespace Unity.Regression.Tests
{
    public partial class InjectionParameterValueFixture
    {
        [Ignore]
        [TestMethod]
        public void DependencyParameterCreatesExpectedResolver()
        {
            //Type expectedType = typeof(ILogger);

            //ResolvedParameter parameter = new ResolvedParameter<ILogger>();
            //IDependencyResolverPolicy resolver = parameter.GetResolverPolicy(expectedType);

            //Assert.IsInstanceOfType(resolver, typeof(NamedTypeDependencyResolverPolicy));
            //Assert.AreEqual(expectedType, ((NamedTypeDependencyResolverPolicy)resolver).Type);
            //Assert.IsNull(((NamedTypeDependencyResolverPolicy)resolver).Name);
        }

        [Ignore]
        [TestMethod]
        public void ResolvedParameterHandledNamedTypes()
        {
            //Type expectedType = typeof(ILogger);
            //string name = "special";

            //ResolvedParameter parameter = new ResolvedParameter(expectedType, name);
            //IDependencyResolverPolicy resolver = parameter.GetResolverPolicy(expectedType);

            //Assert.IsInstanceOfType(resolver, typeof(NamedTypeDependencyResolverPolicy));
            //Assert.AreEqual(expectedType, ((NamedTypeDependencyResolverPolicy)resolver).Type);
            //Assert.AreEqual(name, ((NamedTypeDependencyResolverPolicy)resolver).Name);
        }

        [Ignore]
        [TestMethod]
        public void ObjectsConverterToInjectionParametersResolveCorrectly()
        {
            //List<InjectionParameterValue> values = GetParameterValues(15);

            //InjectionParameter parameter = (InjectionParameter)values[0];
            //Assert.AreEqual(typeof(int), parameter.ParameterType);
            //IDependencyResolverPolicy policy = parameter.GetResolverPolicy(null);
            //int result = (int)policy.Resolve(null);

            //Assert.AreEqual(15, result);
        }

        [Ignore]
        [TestMethod]
        public void ConcreteTypesMatch()
        {
            //List<InjectionParameterValue> values = GetParameterValues(typeof(int), typeof(string), typeof(User));
            //Type[] expectedTypes = Sequence.Collect(typeof(int), typeof(string), typeof(User));
            //for (int i = 0; i < values.Count; ++i)
            //{
            //    Assert.IsTrue(values[i].MatchesType(expectedTypes[i]));
            //}
        }

        [Ignore]
        [TestMethod]
        public void TypesImplicitlyConvertToResolvedDependencies()
        {
            //List<InjectionParameterValue> values = GetParameterValues(typeof(int));

            //Assert.AreEqual(1, values.Count);
            //Assert.IsInstanceOfType(values[0], typeof(ResolvedParameter));
        }

        [Ignore]
        [TestMethod]
        public void TypesAndObjectsImplicitlyConvertToInjectionParameters()
        {
            //List<InjectionParameterValue> values = GetParameterValues(
            //    15, typeof(string), 22.5);

            //Assert.AreEqual(3, values.Count);
            //Assert.IsInstanceOfType(values[0], typeof(InjectionParameter));
            //Assert.IsInstanceOfType(values[1], typeof(ResolvedParameter));
            //Assert.IsInstanceOfType(values[2], typeof(InjectionParameter));
        }

        //private List<InjectionParameterValue> GetParameterValues(params object[] values)
        //{
        //    return InjectionParameterValue.ToParameters(values).ToList();
        //}

        private void AssertExpectedValue(InjectionParameter parameter, Type expectedType, object expectedValue)
        {
            //IDependencyResolverPolicy resolver = parameter.GetResolverPolicy(expectedType);
            //object result = resolver.Resolve(null);

            //Assert.AreEqual(expectedType, parameter.ParameterType);
            //Assert.IsInstanceOfType(resolver, typeof(LiteralValueDependencyResolverPolicy));
            //Assert.AreEqual(expectedValue, result);
        }
    }
}
