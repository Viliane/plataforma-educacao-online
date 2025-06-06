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

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
