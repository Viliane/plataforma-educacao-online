using FluentValidation;
using PlataformaEducacaoOnline.Core.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaEducacaoOnline.GestaoConteudo.Application.Commands
{
    public class RemoverMaterialAulaCommand : Command
    {
        public Guid MaterialId { get; private set; }

        public RemoverMaterialAulaCommand(Guid materialId)
        {
            MaterialId = materialId;
        }

        public override bool EhValido()
        {
            ValidationResult = new RemoverMaterialAulaCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class RemoverMaterialAulaCommandValidation : AbstractValidator<RemoverMaterialAulaCommand>
    {
        public RemoverMaterialAulaCommandValidation()
        {
            RuleFor(c => c.MaterialId)
                .NotEqual(Guid.Empty).WithMessage("O ID do material é obrigatório.");
        }
    }
}
