using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#if NET45
using  Microsoft.Practices.Unity;
#else
using Unity;
#endif


namespace Unity.Regression.Tests
{
    [TestClass]
    public class BaselineFixture
    {
        [TestMethod]
        public void BaselineTest()
        {
            var container = new UnityContainer();
        
        }
    }
}
