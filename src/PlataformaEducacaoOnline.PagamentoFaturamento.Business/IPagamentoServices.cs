using PlataformaEducacaoOnline.Core.DomainObjects.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaEducacaoOnline.PagamentoFaturamento.Business
{
    public interface IPagamentoServices
    {
        Task<Transacao> RealizarPagamentoMatricula(PagamentoMatricula pagamentoMatricula);
    }
}
