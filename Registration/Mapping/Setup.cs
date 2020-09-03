
using System;

namespace Registrations.Mapping
{
    #region Test Data

    public interface IFoo<T>
    { }

    public class Foo<T> : IFoo<T>
    { }

    public interface IService<T>
    {
        string Id { get; }

    }

    public class Service<T> : IService<T>
    {
        public string Id { get; } = Guid.NewGuid().ToString();

        public Service()
        {
        }

        public Service(object inject)
        {
            Id = $"Ctor injected with: { inject.GetHashCode() }";
        }
    }

    public class OtherService<T> : IService<T>
    {
        public string Id { get; } = Guid.NewGuid().ToString();
    }

    #endregion
}
