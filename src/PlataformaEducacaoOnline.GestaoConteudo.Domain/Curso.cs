using System;
using PlataformaEducacaoOnline.Core.DomainObjects;

namespace PlataformaEducacaoOnline.GestaoConteudo.Domain
{
    public class Curso : Entity, IAggregateRoot
    {

        public string Nome { get; private set; }

        public ConteudoProgramatico ConteudoProgramatico { get; private set; }

        public Guid UsuarioId { get; private set; }

        public ICollection<Aula> Aulas { get; set; }

        public Curso(string nome, ConteudoProgramatico conteudoProgramatico, Guid usuarioId)
        {
            Nome = nome;
            ConteudoProgramatico = conteudoProgramatico;
            UsuarioId = usuarioId;
            Aulas = new List<Aula>();

            Validar();
        }

        public void AdicionarAula(Aula aula)
        {
            aula.VincularCurso(Id);
            Aulas.Add(aula);
        }

        public void Validar() { }

    }
}
