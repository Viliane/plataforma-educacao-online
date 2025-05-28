using System;
using PlataformaEducacaoOnline.Core.DomainObjects;

namespace PlataformaEducacaoOnline.GestaoConteudo.Domain
{
    public class Curso : Entity, IAggregateRoot
    {
        public string Nome { get; private set; }
        public ConteudoProgramatico ConteudoProgramatico { get; private set; }

        private readonly List<Aula> _aulas;
        public IReadOnlyCollection<Aula> Aulas => _aulas;

        public Curso(string nome, ConteudoProgramatico conteudoProgramatico)
        {
            Nome = nome;
            ConteudoProgramatico = conteudoProgramatico;
            _aulas = new List<Aula>();

            Validar();
        }

        public void AdicionarAula(Aula aula)
        {
            aula.VincularCurso(Id);
            _aulas.Add(aula);
        }

        public void Validar()
        {
            if (string.IsNullOrEmpty(Nome))
                throw new DomainException("O nome do curso é obrigatório.");
            if (ConteudoProgramatico == null)
                throw new DomainException("O conteúdo programático é obrigatório.");
        }

    }
}
