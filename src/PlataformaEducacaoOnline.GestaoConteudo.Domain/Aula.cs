using PlataformaEducacaoOnline.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaEducacaoOnline.GestaoConteudo.Domain
{
    public class Aula: Entity
    {
        public string Titulo { get; private set; }
        public string ConteudoAula { get; private set; }
        public string Material { get; private set; }
        public Guid CursoId { get; private set; }

        public Aula(string titulo, string conteudoAula, string material)
        {
            Titulo = titulo;
            ConteudoAula = conteudoAula;
            Material = material;

            Validar();
        }

        public void VincularCurso(Guid cursoId)
        {
            CursoId = cursoId;
        }

        public void Validar()
        {

        }
    }
}
