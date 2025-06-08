using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaEducacaoOnline.Core.Messages.CommonMessagens.IntegrationEvent
{
    public class PagamentoRecusadoEvent : IntegrationEvent
    {
        public Guid PagamentoId { get; private set; }
        public Guid MatriculaId { get; private set; }
        public Guid AlunoId { get; private set; }
        public Guid CursoId { get; private set; }

        public PagamentoRecusadoEvent(Guid pagamentoId, Guid matriculaId, Guid alunoId, Guid cursoId)
        {
            PagamentoId = pagamentoId;
            MatriculaId = matriculaId;
            AlunoId = alunoId;
            CursoId = cursoId;
        }
    }
}
