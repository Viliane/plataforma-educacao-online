using PlataformaEducacaoOnline.PagamentoFaturamento.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaEducacaoOnline.PagamentoFaturamento.AntiCorruption
{
    public class PagamentoCartaoCreditoFacade : IPagamentoCartaoCreditoFacade
    {
        private readonly IPayPalGateway _payPalGateway;
        private readonly IConfigurationManager _configurationManager;

        public PagamentoCartaoCreditoFacade(IPayPalGateway payPalGateway, IConfigurationManager configurationManager)
        {
            _payPalGateway = payPalGateway;
            _configurationManager = configurationManager;
        }

        public Transacao RealizarPagamento(Pedido pedido, Pagamento pagamento)
        {
            var apiKey = _configurationManager.GetValue("apiKey");
            var encriptionKey = _configurationManager.GetValue("encriptionKey");

            var serviceKey = _payPalGateway.GetPayPalServiceKey(apiKey, encriptionKey);
            var cardHashKey = _payPalGateway.GetCardHashKey(serviceKey, pagamento.DadosCartao.NumeroCartao);

            var pagamentoResult = _payPalGateway.CommitTransaction(cardHashKey, pedido.CursoId.ToString(), pagamento.Valor);

            var transacao = new Transacao
            {   
                PagamentoId = pagamento.Id,
                AlunoId = pedido.AlunoId,
                CursoId = pedido.CursoId,
                MatriculaId = pedido.MatriculaId,
                Valor = pagamento.Valor
            };

            if (pagamentoResult)
            {
                transacao.StatusTransacao = StatusTransacao.Pago;
                return transacao;
            }

            transacao.StatusTransacao = StatusTransacao.Recusado;
            return transacao;
        }
    }
}