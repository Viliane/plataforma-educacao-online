using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaEducacaoOnline.Core.Messages.CommonMessagens.IntegrationEvent
{
    public class PedidoMatriculaConfirmadoEvent : IntegrationEvent
    {
        public Guid AlunoId { get; private set; }
        public Guid CursoId { get; private set; }
        public Guid MatriculaId { get; private set; }
        public decimal Valor { get; private set; }
        public string NomeCartao { get; private set; }
        public string NumeroCartao { get; private set; }
        public string ExpiracaoCartao { get; private set; }
        public string CvvCartao { get; private set; }

        public PedidoMatriculaConfirmadoEvent(Guid alunoId, Guid cursoId, decimal valor, string nomeCartao, string numeroCartao, string expiracaoCartao, string cvvCartao)
        {
            AlunoId = alunoId;
            CursoId = cursoId;
            Valor = valor;
            NomeCartao = nomeCartao;
            NumeroCartao = numeroCartao;
            ExpiracaoCartao = expiracaoCartao;
            CvvCartao = cvvCartao;
        }
    }
}
