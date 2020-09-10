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
        [DataRow("Required_Dependency_Ref")]
        [DataRow("Optional_Dependency_Ref")]
        [DataRow("Required_Dependency_Out")]
        [DataRow("Optional_Dependency_Out")]
#if !V4
        [DataRow("Required_Dependency_RefStruct")]
        [DataRow("Optional_Dependency_RefStruct")]
#endif
        [ExpectedException(typeof(ResolutionFailedException))]
        public virtual void Unsupported_Parameter(string name)
        {
            // Arrange
            RegisterTypes();

            var type = TargetType(name);

            // Act
            //_ = Container.Resolve(type);
            var instance = Container.Resolve(type) as PatternBase;

            // Validate
            Assert.IsNotNull(instance);
        }
    }
}
