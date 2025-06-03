using PlataformaEducacaoOnline.Core.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaEducacaoOnline.GestaoConteudo.Application.Events
{
    public class CursoRemovidoEvent : Event
    {
        public Guid UsuarioId { get; private set; }
        public string Nome { get; private set; }
        public string ConteudoProgramatico { get; private set; }
        public decimal Valor { get; private set; }
        public CursoRemovidoEvent(Guid cursoId, Guid usuarioId, string nome, string conteudoProgramatico, decimal valor)
        {
            AggregateId = cursoId;            
            UsuarioId = usuarioId;
            Nome = nome;
            ConteudoProgramatico = conteudoProgramatico;
            Valor = valor;
        }
    }
}
