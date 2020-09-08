using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
#if NET45
using Microsoft.Practices.Unity;
#else
using Unity;
#endif

namespace Specification
{
    public abstract partial class DependencyPattern
    {
        [DataTestMethod]
        [DynamicData(nameof(Data))]
        public void None(string name, object expected)
        {
            var type = Type.GetType($"{GetType().FullName}+{name}");
            var actual = Container.Resolve(type) as PatternBase;

            Assert.IsNotNull(actual);
            Assert.AreEqual(expected, actual.Value);
        }

        [DataTestMethod]
        [DynamicData(nameof(Data_Missing))]
        [ExpectedException(typeof(ResolutionFailedException))]
        public void None_Missing(string name)
        {
            var value = Container.Resolve<PatternBase>(name);

            Assert.IsNotNull(value);
        }


        /*
         */

        public static IEnumerable<object[]> Data
        {
            get
            {
                yield return new object[] { "None_NoDefault_ValueType", Integer };
                yield return new object[] { "None_NoDefault_Class",     Singleton };
              //yield return new object[] { "None_NoDefault_Generic",   Integer };
                yield return new object[] { "None_WithDefault_ValueType", 10     };
                yield return new object[] { "None_WithDefault_Class"             };
             // yield return new object[] { "None_WithDefault_Generic"           };
            }
        }

        public static IEnumerable<object[]> Data_Missing
        {
            get
            {
                yield return new object[] { "None_NoDefault_ValueType_Missing" };
                yield return new object[] { "None_NoDefault_Class_Missing"    };
                //yield return new object[] { "None_NoDefault_Generic_Missing"  };
                yield return new object[] { "None_WithDefault_ValueType_Missing" };
                yield return new object[] { "None_WithDefault_Class_Missing" };
                yield return new object[] { "None_WithDefault_Generic_Missing" };
            }
        }

    }
}
