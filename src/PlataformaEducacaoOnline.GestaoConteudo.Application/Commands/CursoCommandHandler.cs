using MediatR;
using PlataformaEducacaoOnline.Core.DomainObjects;
using PlataformaEducacaoOnline.Core.Messages;
using PlataformaEducacaoOnline.GestaoConteudo.Application.Events;
using PlataformaEducacaoOnline.GestaoConteudo.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaEducacaoOnline.GestaoConteudo.Application.Commands
{
    public class CursoCommandHandler : IRequestHandler<AdicionarCursoCommand, bool>
    {
        private readonly ICursoRepositoty _cursoRepository;
        private readonly IMediator _mediator;

        public CursoCommandHandler(ICursoRepositoty cursoRepository, IMediator mediator)
        {
            _cursoRepository = cursoRepository;
            _mediator = mediator;
        }

        public async Task<bool> Handle(AdicionarCursoCommand message, CancellationToken cancellationToken)
        {
            if (!ValidarComando(message))
                return false;

            var curso = new Curso(message.Nome, message.UsuarioId, message.ConteudoProgramatico);
            _cursoRepository.Adicionar(curso);

            curso.AdicionarEvento(new CursoAdicionadoEvent(curso.Id, curso.UsuarioId, curso.Nome, curso.ConteudoProgramatico.DescricaoConteudoProgramatico));

            return await _cursoRepository.UnitOfWork.Commit();
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
