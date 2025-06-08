using PlataformaEducacaoOnline.GestaoAluno.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaEducacaoOnline.GestaoAluno.Application.Queries.DTO
{
    public class CertificadoQueryDto
    {
        public Guid Id { get; set; }
        public Guid MatriculaId { get; set; }
        public Guid AlunoId { get; set; }
        public DateTime DataEmissao { get; set; }
    }
}
