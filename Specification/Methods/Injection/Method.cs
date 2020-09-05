using Microsoft.VisualStudio.TestTools.UnitTesting;
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
        public void Injection_InjectedMethodIsCalled()
        {
            // Setup
            Container.RegisterType<LegalInjectionMethod>(
                Invoke.Method(nameof(LegalInjectionMethod.InjectMe)));

            // Act
            LegalInjectionMethod result = Container.Resolve<LegalInjectionMethod>();

            // Verify
            Assert.IsTrue(result.WasInjected);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Injection_MethodWithOutParameter()
        {
            // Act
            Container.RegisterType<OutParams>(Invoke.Method(nameof(OutParams.InjectMe), 12));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Injection_MethodWithRefParameter()
        {
            // Act
            Container.RegisterType<RefParams>(
                    Invoke.Method(nameof(RefParams.InjectMe), 15));
        }

        [TestMethod]
        public void Injection_InheritedMethod()
        {
            // Setup
            Container.RegisterType<InheritedClass>(
                Invoke.Method(nameof(InheritedClass.InjectMe)));

            // Act
            var result = Container.Resolve<InheritedClass>();

            // Verify
            Assert.IsTrue(result.WasInjected);
        }


        [TestMethod]
        public virtual void Injection_MethodPassingVoid()
        {
            // Setup
            Container.RegisterType(typeof(GuineaPig),
                Invoke.Method(nameof(GuineaPig.Inject1)));

            // Act
            GuineaPig pig = Container.Resolve<GuineaPig>();

            // Verify
            Assert.AreEqual("void", pig.StringValue);
        }

        [TestMethod]
        public virtual void Injection_ReturningVoid()
        {
            // Setup
            Container.RegisterType(typeof(GuineaPig),
                Invoke.Method(nameof(GuineaPig.Inject2), "Hello"));

            // Act
            GuineaPig pig = Container.Resolve<GuineaPig>();

            // Verify
            Assert.AreEqual("Hello", pig.StringValue);
        }

        [TestMethod]
        public virtual void Injection_ReturningInt()
        {
            // Setup
            Container.RegisterType(typeof(GuineaPig),
                Invoke.Method(nameof(GuineaPig.Inject3), 17));

            // Act
            GuineaPig pig = Container.Resolve<GuineaPig>();


            // Verify
            Assert.AreEqual(17, pig.IntValue);
        }

        [TestMethod]
        public virtual void Injection_MultipleMethods()
        {
            // Setup
            Container.RegisterType<GuineaPig>(
                    Invoke.Method(nameof(GuineaPig.Inject3), 37),
                    Invoke.Method(nameof(GuineaPig.Inject2), "Hi there"));

            // Act
            GuineaPig pig = Container.Resolve<GuineaPig>();


            // Verify
            Assert.AreEqual(37, pig.IntValue);
            Assert.AreEqual("Hi there", pig.StringValue);
        }

        [TestMethod]
        public virtual void Injection_StaticMethod()
        {
            // Act
            Container.Resolve<GuineaPig>();

            // Verify
            Assert.IsFalse(GuineaPig.StaticMethodWasCalled);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Injection_InjectingStaticMethod()
        {
            // Verify
            Container.RegisterType<GuineaPig>(
                Invoke.Method(nameof(GuineaPig.ShouldntBeCalled)));
        }
    }
}
