using FluentValidation;
using PlataformaEducacaoOnline.Core.Messages;

namespace PlataformaEducacaoOnline.GestaoAluno.Application.Commands
{
    public class AdicionarAlunoCommand : Command
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; }

        public AdicionarAlunoCommand(Guid id, string nome)
        {
            Id = id;
            Nome = nome;
        }

        public override bool EhValido()
        {
            ValidationResult = new AdicionarAlunoCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class AdicionarAlunoCommandValidation : AbstractValidator<AdicionarAlunoCommand>
    {
        public AdicionarAlunoCommandValidation()
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty).WithMessage("O ID do aluno é obrigatório.");
            RuleFor(c => c.Nome)
                .NotEmpty().WithMessage("O nome do aluno é obrigatório.")
                .Length(2, 100).WithMessage("O nome do aluno deve ter entre 2 e 100 caracteres.");            
        }
    }
}
