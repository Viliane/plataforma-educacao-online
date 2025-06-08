using PlataformaEducacaoOnline.Core.DomainObjects;
using PlataformaEducacaoOnline.Core.DomainObjects.DTO;

namespace PlataformaEducacaoOnline.GestaoConteudo.Domain
{
    public class EvolucaoAula : Entity
    {
        public Guid AulaId { get; private set; }
        public Guid UsuarioId { get; private set; }
        public StatusAula Status { get; private set; }

        //EF
        protected EvolucaoAula() { }

        public EvolucaoAula(Guid aulaId, Guid usuarioId)
        {
            AulaId = aulaId;
            UsuarioId = usuarioId;
            Status = StatusAula.NaoIniciada;
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
        }
    }
}
