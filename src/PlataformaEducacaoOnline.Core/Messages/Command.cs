﻿using System;
using FluentValidation.Results;
using MediatR;

namespace PlataformaEducacaoOnline.Core.Messages
{
    public abstract class Command : Message, IRequest<bool>
    {
        public DateTime Timestamp { get; private set; }

        public ValidationResult ValidationResult { get; set; } = new ValidationResult();

        protected Command()
        {
            Timestamp = DateTime.Now;
        }

        public abstract bool EhValido(); 
    }
}
