using PlataformaEducacaoOnline.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaEducacaoOnline.GestaoAluno.Domain
{
    public interface IAlunoRepository : IRepository<Aluno>
    {
        void Adicionar(Aluno aluno);
        void Adicionar(Matricula matricula);
        void AdicionarCertificado(Certificado certificado);
        Task<Aluno?> ObterPorId(Guid id);
        Task<IEnumerable<Aluno>> ObterTodos();
        Task<Matricula?> ObterMatriculaPorId(Guid matriculaId);
        Task<Matricula?> ObterMatriculaPorAlunoIdCursoId(Guid AlunoId, Guid cursoId);
        Task<IEnumerable<Matricula>> ObterMatriculasPorAlunoId(Guid alunoId);
        Task<Certificado?> ObterCertificadoPorId(Guid certificadoId);
        void Atualizar(Matricula matricula);
    }
}
