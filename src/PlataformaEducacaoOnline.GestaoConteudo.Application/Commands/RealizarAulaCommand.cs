using FluentValidation;
using PlataformaEducacaoOnline.Core.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaEducacaoOnline.GestaoConteudo.Application.Commands
{
    public class RealizarAulaCommand : Command
    {
        public Guid AulaId { get; private set; }
        public Guid UsuarioId { get; private set; }

        public RealizarAulaCommand(Guid aulaId, Guid usuarioId)
        {
            AulaId = aulaId;
            UsuarioId = usuarioId;
        }

        public override bool EhValido()
        {
            ValidationResult = new RealizarAulaCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class RealizarAulaCommandValidation : AbstractValidator<RealizarAulaCommand>
    {
        public RealizarAulaCommandValidation()
        {
            RuleFor(c => c.AulaId)
                .NotEqual(Guid.Empty).WithMessage("O ID da aula é obrigatório.");
            RuleFor(c => c.UsuarioId)
                .NotEqual(Guid.Empty).WithMessage("O ID do usuário é obrigatório.");
        }
    }
}
