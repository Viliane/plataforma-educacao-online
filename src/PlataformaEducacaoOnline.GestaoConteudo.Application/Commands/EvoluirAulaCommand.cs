using FluentValidation;
using PlataformaEducacaoOnline.Core.Messages;
using PlataformaEducacaoOnline.GestaoConteudo.Application.Queries.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaEducacaoOnline.GestaoConteudo.Application.Commands
{
    public class EvoluirAulaCommand : Command
    {
        public Guid UsuarioId { get; private set; }
        public Guid CursoId { get; private set; }
        public IEnumerable<AulaQueryDto> AulaCurso { get; private set; }

        public EvoluirAulaCommand(Guid usuarioId, Guid cursoId, IEnumerable<AulaQueryDto> aulaCurso)
        {
            UsuarioId = usuarioId;
            CursoId = cursoId;
            AulaCurso = aulaCurso;
        }

        public override bool EhValido()
        {
            ValidationResult = new EvoluirAulaCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class EvoluirAulaCommandValidation : AbstractValidator<EvoluirAulaCommand>
    {
        public EvoluirAulaCommandValidation()
        {
            RuleFor(RuleFor => RuleFor.UsuarioId)
                .NotEqual(Guid.Empty).WithMessage("O ID do usuário é obrigatório.");
            RuleFor(RuleFor => RuleFor.CursoId)
                .NotEqual(Guid.Empty).WithMessage("O ID do curso é obrigatório.");
        }
    }
}
