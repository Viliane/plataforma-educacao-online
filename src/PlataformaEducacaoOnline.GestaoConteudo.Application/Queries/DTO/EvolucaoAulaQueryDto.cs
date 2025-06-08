using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaEducacaoOnline.GestaoConteudo.Application.Queries.DTO
{
    public class EvolucaoAulaQueryDto
    {
        public Guid Id { get; set; }
        public Guid AulaId { get; set; }
        public Guid UsuarioId { get; set; }
        public Guid CursoId { get; set; }
        public int Status { get; set; } 
    }
}
