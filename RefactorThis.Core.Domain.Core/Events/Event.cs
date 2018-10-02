using System;
using MediatR;

namespace RefactorThis.Core.Domain.Core.Events
{ 

    public abstract class Event : Message, INotification
    {
        public DateTime Timestamp { get; private set; }

        protected Event()
        {
            Timestamp = DateTime.Now;
        }
    }
}