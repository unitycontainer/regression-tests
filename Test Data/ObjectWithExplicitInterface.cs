using Microsoft.VisualStudio.TestTools.UnitTesting;
#if NET45
using Microsoft.Practices.Unity;
#else
using Unity;
#endif

namespace Unity.Regression.Tests
{
    public interface ISomeCommonProperties
    {
        [Dependency]
        ILogger Logger { get; set; }

        [Dependency]
        object SyncObject { get; set; }
    }

    public class ObjectWithExplicitInterface : ISomeCommonProperties
    {
        private ILogger logger;
        private object syncObject;

        private object somethingElse;

        [Dependency]
        public object SomethingElse
        {
            get { return somethingElse; }
            set { somethingElse = value; }
        }

        [Dependency]
        ILogger ISomeCommonProperties.Logger
        {
            get { return logger; }
            set { logger = value; }
        }

        [Dependency]
        object ISomeCommonProperties.SyncObject
        {
            get { return syncObject; }
            set { syncObject = value; }
        }

        public void ValidateInterface()
        {
            Assert.IsNotNull(logger);
            Assert.IsNotNull(syncObject);
        }
    }
}
