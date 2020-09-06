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
    public partial class Parameters
    {

#if !NET45
        [TestMethod]
        public void Resolved_ResolveCorrespondingType()
        {
            // Setup
            Container
                .RegisterType(typeof(ICommand<>), typeof(ConcreteCommand<>),
                    new InjectionMethod("Execute", Resolve.Parameter()));

            // Act
            var result = Container.Resolve<ICommand<Account>>();

            // Verify
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Executed, typeof(Account));
        }

        [TestMethod]
        public void Resolved_ResolveCorrespondingTypeNamed()
        {
            // Setup
            Container.RegisterInstance(10);
            Container.RegisterInstance("1", 1);
            Container.RegisterInstance("2", 2);
            Container.RegisterInstance("1", "1");
            Container.RegisterInstance("2", "2");
            Container
                .RegisterType(typeof(ICommand<>), typeof(ConcreteCommand<>),
                    new InjectionMethod("Execute", Resolve.Parameter("1")));

            // Act
            var result = Container.Resolve<ICommand<string>>();

            // Verify
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Executed, Container.Resolve<string>("1"));
        }

        [TestMethod]
        public void Resolved_NamedImplicitOpenGeneric()
        {
            // Setup
            Container
                .RegisterType(typeof(ICommand<>), typeof(ConcreteCommand<>), "inner")
                .RegisterType(typeof(ICommand<>), typeof(ConcreteCommand<>),
                    new InjectionMethod("ChainedExecute", Resolve.Parameter("inner")));

            // Act
            var result = Container.Resolve<ICommand<Account>>();

            // Verify
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Chained, typeof(ICommand<Account>));
        }
#endif

        [TestMethod]
        public void Resolved_NamedExplicitOpenGeneric()
        {
            // Setup
            Container
                .RegisterType(typeof(ICommand<>), typeof(ConcreteCommand<>), "inner")
                .RegisterType(typeof(ICommand<>), typeof(ConcreteCommand<>),
                    new InjectionMethod("ChainedExecute", Resolve.Parameter(typeof(ICommand<>), "inner")));

            // Act
            var result = Container.Resolve<ICommand<Account>>();

            // Verify
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Chained, typeof(ICommand<Account>));
        }

        [TestMethod]
        public void Resolved_GenericInjectionIsCalled()
        {
            // Setup
            Container
                .RegisterType(typeof(ICommand<>), typeof(LoggingCommand<>),
                    new InjectionConstructor(Resolve.Parameter(typeof(ICommand<>), "concrete")),
                    new InjectionMethod("ChainedExecute", Resolve.Parameter(typeof(ICommand<>), "inner")))
                .RegisterType(typeof(ICommand<>), typeof(ConcreteCommand<>), "concrete")
                .RegisterType(typeof(ICommand<>), typeof(ConcreteCommand<>), "inner");

            // Act
            ICommand<Account> result = Container.Resolve<ICommand<Account>>();
            LoggingCommand<Account> lc = (LoggingCommand<Account>)result;

            // Verify
            Assert.IsTrue(lc.ChainedExecuteWasCalled);
        }
    }
}
