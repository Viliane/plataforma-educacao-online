using PlataformaEducacaoOnline.Core.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaEducacaoOnline.GestaoConteudo.Application.Events
{
    public class MaterialAulaRemovidaEvent : Event
    {
        public Guid materialId { get; private set; }
        public string Nome { get; private set; }
        public Guid AulaId { get; private set; }

        public MaterialAulaRemovidaEvent(Guid materialId, string nome, Guid aulaId)
        {
            AggregateId = materialId;
            Nome = nome;
            AulaId = aulaId;
        }
    }
}


    
