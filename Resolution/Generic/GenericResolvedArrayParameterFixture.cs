﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
#if NET45
using Microsoft.Practices.Unity;
#else
using Unity;
using Unity.Injection;
#endif

namespace Resolution
{
    public partial class Generics
    {
        [TestMethod]
        public void CanCallConstructorTakingGenericParameterArray()
        {
            Container.RegisterType(
                    typeof(ClassWithOneArrayGenericParameter<>),
                    new InjectionConstructor(new GenericParameter("T[]")));

            Account a0 = new Account();
            Container.RegisterInstance<Account>("a0", a0);
            Account a1 = new Account();
            Container.RegisterInstance<Account>("a1", a1);
            Account a2 = new Account();
            Container.RegisterInstance<Account>(a2);

            ClassWithOneArrayGenericParameter<Account> result
                = Container.Resolve<ClassWithOneArrayGenericParameter<Account>>();
            Assert.IsFalse(result.DefaultConstructorCalled);
            Assert.AreEqual(2, result.InjectedValue.Length);
            Assert.AreSame(a0, result.InjectedValue[0]);
            Assert.AreSame(a1, result.InjectedValue[1]);
        }

        [TestMethod]
        public void CanCallConstructorTakingGenericParameterArrayWithValues()
        {
            Container.RegisterType(
                    typeof(ClassWithOneArrayGenericParameter<>),
                    new InjectionConstructor(
                        new GenericResolvedArrayParameter(
                            "T",
                            new GenericParameter("T", "a2"),
                            new GenericParameter("T", "a1"))));

            Account a0 = new Account();
            Container.RegisterInstance<Account>("a0", a0);
            Account a1 = new Account();
            Container.RegisterInstance<Account>("a1", a1);
            Account a2 = new Account();
            Container.RegisterInstance<Account>("a2", a2);

            ClassWithOneArrayGenericParameter<Account> result
                = Container.Resolve<ClassWithOneArrayGenericParameter<Account>>();
            Assert.IsFalse(result.DefaultConstructorCalled);
            Assert.AreEqual(2, result.InjectedValue.Length);
            Assert.AreSame(a2, result.InjectedValue[0]);
            Assert.AreSame(a1, result.InjectedValue[1]);
        }

        [TestMethod]
        public void CanSetPropertyWithGenericParameterArrayType()
        {
            Container.RegisterType(typeof(ClassWithOneArrayGenericParameter<>),
                                       new InjectionConstructor(),
                                       new InjectionProperty("InjectedValue", new GenericParameter("T()")));

            Account a0 = new Account();
            Container.RegisterInstance<Account>("a1", a0);
            Account a1 = new Account();
            Container.RegisterInstance<Account>("a2", a1);
            Account a2 = new Account();
            Container.RegisterInstance<Account>(a2);

            ClassWithOneArrayGenericParameter<Account> result
                = Container.Resolve<ClassWithOneArrayGenericParameter<Account>>();
            Assert.IsTrue(result.DefaultConstructorCalled);
            Assert.AreEqual(2, result.InjectedValue.Length);
            Assert.AreSame(a0, result.InjectedValue[0]);
            Assert.AreSame(a1, result.InjectedValue[1]);
        }

        [TestMethod]
#if NET45
        [ExpectedException(typeof(Exception), AllowDerivedTypes = true)]
#else
        [ExpectedException(typeof(InvalidOperationException))]
#endif
        public void AppropriateExceptionIsThrownWhenNoMatchingConstructorCanBeFound()
        {
            Container.RegisterType(typeof(ClassWithOneGenericParameter<>),
                    new InjectionConstructor(new GenericResolvedArrayParameter("T")));
        }
    }
}
