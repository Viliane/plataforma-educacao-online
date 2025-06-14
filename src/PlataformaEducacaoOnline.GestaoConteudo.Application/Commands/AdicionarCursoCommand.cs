﻿using PlataformaEducacaoOnline.GestaoConteudo.Domain;
using FluentValidation;
using PlataformaEducacaoOnline.Core.Messages;

namespace PlataformaEducacaoOnline.GestaoConteudo.Application.Commands
{
    public class AdicionarCursoCommand : Command
    {
        public string Nome { get; private set; }
        public ConteudoProgramatico ConteudoProgramatico { get; private set; }
        public Guid UsuarioId { get; private set; }

        public decimal Valor { get; private set; }

        public AdicionarCursoCommand(string nome, Guid usuarioId, string conteudo, decimal valor)
        {
            Nome = nome;
            UsuarioId = usuarioId;
            ConteudoProgramatico = new ConteudoProgramatico(conteudo);
            Valor = valor;
        }

        public override bool EhValido()
        {
            ValidationResult = new AdicionarCursoCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class AdicionarCursoCommandValidation : AbstractValidator<AdicionarCursoCommand>
    {
        public AdicionarCursoCommandValidation()
        {
            RuleFor(c => c.Nome)
                .NotEmpty().WithMessage("O nome do curso é obrigatório.")
                .Length(2, 100).WithMessage("O nome do curso deve ter entre 2 e 100 caracteres.");
            RuleFor(c => c.UsuarioId)
                .NotEqual(Guid.Empty).WithMessage("O ID do usuário é obrigatório.");
            RuleFor(c => c.ConteudoProgramatico.DescricaoConteudoProgramatico)
                .NotEmpty().WithMessage("A descrição do conteúdo programático é obrigatória.");
            RuleFor(c => c.Valor).GreaterThan(0)
                .WithMessage("O valor do curso deve ser maior que zero.");
        }
    }
}
