using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
#if NET45
using Microsoft.Practices.Unity;
#else
using Unity;
using Unity.Injection;
using Unity.Resolution;
#endif

namespace Specification
{
    public partial class Parameters
    {
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Resolved_ProvidingConcreteTypeForGenericFails()
        {
            // Act
            Container.RegisterType(typeof(GenericService<,,>), 
               new InjectionMethod("Method", Resolve.Parameter(typeof(string))));
        }

        [TestMethod]
        public void Resolved_GenericParameterT1()
        {
            // Setup
            Container.RegisterType(typeof(GenericService<,,>),
               new InjectionMethod("Method", Resolve.Generic("T1")));

            // Act
            var result = Container.Resolve<GenericService<object, string, int>>();

            // Verify
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Called, 1);
            Assert.IsInstanceOfType(result.Value, typeof(object));
        }

        [TestMethod]
        public void Resolved_GenericParameterT2()
        {
            // Setup
            Container.RegisterType(typeof(GenericService<,,>),
               new InjectionMethod("Method", Resolve.Generic("T2")));

            // Act
            var result = Container.Resolve<GenericService<object, string, int>>();

            // Verify
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Called, 2);
            Assert.IsInstanceOfType(result.Value, typeof(string));
            Assert.AreEqual(result.Value, "other");
        }

        [TestMethod]
        public void Resolved_GenericParameterT3()
        {
            // Setup
            Container.RegisterInstance(10);
            Container.RegisterInstance("1", 1);
            Container.RegisterInstance("2", 2);
            Container.RegisterInstance("1", "1");
            Container.RegisterInstance("2", "2");
            Container.RegisterType(typeof(GenericService<,,>),
               new InjectionMethod("Method", Resolve.Generic("T3")));

            // Act
            var result = Container.Resolve<GenericService<object, string, int>>();

            // Verify
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Called, 3);
            Assert.AreEqual(result.Value, 10);
        }

        [TestMethod]
        public void Resolved_GenericParameterT1WithName()
        {
            // Setup
            Container.RegisterType(typeof(GenericService<,,>),
               new InjectionMethod("Method", Resolve.Generic("T1", "1")));

            // Act
            var result = Container.Resolve<GenericService<object, string, int>>();

            // Verify
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Called, 1);
            Assert.IsInstanceOfType(result.Value, typeof(object));
        }

        [TestMethod]
        public void Resolved_GenericParameterT2WithName()
        {
            // Setup
            Container.RegisterInstance(10);
            Container.RegisterInstance("1", 1);
            Container.RegisterInstance("2", 2);
            Container.RegisterInstance("1", "1");
            Container.RegisterInstance("2", "2");
            Container.RegisterType(typeof(GenericService<,,>),
               new InjectionMethod("Method", Resolve.Generic("T2", "1")));

            // Act
            var result = Container.Resolve<GenericService<object, string, int>>();

            // Verify
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Called, 2);
            Assert.IsInstanceOfType(result.Value, typeof(string));
            Assert.AreEqual(result.Value, "1");
        }

        [TestMethod]
        public void Resolved_GenericParameterT3WithName()
        {
            // Setup
            Container.RegisterInstance(10);
            Container.RegisterInstance("1", 1);
            Container.RegisterInstance("2", 2);
            Container.RegisterInstance("1", "1");
            Container.RegisterInstance("2", "2");
            Container.RegisterType(typeof(GenericService<,,>),
               new InjectionMethod("Method", Resolve.Generic("T3", "1")));

            // Act
            var result = Container.Resolve<GenericService<object, string, int>>();

            // Verify
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Called, 3);
            Assert.AreEqual(result.Value, 1);
        }
    }
}
