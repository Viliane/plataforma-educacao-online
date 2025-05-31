using PlataformaEducacaoOnline.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaEducacaoOnline.GestaoConteudo.Domain
{
    public class Aula : Entity
    {
        public Guid CursoId { get; private set; }
        public string Titulo { get; private set; }
        public string Conteudo { get; private set; }

        private readonly List<Material> _materiais;
        public IReadOnlyCollection<Material> Materiais => _materiais;

        //EF
        public Curso Curso { get; private set; } = null!;

        public Aula(string titulo, string conteudo, Guid cursoId)
        {
            Titulo = titulo;
            Conteudo = conteudo;
            CursoId = cursoId;
            _materiais = new List<Material>();

            Validar();
        }

        //public void VincularCurso(Guid cursoId)
        //{
        //    CursoId = cursoId;
        //}

        public void AdicionarMaterial(Material material)
        {
            if (MaterialExistente(material))
                throw new DomainException("Material já está vinculada a esta aula.");

            material.VincularMaterial(Id);
            _materiais.Add(material);
        }

        private bool MaterialExistente(Material material)
        {
            return _materiais.Any(m => m.Id == material.Id);
        }

        public void Validar()
        {
            if (string.IsNullOrEmpty(Titulo))
                throw new DomainException("O título da aula é obrigatório.");
            if (string.IsNullOrEmpty(Conteudo))
                throw new DomainException("O conteúdo da aula é obrigatório.");
            if (CursoId == Guid.Empty)
                throw new DomainException("O ID do curso é obrigatório.");
        }
    }
}
