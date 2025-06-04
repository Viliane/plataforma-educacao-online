using PlataformaEducacaoOnline.Core.Messages;

namespace PlataformaEducacaoOnline.GestaoConteudo.Application.Events
{
    public class CursoAdicionadoEvent : Event
    {
        public Guid UsuarioId { get; private set; }
        public string Nome { get; private set; }
        public string ConteudoProgramatico { get; private set; }
        public decimal Valor { get; private set; }

        public CursoAdicionadoEvent(Guid cursoId, Guid usuarioId, string nome, string conteudoProgramatico, decimal valor)
        {
            AggregateId = cursoId;
            UsuarioId = usuarioId;
            Nome = nome;
            ConteudoProgramatico = conteudoProgramatico;
            Valor = valor;
        }
    }
}
