using Microsoft.VisualStudio.TestTools.UnitTesting;
#if NET45
using Microsoft.Practices.Unity;
#else
using Unity;
#endif


namespace Unity.Regression.Tests
{
    public class ObjectWithStaticAndInstanceProperties
    {
        [Dependency]
        public static object StaticProperty { get; set; }

        [Dependency]
        public object InstanceProperty { get; set; }

        public void Validate()
        {
            Assert.IsNull(StaticProperty);
            Assert.IsNotNull(this.InstanceProperty);
        }
    }
}
