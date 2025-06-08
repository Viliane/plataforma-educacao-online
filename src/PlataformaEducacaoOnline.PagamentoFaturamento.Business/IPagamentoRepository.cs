using PlataformaEducacaoOnline.Core.Data;

namespace PlataformaEducacaoOnline.PagamentoFaturamento.Business
{
    public interface IPagamentoRepository : IRepository<Pagamento>
    {
        void Adicionar(Pagamento pagamento);
        void Adicionar(Transacao transacao);
        Task<Transacao?> ObterTransacaoPorAlunoIdCursoIdMatricula(Guid alunoId, Guid cursoId, Guid Matricula);
    }
}
