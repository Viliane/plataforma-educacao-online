using PlataformaEducacaoOnline.Core.Bus;
using PlataformaEducacaoOnline.Core.DomainObjects;
using PlataformaEducacaoOnline.Core.DomainObjects.DTO;
using PlataformaEducacaoOnline.Core.Messages.CommonMessagens.IntegrationEvent;

namespace PlataformaEducacaoOnline.PagamentoFaturamento.Business
{
    public class PagamentoService : IPagamentoServices
    {
        private readonly IPagamentoCartaoCreditoFacade _pagamentoCartaoCreditoFacade;
        private readonly IPagamentoRepository _pagamentoRepository;
        private readonly IMediatrHandler _mediatorHandler;

        public PagamentoService(IPagamentoCartaoCreditoFacade pagamentoCartaoCreditoFacade, 
            IPagamentoRepository pagamentoRepository,
            IMediatrHandler mediatorHandler)
        {
            _pagamentoCartaoCreditoFacade = pagamentoCartaoCreditoFacade;
            _pagamentoRepository = pagamentoRepository;
            _mediatorHandler = mediatorHandler;
        }

        public async Task<Transacao> RealizarPagamentoMatricula(PagamentoMatricula pagamentoMatricula)
        {
            var pedido = new Pedido
            {
                AlunoId = pagamentoMatricula.AlunoId,
                CursoId = pagamentoMatricula.CursoId,
                MatriculaId = pagamentoMatricula.MatriculaId,
                Valor = pagamentoMatricula.Valor,
            };

            var pagamento = new Pagamento
            {
                DadosCartao  = new DadosCartao
                {   
                    NomeCartao = pagamentoMatricula.NomeCartao,
                    NumeroCartao = pagamentoMatricula.NumeroCartao,
                    ExpiracaoCartao = pagamentoMatricula.ExpiracaoCartao,
                    CodigoSegurancaCartao = pagamentoMatricula.CvvCartao                    
                },

                MatriculaId = pagamentoMatricula.MatriculaId,
                CursoId = pagamentoMatricula.CursoId,
                AlunoId = pagamentoMatricula.AlunoId,
                Valor = pagamentoMatricula.Valor
            };

            var transacao = _pagamentoCartaoCreditoFacade.RealizarPagamento(pedido, pagamento);

            if (transacao.StatusTransacao == StatusTransacao.Pago)
            {
                pagamento.AdicionarEvento(new PagamentoRealizadoEvent(
                    transacao.PagamentoId,
                    transacao.MatriculaId,
                    transacao.AlunoId,
                    transacao.CursoId));

                 _pagamentoRepository.Adicionar(pagamento);
                 _pagamentoRepository.Adicionar(transacao);

                await _pagamentoRepository.UnitOfWork.Commit();
                return transacao;
            }

            await _mediatorHandler.PublicarNotificacao(new DomainNotification("pagamento", "Pagamento recusado"));
            await _mediatorHandler.PublicarEvento(new PagamentoRecusadoEvent(
                transacao.PagamentoId,
                transacao.MatriculaId,
                transacao.AlunoId,
                transacao.CursoId));

            return transacao;
        }
    }
}
