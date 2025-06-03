using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaEducacaoOnline.GestaoConteudo.Application.Queries.DTO
{
    public class CursoQueryDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Conteudo { get; set; }
        public DateTime DataAtualizacaoConteudo { get; set; }   
        public Guid UsuarioId { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime? DataAtualizacao { get; set; }
        public decimal Valor { get; set; }
    }
}
