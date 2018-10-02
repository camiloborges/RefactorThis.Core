
using RefactorThis.Core.SharedKernel;

namespace RefactorThis.Core.Interfaces
{
    public interface IHandle<T> where T : BaseDomainEvent
    {
        void Handle(T domainEvent);
    }
}