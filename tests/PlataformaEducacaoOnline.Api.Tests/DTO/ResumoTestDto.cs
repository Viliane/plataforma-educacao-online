using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaEducacaoOnline.Api.Tests.DTO
{
    public class ResumoTestDto
    {
        public bool sucesso { get; set; }
        public MatriculaResumoDto data { get; set; }
    }

    public class MatriculaResumoDto
    {
        public string matricula { get; set; }
        public string nomeCurso { get; set; }
        public string statusMatricula { get; set; }
    }
}
