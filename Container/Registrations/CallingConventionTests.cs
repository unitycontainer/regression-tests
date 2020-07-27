using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity.Regression.Tests;
using System.Linq;
using System.Collections.Generic;
using System;
#if NET45
using Microsoft.Practices.Unity;
#else
using Unity.Injection;
using Unity.Lifetime;
using Unity;
#endif

namespace Container.Registrations
{
    [TestClass]
    public class CallingConventionTests
    {
    }
}
