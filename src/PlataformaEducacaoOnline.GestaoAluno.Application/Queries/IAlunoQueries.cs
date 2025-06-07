using PlataformaEducacaoOnline.GestaoAluno.Application.Queries.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaEducacaoOnline.GestaoAluno.Application.Queries
{
    public interface IAlunoQueries
    {
        Task<MatriculaQueryDto?> ObterMatriculaAlunoIdCursoId(Guid usuarioId, Guid cursoId);
    }
}
