using System;
using MediatR;

namespace PG.Management.Core.Application.Consultation.Events
{
    public class SampleEvent : INotification
    {
        public Guid Id { get; }
        public DateTime TimeStamp { get; } = DateTime.Now;

        public SampleEvent(Guid id)
        {
            Id = id;
        }
    }
}
