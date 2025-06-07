using PlataformaEducacaoOnline.Core.DomainObjects;

namespace PlataformaEducacaoOnline.GestaoAluno.Domain
{
    public class Aluno : Entity, IAggregateRoot
    {
        public string Nome { get; private set; }

        public HistoricoAprendizado HistoricoAprendizado { get; private set; }

        private readonly List<Matricula> _matriculas;

        public IReadOnlyCollection<Matricula> Matriculas => _matriculas;

        //EF
        protected Aluno() { }
        public Certificado Certificado { get; private set; } = null!;

        public Aluno(string nome, Guid cursoId, HistoricoAprendizado historicoAprendizado)
        {
            Nome = nome;
            HistoricoAprendizado = historicoAprendizado;
            _matriculas = new List<Matricula>();

            Validar();
        }

        public Aluno(Guid id, string nome)
        {
            Id = id;
            Nome = nome;

            Validar();
        }

        public void SolicitarCertificado()
        {

        }

        public void RegistrarHistorico()
        {

        }

        public void Validar()
        {
            if(Guid.Empty == Id)
                throw new DomainException("Id não pode ser vazio.");
            if (string.IsNullOrWhiteSpace(Nome))
                throw new DomainException("Nome não pode ser vazio.");
        }
    }
}
