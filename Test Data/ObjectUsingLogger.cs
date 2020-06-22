#if NET45
using Microsoft.Practices.Unity;
#else
using Unity;
#endif

namespace Unity.Regression.Tests
{
    public class ObjectUsingLogger
    {
        private ILogger logger;

        [Dependency]
        public ILogger Logger
        {
            get { return logger; }
            set { logger = value; }
        }
    }
}
