using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
#if V4
using Microsoft.Practices.Unity;
#else
using Unity;
using Unity.Injection;
using Unity.Resolution;
#endif


namespace Unity.V4
{
    [TestClass]
    public class InjectionParameterValueFixture
    {
        [TestMethod]
        public void InjectionParameterReturnsExpectedValue()
        {
            int expected = 12;
            InjectionParameter parameter = new InjectionParameter(expected);
            AssertExpectedValue(parameter, typeof(int), expected);
        }

        [TestMethod]
        public void InjectionParameterCanTakeExplicitType()
        {
            double expected = Math.E;
            InjectionParameter parameter = new InjectionParameter<double>(expected);
            AssertExpectedValue(parameter, typeof(double), expected);
        }

        [TestMethod]
        public void InjectionParameterCanReturnNull()
        {
            string expected = null;
            InjectionParameter parameter = new InjectionParameter(typeof(string), expected);
            AssertExpectedValue(parameter, typeof(string), expected);
        }

        [Ignore]
        [TestMethod]
        public void DependencyParameterCreatesExpectedResolver()
        {
            //Type expectedType = typeof(ILogger);

            //ResolvedParameter parameter = new ResolvedParameter<ILogger>();
            //IDependencyResolverPolicy resolver = parameter.GetResolverPolicy(expectedType);

            //AssertExtensions.IsInstanceOfType(resolver, typeof(NamedTypeDependencyResolverPolicy));
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
            //var resolver = parameter.GetResolverPolicy(expectedType);

            //Assert.IsInstanceOfType(resolver, typeof(NamedTypeDependencyResolverPolicy));
            //Assert.AreEqual(expectedType, ((NamedTypeDependencyResolverPolicy)resolver).Type);
            //Assert.AreEqual(name, ((NamedTypeDependencyResolverPolicy)resolver).Name);
        }

#if NET45
        [TestMethod]
        public void TypesImplicitlyConvertToResolvedDependencies()
        {
            List<InjectionParameterValue> values = GetParameterValues(typeof(int));

            Assert.AreEqual(1, values.Count);
            Assert.IsInstanceOfType(values[0], typeof(ResolvedParameter));
        }

        [TestMethod]
        public void ObjectsConverterToInjectionParametersResolveCorrectly()
        {
            List<InjectionParameterValue> values = GetParameterValues(15);

            InjectionParameter parameter = (InjectionParameter)values[0];
            Assert.AreEqual(typeof(int), parameter.ParameterType);
            var policy = parameter.GetResolverPolicy(null);
            int result = (int)policy.Resolve(null);

            Assert.AreEqual(15, result);
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

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreatingInjectionParameterWithNullValueThrows()
        {
            new InjectionParameter(null);
        }

        private List<InjectionParameterValue> GetParameterValues(params object[] values)
        {
            return InjectionParameterValue.ToParameters(values).ToList();
        }
#endif
        [TestMethod]
        public void InjectionParameterForNullValueReturnsExpectedValueIfTypeIsSuppliedExplicitly()
        {
            var parameter = new InjectionParameter(typeof(string), null);

            AssertExpectedValue(parameter, typeof(string), null);
        }

        private void AssertExpectedValue(InjectionParameter parameter, Type expectedType, object expectedValue)
        {
#if NET45
            var resolver = parameter.GetResolverPolicy(expectedType);
            object result = resolver.Resolve(null);

            Assert.AreEqual(expectedType, parameter.ParameterType);
            Assert.IsInstanceOfType(resolver, typeof(Microsoft.Practices.Unity.ObjectBuilder.LiteralValueDependencyResolverPolicy));
            Assert.AreEqual(expectedValue, result);
#else
            Assert.Fail();
#endif
        }

        public interface ILogger
        {
        }

        // A couple of sample objects we're stuffing into our commands
        public class User
        {
            public void DoSomething(string message)
            {
            }
        }
    }
}
