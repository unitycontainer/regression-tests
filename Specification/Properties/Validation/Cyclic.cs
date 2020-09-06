﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
#if NET45
using Microsoft.Practices.Unity;
#else
using Unity;
using Unity.Resolution;
#endif

namespace Specification
{
    public partial class Properties_Diagnostic
    {
        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public void Cyclic_PropertyToInterface()
        {
            // Arrange
            Container.RegisterType<I1, E1>();

            // Act
            Container.Resolve<E1>();
        }

        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public void Cyclic_DependencyOverride()
        {
            // Arrange
            Container.RegisterType<I0, G0>()
                     .RegisterType<I1, G1>();

            //next line throws StackOverflowException
            Container.Resolve<G1>(
                Override.Dependency<I0>(
                    Resolve.Dependency<I1>()));
        }
    }
}
