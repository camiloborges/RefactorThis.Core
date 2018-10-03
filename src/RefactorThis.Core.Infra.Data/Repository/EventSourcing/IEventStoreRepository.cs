using RefactorThis.Core.Domain.Core.Events;
using System;
using System.Collections.Generic;

namespace RefactorThis.Core.Infra.Data.Repository.EventSourcing
{
    public interface IEventStoreRepository : IDisposable
    {
        void Store(StoredEvent theEvent);

        IList<StoredEvent> All(Guid aggregateId);
    }
}