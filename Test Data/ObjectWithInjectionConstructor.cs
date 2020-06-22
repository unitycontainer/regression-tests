#if NET45
using Microsoft.Practices.Unity;
#else
using Unity;
#endif

namespace Unity.Regression.Tests
{
    public class ObjectWithInjectionConstructor
    {
        private object constructorDependency;

        public ObjectWithInjectionConstructor(object constructorDependency)
        {
            this.constructorDependency = constructorDependency;
        }

        [InjectionConstructor]
        public ObjectWithInjectionConstructor(string s)
        {
            constructorDependency = s;
        }

        public object ConstructorDependency
        {
            get { return constructorDependency; }
        }
    }
}
