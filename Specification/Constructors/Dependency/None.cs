using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Reflection;
using System.Collections;
#if NET45
using Microsoft.Practices.Unity;
#else
using Unity;
using Unity.Injection;
using Unity.Resolution;
#endif

namespace Specification
{
    public partial class Constructor_Parameters
    {
        public class None_NoDefault_ValueType : PatternBase
        {
            public None_NoDefault_ValueType(int value) => Value = value;
        }

        public class None_NoDefault_ValueType_Missing : PatternBase
        {
            public None_NoDefault_ValueType_Missing(long value) => Value = value;
        }


        public class None_NoDefault_Class : PatternBase
        {
            public None_NoDefault_Class(object value) => Value = value;
        }

        public class None_NoDefault_Class_Missing : PatternBase
        {
            public None_NoDefault_Class_Missing(object value) => Value = value;
        }
       


        public class None_WithDefault_ValueType : PatternBase
        {
            public None_WithDefault_ValueType(int value = 10) => Value = value;
        }

        public class None_WithDefault_ValueType_Missing : PatternBase
        {
            public None_WithDefault_ValueType_Missing(long value = 10) => Value = value;
        }


        public class None_WithDefault_Class : PatternBase
        {
            public None_WithDefault_Class(string value = Name) => Value = value;
        }

        public class None_WithDefault_Class_Missing : PatternBase
        {
            public None_WithDefault_Class_Missing(IList value = null) => Value = value;
        }
    }
}
