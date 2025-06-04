using PlataformaEducacaoOnline.Core.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaEducacaoOnline.GestaoConteudo.Application.Events
{
    public class AulaAtualizadaEvent : Event
    {   
        public string Titulo { get; private set; }
        public string Conteudo { get; private set; }
        public AulaAtualizadaEvent(Guid aulaId, string titulo, string conteudo)
        {
            AggregateId = aulaId;
            Titulo = titulo;
            Conteudo = conteudo;
        }
    }
}
