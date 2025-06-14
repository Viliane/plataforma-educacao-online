using PlataformaEducacaoOnline.GestaoConteudo.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaEducacaoOnline.Api.Tests.DTO
{
    public class AulaTestDto
    {
        public bool sucesso { get; set; }
        public List<Data> data { get; set; }
    }

    public class Data
    {
        public Guid id { get; set; }
        public string titulo { get; set; }
        public string conteudo { get; set; }
        public Guid cursoId { get; set; }
        public List<Material> materiais { get; set; }
    }

    public class Material
    {
        public string nome { get; set; }
        public string aulaId { get; set; }
        public object aula { get; set; }
        public string id { get; set; }
        public object notificacoes { get; set; }
    }
}
