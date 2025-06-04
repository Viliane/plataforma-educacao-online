using FluentValidation;
using PlataformaEducacaoOnline.Core.Messages;

namespace PlataformaEducacaoOnline.GestaoConteudo.Application.Commands
{
    public class RemoverAulaCommand : Command
    {
        public Guid AulaId { get; private set; }

        public RemoverAulaCommand(Guid aulaId)
        {
            AulaId = aulaId;
        }

        public override bool EhValido()
        {
            ValidationResult = new RemoverAulaCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class RemoverAulaCommandValidation : AbstractValidator<RemoverAulaCommand>
    {
        public RemoverAulaCommandValidation()
        {
            RuleFor(c => c.AulaId)
                .NotEqual(Guid.Empty).WithMessage("O ID do aula é obrigatório.");
        }
    }
}
