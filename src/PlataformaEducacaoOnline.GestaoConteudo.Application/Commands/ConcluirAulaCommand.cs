using FluentValidation;
using PlataformaEducacaoOnline.Core.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaEducacaoOnline.GestaoConteudo.Application.Commands
{
    public class ConcluirAulaCommand : Command
    {
        public Guid AulaId { get; private set; }
        public Guid UsuarioId { get; private set; }

        public ConcluirAulaCommand(Guid aulaId, Guid usuarioId)
        {
            AulaId = aulaId;
            UsuarioId = usuarioId;
        }

        public override bool EhValido()
        {
            ValidationResult = new ConcluirAulaCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class ConcluirAulaCommandValidation : AbstractValidator<ConcluirAulaCommand>
    {
        public ConcluirAulaCommandValidation()
        {
            RuleFor(c => c.AulaId)
                .NotEqual(Guid.Empty).WithMessage("O ID da aula é obrigatório.");
            RuleFor(c => c.UsuarioId)
                .NotEqual(Guid.Empty).WithMessage("O ID do usuário é obrigatório.");
        }
    }
}
