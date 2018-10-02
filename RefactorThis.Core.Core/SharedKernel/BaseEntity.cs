using System;
using System.Collections.Generic;

namespace RefactorThis.Core.SharedKernel
{
    // This can be modified to BaseEntity<TId> to support multiple key types (e.g. Guid)
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
//        public List<BaseDomainEvent> Events = new List<BaseDomainEvent>();
    }
}