using PlataformaEducacaoOnline.Core.DomainObjects;
using PlataformaEducacaoOnline.Core.DomainObjects.DTO;

namespace PlataformaEducacaoOnline.GestaoConteudo.Domain
{
    public class EvolucaoAula : Entity
    {
        public Guid AulaId { get; private set; }
        public Guid UsuarioId { get; private set; }
        public Guid CursoId { get; private set; }

        public StatusAula Status { get; private set; }

        //EF
        protected EvolucaoAula() { }

        public EvolucaoAula(Guid aulaId, Guid usuarioId, Guid cursoId)
        {
            AulaId = aulaId;
            UsuarioId = usuarioId;
            Status = StatusAula.NaoIniciada;
            CursoId = cursoId;

            Validar();
        }

        public void AulaEmAndamento()
        {
            Status = StatusAula.EmAndamento;
        }

        public void AulaConcluida()
        {   
            Status = StatusAula.Concluida;
        }

        public void Validar()
        {
            if (AulaId == Guid.Empty)
                throw new DomainException("O ID da aula não pode ser vazio.");
            if (UsuarioId == Guid.Empty)
                throw new DomainException("O ID do usuário não pode ser vazio.");
            if (CursoId == Guid.Empty)
                throw new DomainException("O ID do curso não pode ser vazio.");
        }
    }
}
