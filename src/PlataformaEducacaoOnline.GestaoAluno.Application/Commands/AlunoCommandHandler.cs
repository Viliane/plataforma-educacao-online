using MediatR;
using PlataformaEducacaoOnline.Core.DomainObjects;
using PlataformaEducacaoOnline.Core.Messages;
using PlataformaEducacaoOnline.GestaoAluno.Application.Events;
using PlataformaEducacaoOnline.GestaoAluno.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaEducacaoOnline.GestaoAluno.Application.Commands
{
    public class AlunoCommandHandler : IRequestHandler<AdicionarAlunoCommand, bool>
    {
        private readonly IAlunoRepository _alunoRepository;
        private readonly IMediator _mediator;

        public AlunoCommandHandler(IAlunoRepository alunoRepository, IMediator mediator)
        {
            _alunoRepository = alunoRepository;
            _mediator = mediator;
        }

        public async Task<bool> Handle(AdicionarAlunoCommand message, CancellationToken cancellationToken)
        {
            if (!ValidarComando(message))
                return false;

            var aluno = new Aluno(message.Id, message.Nome);
            _alunoRepository.Adicionar(aluno);

            aluno.AdicionarEvento(new AlunoAdicionadoEvent(aluno.Id, aluno.Nome));
        
            return await _alunoRepository.UnitOfWork.Commit();
        }

        private bool ValidarComando(Command message)
        {
            if (!message.EhValido())
            {
                foreach (var item in message.ValidationResult.Errors)
                {
                    _mediator.Publish(new DomainNotification(message.MessageType, item.ErrorMessage));
                }
                return false;
            }
            return true;
        }
    }
}
