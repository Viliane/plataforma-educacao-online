using Microsoft.EntityFrameworkCore;
using PlataformaEducacaoOnline.Core.Data;
using PlataformaEducacaoOnline.PagamentoFaturamento.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaEducacaoOnline.PagamentoFaturamento.Data.Repository
{
    public class PagamentoRepository : IPagamentoRepository
    {
        private readonly PagamentoContext _context;

        public PagamentoRepository(PagamentoContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public void Adicionar(Pagamento pagamento)
        {
            _context.Pagamentos.Add(pagamento);
        }

        public void Adicionar(Transacao transacao)
        {
            _context.Transacoes.Add(transacao);
        }

        public async Task<Transacao?> ObterTransacaoPorAlunoIdCursoIdMatricula(Guid alunoId, Guid cursoId, Guid Matricula)
        {
            return await _context.Transacoes.AsNoTracking()
                .FirstOrDefaultAsync(t => t.AlunoId == alunoId && t.CursoId == cursoId && t.MatriculaId == Matricula);
        }

        public async Task<Pagamento?> ObterPagamentoPorAlunoIdCursoIdMatriculaId(Guid alunoId, Guid cursoId, Guid matriculaId)
        {
            return await _context.Pagamentos.AsNoTracking()
                .FirstOrDefaultAsync(p => p.AlunoId == alunoId && p.CursoId == cursoId && p.MatriculaId == matriculaId);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
