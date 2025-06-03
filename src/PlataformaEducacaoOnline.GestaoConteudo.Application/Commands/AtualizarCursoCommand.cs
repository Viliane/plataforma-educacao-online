using FluentValidation;
using PlataformaEducacaoOnline.Core.Messages;
using PlataformaEducacaoOnline.GestaoConteudo.Domain;

namespace PlataformaEducacaoOnline.GestaoConteudo.Application.Commands
{
    public class AtualizarCursoCommand : Command
    {
        public Guid CursoId { get; private set; }
        public string Nome { get; private set; }
        public ConteudoProgramatico ConteudoProgramatico { get; private set; }
        public decimal Valor { get; private set; }

        public AtualizarCursoCommand(string nome, Guid cursoId, string conteudo, decimal valor)
        {
            Nome = nome;
            CursoId = cursoId;
            ConteudoProgramatico = new ConteudoProgramatico(conteudo);
            Valor = valor;
        }

        public override bool EhValido()
        {
            ValidationResult = new AtualizarCursoCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
    public class AtualizarCursoCommandValidation : AbstractValidator<AtualizarCursoCommand>
    {
        public AtualizarCursoCommandValidation()
        {
            RuleFor(c => c.Nome)
                .NotEmpty().WithMessage("O nome do curso é obrigatório.")
                .Length(2, 100).WithMessage("O nome do curso deve ter entre 2 e 100 caracteres.");
            RuleFor(c => c.CursoId)
                .NotEqual(Guid.Empty).WithMessage("O ID do curso é obrigatório.");
            RuleFor(c => c.ConteudoProgramatico.DescricaoConteudoProgramatico)
                .NotEmpty().WithMessage("A descrição do conteúdo programático é obrigatória.");
        }
    }
}
