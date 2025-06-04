using Microsoft.EntityFrameworkCore;
using PlataformaEducacaoOnline.Core.Data;
using PlataformaEducacaoOnline.GestaoConteudo.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaEducacaoOnline.GestaoConteudo.Data.Repository
{
    public class CursoRepository : ICursoRepositoty
    {
        private readonly GestaoConteudoContext _context;

        public CursoRepository(GestaoConteudoContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<Aula?> ObterAulaPorId(Guid aulaId)
        {
            return await _context.Aulas.AsNoTracking().FirstOrDefaultAsync(a => a.Id == aulaId);
        }

        public async Task<Curso?> ObterCursoPorId(Guid cursoId)
        {
            return await _context.Cursos.AsNoTracking().FirstOrDefaultAsync(a => a.Id == cursoId);
        }

        public async Task<IEnumerable<Aula>> ObterTodasAulas()
        {
            return await _context.Aulas.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Curso>> ObterTodosCursos()
        {
            return await _context.Cursos.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Aula>> ObterAulasPorCursoId(Guid cursoId)
        {
            return await _context.Aulas.AsNoTracking().Where(a => a.CursoId == cursoId).ToListAsync();
        }

        public async Task<IEnumerable<Material>> ObterMateriaisPorAulaId(Guid aulaId)
        {
            return await _context.Materiais.AsNoTracking().Where(a => a.AulaId == aulaId).ToListAsync();
        }

        public void Adicionar(Curso curso)
        {
            _context.Cursos.Add(curso);
        }

        public void Adicionar(Aula aula)
        {
            _context.Aulas.Add(aula);
        }

        public void Atualizar(Curso curso)
        {
            _context.Cursos.Update(curso);
        }

        public void Atualizar(Aula aula)
        {
            _context.Aulas.Update(aula);
        }

        public void Remover(Curso curso)
        {
            _context.Cursos.Remove(curso);
        }

        public void Remover(Aula aula)
        {
            _context.Aulas.Remove(aula);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
