
using RefactorThis.Core.SharedKernel;

namespace RefactorThis.Core.Interfaces
{
    public interface IDomainEventDispatcher
    {
        void Dispatch(BaseDomainEvent domainEvent);
    }
}