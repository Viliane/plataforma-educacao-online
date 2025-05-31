using PlataformaEducacaoOnline.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaEducacaoOnline.GestaoConteudo.Domain
{
    public class Material : Entity
    {
        public string Nome { get; private set; }
        public Guid AulaId { get; private set; }

        // EF
        public Aula Aula { get; private set; } = null!;

        public Material(string nome)
        {
            Nome = nome;

            Validar();
        }

        public void VincularMaterial(Guid aulaId)
        {
            AulaId = aulaId;
        }

        public void Validar()
        {
            if (string.IsNullOrEmpty(Nome))
                throw new DomainException("O nome do material é obrigatório.");            
        }
    }
}
