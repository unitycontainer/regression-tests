﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
#if NET45
using Microsoft.Practices.Unity;
#else
using Unity;
using Unity.Injection;
using Unity.Resolution;
#endif

namespace Spec.Constructors
{
    public partial class Constructors
    {
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Injection_NoConstructor()
        {
            // Act
            Container.RegisterType<TypeWithAmbiguousCtors>(
                new InjectionConstructor(new ResolvedParameter(typeof(object))));
        }

        [TestMethod]
        public virtual void Injection_MultipleConstructor()
        {
            // Arrange
            Container.RegisterType<TypeWithAmbiguousCtors>(
                new InjectionConstructor(),
                new InjectionConstructor());

            // Act
            var instance = Container.Resolve<TypeWithAmbiguousCtors>();

            // Validate
            Assert.IsNotNull(instance);
            Assert.AreEqual(TypeWithAmbiguousCtors.One, instance.Signature);
        }

    }

    public partial class Constructors_Diagnostic
    {
        [TestMethod]
        [ExpectedException(typeof(ResolutionFailedException))]
        public override void Injection_MultipleConstructor() 
            => base.Injection_MultipleConstructor();
    }
}