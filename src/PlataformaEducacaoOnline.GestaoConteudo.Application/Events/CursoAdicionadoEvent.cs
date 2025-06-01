using MediatR;
using PlataformaEducacaoOnline.Core.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaEducacaoOnline.GestaoConteudo.Application.Events
{
    public class CursoAdicionadoEvent : Event
    {
        public Guid UsuarioId { get; private set; }
        public string Nome { get; private set; }
        public string ConteudoProgramatico { get; private set; }
        public CursoAdicionadoEvent(Guid cursoId, Guid usuarioId, string nome, string conteudoProgramatico)
        {
            AggregateId = cursoId;
            UsuarioId = usuarioId;
            Nome = nome;
            ConteudoProgramatico = conteudoProgramatico;
        }
    }
}
