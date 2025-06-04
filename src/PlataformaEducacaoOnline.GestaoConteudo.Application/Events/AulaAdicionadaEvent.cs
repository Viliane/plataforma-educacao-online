using PlataformaEducacaoOnline.Core.Messages;

namespace PlataformaEducacaoOnline.GestaoConteudo.Application.Events
{
    public class AulaAdicionadaEvent : Event
    {
        public Guid CursoId { get; private set; }
        public string Titulo { get; private set; }
        public string Conteudo { get; private set; }

        public AulaAdicionadaEvent(Guid aulaId, Guid cursoId, string conteudo, string titulo)
        {
            AggregateId = aulaId;
            CursoId = cursoId;
            Titulo = titulo;
            Conteudo = conteudo;
        }
    }
}
