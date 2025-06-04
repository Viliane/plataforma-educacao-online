using FluentValidation;
using PlataformaEducacaoOnline.Core.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaEducacaoOnline.GestaoConteudo.Application.Commands
{
    public class AtualizarAulaCommand : Command
    {
        public Guid AulaId { get; private set; }
        public string Titulo { get; private set; }
        public string Conteudo { get; private set; }

        public AtualizarAulaCommand(Guid aulaId, string titulo, string conteudo)
        {
            AulaId = aulaId;
            Titulo = titulo;
            Conteudo = conteudo;
        }

        public override bool EhValido()
        {
            ValidationResult = new AtualizarAulaCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class AtualizarAulaCommandValidation : AbstractValidator<AtualizarAulaCommand>
    {
        public AtualizarAulaCommandValidation()
        {
            RuleFor(c => c.Titulo)
                .NotEmpty().WithMessage("O título da aula é obrigatório.")
                .Length(2, 100).WithMessage("O título da aula deve ter entre 2 e 100 caracteres.");
            RuleFor(c => c.Conteudo)
                .NotEmpty().WithMessage("O conteúdo da aula é obrigatório.");            
        }
    }
}
