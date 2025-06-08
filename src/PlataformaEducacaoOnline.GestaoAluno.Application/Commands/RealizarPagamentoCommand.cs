using FluentValidation;
using PlataformaEducacaoOnline.Core.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaEducacaoOnline.GestaoAluno.Application.Commands
{
    public class RealizarPagamentoCommand : Command
    {
        public Guid AlunoId { get; private set; }
        public Guid CursoId { get; private set; }
        public Guid MatriculaId { get; private set; }
        public decimal Valor { get; private set; }
        public string NomeCartao { get; private set; }
        public string NumeroCartao { get; private set; }
        public string ExpiracaoCartao { get; private set; }
        public string CvvCartao { get; private set; }

        public RealizarPagamentoCommand(Guid alunoId, Guid cursoId, Guid matriculaId, decimal valor,
                                        string nomeCartao, string numeroCartao, string expiracaoCartao,
                                        string cvvCartao)
        {
            AlunoId = alunoId;
            CursoId = cursoId;
            MatriculaId = matriculaId;
            Valor = valor;
            NomeCartao = nomeCartao;
            NumeroCartao = numeroCartao;
            ExpiracaoCartao = expiracaoCartao;
            CvvCartao = cvvCartao;
        }

        public override bool EhValido()
        {
            ValidationResult = new RealizarPagamentoCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class RealizarPagamentoCommandValidation : AbstractValidator<RealizarPagamentoCommand>
    {
        public RealizarPagamentoCommandValidation()
        {
            RuleFor(c => c.AlunoId)
                .NotEqual(Guid.Empty).WithMessage("O ID do aluno é obrigatório.");
            RuleFor(c => c.CursoId)
                .NotEqual(Guid.Empty).WithMessage("O ID do curso é obrigatório.");
            RuleFor(c => c.MatriculaId)
                .NotEqual(Guid.Empty).WithMessage("O ID da matrícula é obrigatório.");
            RuleFor(c => c.Valor)
                .GreaterThan(0).WithMessage("O valor do pagamento deve ser maior que zero.");
            RuleFor(c => c.NomeCartao)
                .NotEmpty().WithMessage("O nome no cartão é obrigatório.")
                .Length(2, 100).WithMessage("O nome no cartão deve ter entre 2 e 100 caracteres.");
            RuleFor(c => c.NumeroCartao)
                .NotEmpty().WithMessage("O número do cartão é obrigatório.")
                .Length(16).WithMessage("O número do cartão deve ter 16 dígitos.");
            RuleFor(c => c.ExpiracaoCartao)
                .NotEmpty().WithMessage("A data de expiração do cartão é obrigatória.")
                .Matches(@"^(0[1-9]|1[0-2])\/?([0-9]{2})$").WithMessage("A data de expiração deve estar no formato MM/AA.");
            RuleFor(c => c.CvvCartao)
                .NotEmpty().WithMessage("O CVV do cartão é obrigatório.")
                .Length(3).WithMessage("O CVV deve ter 3 dígitos.");
        }
    }
}
