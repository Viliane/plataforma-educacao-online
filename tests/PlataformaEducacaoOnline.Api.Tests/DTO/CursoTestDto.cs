using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaEducacaoOnline.Api.Tests.DTO
{
    public class CursoTestDto
    {
        public bool sucesso { get; set; }
        public List<CursoTestIntegration> data { get; set; }
    }

    public class CursoTestIntegration
    {
        public Guid id { get; set; }
        public string nome { get; set; }
        public string conteudo { get; set; }
        public DateTime dataAtualizacaoConteudo { get; set; }
        public Guid usuarioId { get; set; }
        public DateTime dataCadastro { get; set; }
        public DateTime? dataAtualizacao { get; set; }
        public decimal valor { get; set; }
    }
}
