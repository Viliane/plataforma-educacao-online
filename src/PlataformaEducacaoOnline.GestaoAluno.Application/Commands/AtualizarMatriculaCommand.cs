using FluentValidation;
using PlataformaEducacaoOnline.Core.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaEducacaoOnline.GestaoAluno.Application.Commands
{
    public class AtualizarMatriculaCommand : Command
    {
        public Guid MatriculaId { get; private set; }
        public AtualizarMatriculaCommand(Guid matriculaId)
        {
            MatriculaId = matriculaId;
        }
        public override bool EhValido()
        {
            ValidationResult = new AtualizarMatriculaCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class AtualizarMatriculaCommandValidation : AbstractValidator<AtualizarMatriculaCommand>
    {
        public AtualizarMatriculaCommandValidation()
        {
            RuleFor(c => c.MatriculaId)
                .NotEqual(Guid.Empty).WithMessage("O ID da matrícula é obrigatório.");
        }
    }
}
