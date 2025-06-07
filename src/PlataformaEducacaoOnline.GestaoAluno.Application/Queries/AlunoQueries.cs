using PlataformaEducacaoOnline.GestaoAluno.Application.Queries.DTO;
using PlataformaEducacaoOnline.GestaoAluno.Domain;

namespace PlataformaEducacaoOnline.GestaoAluno.Application.Queries
{
    public class AlunoQueries : IAlunoQueries
    {
        private readonly IAlunoRepository _alunoRepository;

        public AlunoQueries(IAlunoRepository alunoRepository)
        {
            _alunoRepository = alunoRepository;
        }

        public async Task<MatriculaQueryDto?> ObterMatriculaAlunoIdCursoId(Guid usuarioId, Guid cursoId)
        {
            var matricula = await _alunoRepository.ObterMatriculaPorAlunoIdCursoId(usuarioId, cursoId);

            return matricula is null ? null :
                new MatriculaQueryDto
                {
                    Id = matricula.Id,
                    AlunoId = matricula.AlunoId,
                    CursoId = matricula.CursoId,
                    DataMatricula = matricula.DataMatricula,
                    Status = (int)matricula.StatusMatricula
                };
        }
    }
}
