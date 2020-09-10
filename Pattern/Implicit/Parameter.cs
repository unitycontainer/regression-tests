using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
#if V4
using Microsoft.Practices.Unity;
#else
using Unity;
#endif

namespace Specification
{
    public abstract partial class VerificationPattern
    {
        /// <summary>
        /// This test resolves regular POCO type from empty container.
        /// </summary>
        /// <example>
        /// 
        /// public class PocoType 
        /// {
        ///     [InjectionConstructor]
        ///     public PocoType(ref int value) { }
        ///     
        ///     [InjectionMethod]
        ///     public void Method(out int value) { }
        /// }
        /// 
        /// /////////////////////////////////////
        /// 
        ///  var result = new UnityContainer()
        ///      .Resolve(typeof(PocoType));
        ///      
        /// </example>
        /// <param name="name">Name of the <see cref="Type"/> to resolve</param>
        [DataTestMethod]
        [DataRow("NoDefault_Ref")]
        [DataRow("NoDefault_Out")]
        [DataRow("NoDefault_RefStruct")] // TODO: Requires instance for validation
        [ExpectedException(typeof(ResolutionFailedException))]
        public virtual void Unsupported_Implicit_Parameter(string name)
        {
            var type = TargetType(name);

            // Arrange
            RegisterTypes();

            // Act
            _ = Container.Resolve(type);
        }
    }
}
