using FluentValidation;
using PlataformaEducacaoOnline.Core.Messages;
using PlataformaEducacaoOnline.GestaoConteudo.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaEducacaoOnline.GestaoConteudo.Application.Commands
{
    public class AdicionarAulaCommand : Command
    {
        public string Titulo { get; private set; }
        public string Conteudo { get; private set; }
        public Guid CursoId { get; private set; }
        public List<Material> Materiais { get; private set; } = new List<Material>();

        public AdicionarAulaCommand(string titulo, string conteudo, Guid cursoId, List<Material> materiais)
        {
            Titulo = titulo;
            Conteudo = conteudo;
            CursoId = cursoId;
            Materiais = materiais;
        }
        public override bool EhValido()
        {
            ValidationResult = new AdicionarAulaCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class AdicionarAulaCommandValidation : AbstractValidator<AdicionarAulaCommand>
    {
        public AdicionarAulaCommandValidation()
        {
            RuleFor(c => c.Titulo)
                .NotEmpty().WithMessage("O título da aula é obrigatório.")
                .Length(2, 100).WithMessage("O título da aula deve ter entre 2 e 100 caracteres.");
            RuleFor(c => c.Conteudo)
                .NotEmpty().WithMessage("O conteúdo da aula é obrigatório.");
            RuleFor(c => c.CursoId)
                .NotEqual(Guid.Empty).WithMessage("O ID do curso é obrigatório.");
            //RuleFor(c => c.Materiais)
            //    .NotEmpty().WithMessage("Pelo menos um material deve ser adicionado à aula.");
        }
    }
}
