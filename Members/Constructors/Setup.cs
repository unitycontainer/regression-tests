﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
#if NET45
using Microsoft.Practices.Unity;
#else
using Unity;
#endif

namespace Constructors
{
    [TestClass]
    public partial class Specification
    {
        protected const string Name = "name";
        protected IUnityContainer Container;

        [TestInitialize]
        public virtual void TestInitialize() => Container = new UnityContainer();

        [TestMethod]
        public void Baseline()
        { }
    }


    #region Test Data


    #endregion
}
