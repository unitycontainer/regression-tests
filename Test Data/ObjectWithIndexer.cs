#if NET45
using Microsoft.Practices.Unity;
#else
using Unity;
#endif

namespace Unity.Regression.Tests
{
    public class ObjectWithIndexer
    {
        [Dependency]
        public object this[int index]
        {
            get { return null; }
            set { }
        }

        public bool Validate()
        {
            return true;
        }
    }
}
