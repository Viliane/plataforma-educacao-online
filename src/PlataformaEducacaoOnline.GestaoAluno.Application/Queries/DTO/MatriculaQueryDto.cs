using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaEducacaoOnline.GestaoAluno.Application.Queries.DTO
{
    public class MatriculaQueryDto
    {
        public Guid Id { get; set; }
        public Guid AlunoId { get; set; }
        public Guid CursoId { get; set; }
        public DateTime DataMatricula { get; set; }
        public int Status { get; set; } 
    }
}
