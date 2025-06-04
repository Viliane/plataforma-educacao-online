using FluentValidation;
using PlataformaEducacaoOnline.Core.Messages;

namespace PlataformaEducacaoOnline.GestaoConteudo.Application.Commands
{
    public class RemoverCursoCommand : Command
    {
        public Guid CursoId { get; private set; }

        public RemoverCursoCommand(Guid cursoId)
        {
            CursoId = cursoId;
        }

        public override bool EhValido()
        {
            ValidationResult = new RemoverCursoCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class RemoverCursoCommandValidation : AbstractValidator<RemoverCursoCommand>
    {
        public RemoverCursoCommandValidation()
        {
            RuleFor(c => c.CursoId)
                .NotEqual(Guid.Empty).WithMessage("O ID do curso é obrigatório.");
        }
    }
}
