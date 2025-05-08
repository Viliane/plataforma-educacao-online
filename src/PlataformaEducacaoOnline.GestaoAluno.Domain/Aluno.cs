using PlataformaEducacaoOnline.Core.DomainObjects;

namespace PlataformaEducacaoOnline.GestaoAluno.Domain
{
    public class Aluno : Entity, IAggregateRoot
    {
        public string Nome { get; private set; }
        public Matricula matricula { get; set; }

        public Aluno(string nome)
        {
            Nome = nome;
        }

        public void CriarMatricula(Matricula matricula)
        {
            matricula.PendentePagamento();
        }


    }
}
