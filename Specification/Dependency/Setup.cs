using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
#if NET45
using Microsoft.Practices.Unity;
#else
using Unity;
#endif

namespace Specification
{
    public abstract class PatternBase
    {
        //public int Member { get; protected set; }

        public object Value { get; protected set; }

    }

    [TestClass]
    public abstract partial class DependencyPattern
    {
        protected const string Name = "name";
        protected const int Integer = 100;
        protected static object Singleton = new object();

        protected IUnityContainer Container;

        [TestInitialize]
        public virtual void TestInitialize()
        {
            Container = new UnityContainer();

            Container.RegisterInstance(Name);
            Container.RegisterInstance(Integer);
            Container.RegisterInstance(Singleton);
        }
    }
}
