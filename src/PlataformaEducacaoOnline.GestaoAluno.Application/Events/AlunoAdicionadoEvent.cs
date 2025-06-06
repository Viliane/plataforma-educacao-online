using PlataformaEducacaoOnline.Core.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaEducacaoOnline.GestaoAluno.Application.Events
{
    public class AlunoAdicionadoEvent : Event
    {

        public int Id { get; set; }
        public string Nome { get; set; }

        public AlunoAdicionadoEvent(Guid id, string nome)
        {
            AggregateId = id;
            Nome = nome;
        }
    }
}
