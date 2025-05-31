using System;
using PlataformaEducacaoOnline.Core.DomainObjects;

namespace PlataformaEducacaoOnline.GestaoConteudo.Domain
{
    public class Curso : Entity, IAggregateRoot
    {
        public string Nome { get; private set; }
        public ConteudoProgramatico ConteudoProgramatico { get; private set; }

        public Guid UsuarioId { get; private set; }

        public DateTime DataCadastro { get; private set; }

        private readonly List<Aula> _aulas;
        public IReadOnlyCollection<Aula> Aulas => _aulas;

        public Curso(string nome, Guid usuarioId, ConteudoProgramatico conteudoProgramatico)
        {
            Nome = nome;
            ConteudoProgramatico = conteudoProgramatico;
            UsuarioId = usuarioId;
            _aulas = new List<Aula>();
            DataCadastro = DateTime.UtcNow;

            Validar();
        }

        //public void AdicionarAula(Aula aula)
        //{
        //    if (aula is null) 
        //        throw new DomainException("A aula não pode ser nula.");

        //    if (ExisteAula(aula))
        //        throw new DomainException("A aula já está vinculada a este curso.");

        //    aula.VincularCurso(Id);
        //    _aulas.Add(aula);
        //}

        //private bool ExisteAula(Aula aula)
        //{
        //    return _aulas.Any(a => a.Id == aula.Id);
        //}

        private void Validar()
        {
            if (string.IsNullOrEmpty(Nome))
                throw new DomainException("O nome do curso é obrigatório.");
            if (ConteudoProgramatico == null)
                throw new DomainException("O conteúdo programático é obrigatório.");
            if (UsuarioId == Guid.Empty)
                throw new DomainException("O ID do usuário é obrigatório.");
        }
    }
}
