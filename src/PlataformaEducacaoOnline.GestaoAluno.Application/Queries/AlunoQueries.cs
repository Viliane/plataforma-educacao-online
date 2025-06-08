using PlataformaEducacaoOnline.GestaoAluno.Application.Queries.DTO;
using PlataformaEducacaoOnline.GestaoAluno.Domain;

namespace PlataformaEducacaoOnline.GestaoAluno.Application.Queries
{
    public class AlunoQueries : IAlunoQueries
    {
        private readonly IAlunoRepository _alunoRepository;
        private readonly ICertificadoPdfGenerator _certificadoPdfGenerator;

        public AlunoQueries(IAlunoRepository alunoRepository, ICertificadoPdfGenerator certificadoPdfGenerator)
        {
            _alunoRepository = alunoRepository;
            _certificadoPdfGenerator = certificadoPdfGenerator;
        }

        public async Task<byte[]> ObterCertificado(Guid matriculaId, Guid usuarioId)
        {
            var certificado = await _alunoRepository.ObterCertificadoPorId(matriculaId, usuarioId);
            var gerador = certificado is null ? Array.Empty<byte>() : certificado.EmitirCertificado(_certificadoPdfGenerator);
            return gerador;
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
