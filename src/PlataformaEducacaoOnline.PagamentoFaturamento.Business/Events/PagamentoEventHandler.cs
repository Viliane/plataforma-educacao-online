using MediatR;
using PlataformaEducacaoOnline.Core.DomainObjects.DTO;
using PlataformaEducacaoOnline.Core.Messages.CommonMessagens.IntegrationEvent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaEducacaoOnline.PagamentoFaturamento.Business.Events
{
    public class PagamentoEventHandler : INotificationHandler<PedidoMatriculaConfirmadoEvent>
    {
        private readonly IPagamentoServices _pagamentoServices;

        public PagamentoEventHandler(IPagamentoServices pagamentoServices)
        {
            _pagamentoServices = pagamentoServices;
        }

        public async Task Handle(PedidoMatriculaConfirmadoEvent message, CancellationToken cancellationToken)
        {
            var pagamentoMatricula = new PagamentoMatricula
            {
                AlunoId = message.AlunoId,
                CursoId = message.CursoId,
                Valor = message.Valor,
                NomeCartao = message.NomeCartao,
                NumeroCartao = message.NumeroCartao,
                ExpiracaoCartao = message.ExpiracaoCartao,
                CvvCartao = message.CvvCartao
            };

            await _pagamentoServices.RealizarPagamentoMatricula(pagamentoMatricula);
        }
    }
}
