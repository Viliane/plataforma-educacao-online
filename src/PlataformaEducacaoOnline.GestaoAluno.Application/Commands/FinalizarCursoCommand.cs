﻿using FluentValidation;
using PlataformaEducacaoOnline.Core.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaEducacaoOnline.GestaoAluno.Application.Commands
{
    public class FinalizarCursoCommand : Command
    {
        public Guid AlunoId { get; private set; }
        public Guid CursoId { get; private set; }
        public FinalizarCursoCommand(Guid alunoId, Guid cursoId)
        {
            AlunoId = alunoId;
            CursoId = cursoId;
        }
        public override bool EhValido()
        {
            ValidationResult = new FinalizarCursoCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class FinalizarCursoCommandValidation : AbstractValidator<FinalizarCursoCommand>
    {
        public FinalizarCursoCommandValidation()
        {
            RuleFor(c => c.AlunoId)
                .NotEqual(Guid.Empty).WithMessage("O ID do aluno é obrigatório.");
            RuleFor(c => c.CursoId)
                .NotEqual(Guid.Empty).WithMessage("O ID do curso é obrigatório.");
        }
    }
}
