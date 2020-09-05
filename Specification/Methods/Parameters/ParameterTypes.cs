﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
#if NET45
using Microsoft.Practices.Unity;
#else
using Unity;
#endif

namespace Specification
{
    public partial class Methods
    {
        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public void Parameters_RefParameter()
        {
            // Act
            Container.Resolve<TypeWithMethodWithRefParameter>();
        }

        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public void Parameters_OutParameter()
        {
            // Act
            Container.Resolve<TypeWithMethodWithOutParameter>();
        }
    }

    public partial class Methods_Diagnostic
    {
        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public void ChainedExecuteMethodBaseline()
        {
            // Setup
            Container
                .RegisterType(typeof(ICommand<>), typeof(ConcreteCommand<>),
                    Invoke.Method("ChainedExecute"));

            // Act
            var result = Container.Resolve<ICommand<Account>>();

            // Verify
            Assert.Fail();
        }
    }
}
