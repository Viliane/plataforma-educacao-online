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

        public DateTime? DataAtualizacao { get; private set; }

        public decimal Valor { get; private set; }

        private readonly List<Aula> _aulas;
        public IReadOnlyCollection<Aula> Aulas => _aulas;

        protected Curso() { }

        public Curso(string nome, Guid usuarioId, ConteudoProgramatico conteudoProgramatico, decimal valor)
        {
            Nome = nome;
            ConteudoProgramatico = conteudoProgramatico;
            UsuarioId = usuarioId;
            Valor = valor;
            _aulas = new List<Aula>();
            DataCadastro = DateTime.UtcNow;

            Validar();
        }

        public void AtualizarNome(string nome)
        {
            if (string.IsNullOrEmpty(nome))
                throw new DomainException("O nome do curso é obrigatório.");
            Nome = nome;
        }

        public void AtualizarConteudoProgramatico(ConteudoProgramatico conteudoProgramatico)
        {
            if (conteudoProgramatico == null)
                throw new DomainException("O conteúdo programático é obrigatório.");
            ConteudoProgramatico = conteudoProgramatico;
        }

        public void AtualizarDataAtualizacao()
        {
            DataAtualizacao = DateTime.UtcNow;
        }

        public void AtualizarValor(decimal valor)
        {
            if (valor <= 0)
                throw new DomainException("O valor do curso deve ser maior que zero.");
            Valor = valor;
        }        

        private void Validar()
        {
            if (string.IsNullOrEmpty(Nome))
                throw new DomainException("O nome do curso é obrigatório.");
            if (ConteudoProgramatico == null)
                throw new DomainException("O conteúdo programático é obrigatório.");
            if (UsuarioId == Guid.Empty)
                throw new DomainException("O ID do usuário é obrigatório.");
            if (Valor <= 0)
                throw new DomainException("O valor do curso deve ser maior que zero.");
        }
    }
}
