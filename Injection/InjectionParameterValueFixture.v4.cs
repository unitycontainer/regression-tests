using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.ObjectBuilder;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Unity.Regression.Tests
{
    public partial class InjectionParameterValueFixture
    {
        [TestMethod]
        public void DependencyParameterCreatesExpectedResolver()
        {
            Type expectedType = typeof(ILogger);

            ResolvedParameter parameter = new ResolvedParameter<ILogger>();
            IDependencyResolverPolicy resolver = parameter.GetResolverPolicy(expectedType);

            Assert.IsInstanceOfType(resolver, typeof(NamedTypeDependencyResolverPolicy));
            Assert.AreEqual(expectedType, ((NamedTypeDependencyResolverPolicy)resolver).Type);
            Assert.IsNull(((NamedTypeDependencyResolverPolicy)resolver).Name);
        }

        [TestMethod]
        public void ResolvedParameterHandledNamedTypes()
        {
            Type expectedType = typeof(ILogger);
            string name = "special";

            ResolvedParameter parameter = new ResolvedParameter(expectedType, name);
            IDependencyResolverPolicy resolver = parameter.GetResolverPolicy(expectedType);

            Assert.IsInstanceOfType(resolver, typeof(NamedTypeDependencyResolverPolicy));
            Assert.AreEqual(expectedType, ((NamedTypeDependencyResolverPolicy)resolver).Type);
            Assert.AreEqual(name, ((NamedTypeDependencyResolverPolicy)resolver).Name);
        }

        [TestMethod]
        public void ObjectsConverterToInjectionParametersResolveCorrectly()
        {
            List<InjectionParameterValue> values = GetParameterValues(15);

            InjectionParameter parameter = (InjectionParameter)values[0];
            Assert.AreEqual(typeof(int), parameter.ParameterType);
            IDependencyResolverPolicy policy = parameter.GetResolverPolicy(null);
            int result = (int)policy.Resolve(null);

            Assert.AreEqual(15, result);
        }

        [TestMethod]
        public void ConcreteTypesMatch()
        {
            List<InjectionParameterValue> values = GetParameterValues(typeof(int), typeof(string), typeof(object));
            Type[] expectedTypes = Sequence.Collect(typeof(int), typeof(string), typeof(object));
            for (int i = 0; i < values.Count; ++i)
            {
                Assert.IsTrue(values[i].MatchesType(expectedTypes[i]));
            }
        }


        [TestMethod]
        public void TypesImplicitlyConvertToResolvedDependencies()
        {
            List<InjectionParameterValue> values = GetParameterValues(typeof(int));

            Assert.AreEqual(1, values.Count);
            Assert.IsInstanceOfType(values[0], typeof(ResolvedParameter));
        }

        [TestMethod]
        public void TypesAndObjectsImplicitlyConvertToInjectionParameters()
        {
            List<InjectionParameterValue> values = GetParameterValues(
                15, typeof(string), 22.5);

            Assert.AreEqual(3, values.Count);
            Assert.IsInstanceOfType(values[0], typeof(InjectionParameter));
            Assert.IsInstanceOfType(values[1], typeof(ResolvedParameter));
            Assert.IsInstanceOfType(values[2], typeof(InjectionParameter));
        }

        private List<InjectionParameterValue> GetParameterValues(params object[] values)
        {
            return InjectionParameterValue.ToParameters(values).ToList();
        }

        private void AssertExpectedValue(InjectionParameter parameter, Type expectedType, object expectedValue)
        {
            IDependencyResolverPolicy resolver = parameter.GetResolverPolicy(expectedType);
            object result = resolver.Resolve(null);

            Assert.AreEqual(expectedType, parameter.ParameterType);
            Assert.IsInstanceOfType(resolver, typeof(LiteralValueDependencyResolverPolicy));
            Assert.AreEqual(expectedValue, result);
        }
    }
}
