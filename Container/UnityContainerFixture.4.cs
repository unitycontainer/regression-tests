using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.ObjectBuilder;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Unity.Regression.Tests
{
    public partial class UnityContainerFixture
    {

        [TestMethod]
        public void GetReasonableExceptionWhenRegisteringNullInstance()
        {
            IUnityContainer container = new UnityContainer();
            Assert.ThrowsException<ArgumentNullException>(() =>
                {
                    container.RegisterInstance<SomeType>(null);
                });
        }

        [TestMethod]
        public void ContainerResolvesItselfEvenAfterGarbageCollect()
        {
            IUnityContainer container = new UnityContainer();
            container.AddNewExtension<GarbageCollectingExtension>();

            Assert.IsNotNull(container.Resolve<IUnityContainer>());
        }

        public class GarbageCollectingExtension : UnityContainerExtension
        {
            protected override void Initialize()
            {
                Context.Strategies.AddNew<GarbageCollectingStrategy>(UnityBuildStage.Setup);
            }

            public class GarbageCollectingStrategy : BuilderStrategy
            {
                public override void PreBuildUp(IBuilderContext context)
                {
                    GC.Collect();
                }
            }
        }
    }
}
