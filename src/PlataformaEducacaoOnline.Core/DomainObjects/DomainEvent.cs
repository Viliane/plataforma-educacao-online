using PlataformaEducacaoOnline.Core.Messages;

namespace PlataformaEducacaoOnline.Core.DomainObjects
{
    public class DomainEvent : Event
    {
        public DomainEvent(Guid aggregateId)
        {
            AggregateId = aggregateId;
        }
    }
}
