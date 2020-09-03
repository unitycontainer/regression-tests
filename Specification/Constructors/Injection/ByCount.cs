using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
#if NET45
using Microsoft.Practices.Unity;
#else
using Unity;
using Unity.Resolution;
#endif

namespace Spec.Constructors
{
    public partial class Constructors
    {
        [TestMethod]
        public void Injection_ByCountFirst()
        {
            // Arrange
            #region inject_count_first_arrange

            Container.RegisterType<SampleType>( Invoke.Constructor( Resolve.Parameter() ));

            #endregion

            // Act
            #region inject_count_first_ctor_act

            var instance = Container.Resolve<SampleType>();

            // 1 == instance.Ctor

            #endregion

            // Validate
            Assert.AreEqual(1, instance.Ctor);
        }

        [TestMethod]
        public void Injection_ByCountFirstGeneric()
        {
            // Arrange
            #region inject_count_first_arrange_generic

            Container.RegisterType(typeof(SampleType<>), Invoke.Constructor( Resolve.Parameter() ));

            #endregion

            // Act
            #region inject_count_first_ctor_act_generic

            var instance = Container.Resolve<SampleType<object>>();

            // 1 == instance.Ctor

            #endregion

            // Validate
            Assert.AreEqual(1, instance.Ctor);
        }

        [TestMethod]
        public virtual void Injection_ByCountNamedGeneric()
        {
            // Arrange
            #region inject_count_named_generic
            Container.RegisterType<IService, Service>()
                     .RegisterType<IService, ServiceOne>("one")
                     .RegisterType<IService, ServiceTwo>("two");

            Container.RegisterType(typeof(SampleType<>),
                Invoke.Constructor(
                    Resolve.Parameter(),
                    Resolve.Parameter()));

            var instance = Container.Resolve<SampleType<object>>();

            // 2 == instance.Ctor
            // typeof(Service) == instance.Service.GetType()

            #endregion

            // Validate
            Assert.AreEqual(2, instance.Ctor);
            Assert.IsTrue(typeof(Service) == instance.Service.GetType());
        }

        [TestMethod]
        public virtual void Injection_ByCountNameOverrideGeneric()
        {
            // Arrange
            Container.RegisterType<IService, ServiceOne>("one")
                     .RegisterType<IService, ServiceTwo>("two");

            #region inject_count_name_override_generic

            Container.RegisterType(typeof(SampleType<>),
                Invoke.Constructor(
                    Resolve.Parameter("two"),
                    Resolve.Parameter()));

            var instance = Container.Resolve<SampleType<object>>();

            // 2 == instance.Ctor
            // typeof(ServiceTwo) == instance.Service.GetType()

            #endregion

            // Validate
            Assert.AreEqual(2, instance.Ctor);
            Assert.IsTrue(typeof(ServiceTwo) == instance.Service.GetType());
        }
    }
}
