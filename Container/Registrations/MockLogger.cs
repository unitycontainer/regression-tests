using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unity.Regression.Tests
{
    public interface ILogger
    {
    }

    public interface ISpecialLogger
    {
    }

    public class MockLogger : ILogger
    {
    }

    public class SpecialLogger : ILogger, ISpecialLogger
    {
    }

    public class MockLoggerWithCtor : ILogger
    {
        public MockLoggerWithCtor(string _)
        {

        }
    }

    public class SpecialLoggerWithCtor : ILogger, ISpecialLogger
    {
        public SpecialLoggerWithCtor(string _)
        {

        }
    }
}
