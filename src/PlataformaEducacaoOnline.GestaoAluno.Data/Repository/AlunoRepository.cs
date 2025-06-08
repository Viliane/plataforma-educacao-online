using Microsoft.EntityFrameworkCore;
using PlataformaEducacaoOnline.Core.Data;
using PlataformaEducacaoOnline.GestaoAluno.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaEducacaoOnline.GestaoAluno.Data.Repository
{
    public class AlunoRepository : IAlunoRepository
    {
        private readonly GestaoAlunoContext _context;

        public AlunoRepository(GestaoAlunoContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public void Adicionar(Aluno aluno)
        {
            _context.Alunos.Add(aluno);
        }

        public void Adicionar(Matricula matricula)
        {
            _context.Matriculas.Add(matricula);
        }

        public void AdicionarCertificado(Certificado certificado)
        {
            _context.Certificados.Add(certificado);
        }

        public async Task<Aluno?> ObterPorId(Guid id)
        {
            return await _context.Alunos.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<Aluno>> ObterTodos()
        {
            return await _context.Alunos.AsNoTracking().ToListAsync();
        }

        public async Task<Matricula?> ObterMatriculaPorId(Guid matriculaId)
        {
            return await _context.Matriculas.AsNoTracking().FirstOrDefaultAsync(m => m.Id == matriculaId);
        }

        public async Task<Matricula?> ObterMatriculaPorAlunoIdCursoId(Guid AlunoId, Guid cursoId)
        {
            return await _context.Matriculas.AsNoTracking().FirstOrDefaultAsync(m => m.AlunoId == AlunoId && m.CursoId == cursoId);
        }

        public async Task<IEnumerable<Matricula>> ObterMatriculasPorAlunoId(Guid alunoId)
        {
            return await _context.Matriculas.AsNoTracking().Where(m => m.AlunoId == alunoId).ToListAsync();
        }

        public async Task<Certificado?> ObterCertificadoPorId(Guid certificadoId)
        {
            return await _context.Certificados.AsNoTracking().FirstOrDefaultAsync(c => c.Id == certificadoId);
        }

        public void Atualizar(Matricula matricula)
        {
            _context.Matriculas.Update(matricula);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
