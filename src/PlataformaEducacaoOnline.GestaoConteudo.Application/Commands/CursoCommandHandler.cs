using MediatR;
using PlataformaEducacaoOnline.Core.DomainObjects;
using PlataformaEducacaoOnline.Core.Messages;
using PlataformaEducacaoOnline.GestaoConteudo.Application.Events;
using PlataformaEducacaoOnline.GestaoConteudo.Domain;

namespace PlataformaEducacaoOnline.GestaoConteudo.Application.Commands
{
    public class CursoCommandHandler : IRequestHandler<AdicionarCursoCommand, bool>,
        IRequestHandler<AtualizarCursoCommand, bool>,
        IRequestHandler<RemoverCursoCommand, bool>
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

            var curso = new Curso(message.Nome, message.UsuarioId, message.ConteudoProgramatico, message.Valor);
            _cursoRepository.Adicionar(curso);

            curso.AdicionarEvento(new CursoAdicionadoEvent(curso.Id, curso.UsuarioId, curso.Nome, curso.ConteudoProgramatico.DescricaoConteudoProgramatico, curso.Valor));

            return await _cursoRepository.UnitOfWork.Commit();
        }

        public async Task<bool> Handle(AtualizarCursoCommand message, CancellationToken cancellationToken)
        {
            if (!ValidarComando(message))
                return false;

            var curso = await _cursoRepository.ObterCursoPorId(message.CursoId);

            if (curso is null)
            {
                await _mediator.Publish(new DomainNotification(message.MessageType, "Curso não encontrado."), cancellationToken);
                return false;
            }
 
            if (message.Nome.Trim() != curso.Nome.Trim()) curso.AtualizarNome(message.Nome);
            if (message.ConteudoProgramatico.DescricaoConteudoProgramatico != curso.ConteudoProgramatico.DescricaoConteudoProgramatico) curso.AtualizarConteudoProgramatico(message.ConteudoProgramatico);
            curso.AtualizarDataAtualizacao();
            if (message.Valor != curso.Valor) curso.AtualizarValor(message.Valor);

            _cursoRepository.Atualizar(curso);

            curso.AdicionarEvento(new CursoAtualizadoEvent(curso.Id, curso.UsuarioId, curso.Nome, curso.ConteudoProgramatico.DescricaoConteudoProgramatico, curso.Valor));

            return await _cursoRepository.UnitOfWork.Commit();
        }

        public async Task<bool> Handle(RemoverCursoCommand message, CancellationToken cancellationToken)
        {
            if (!ValidarComando(message))
                return false;

            var curso = await _cursoRepository.ObterCursoPorId(message.CursoId);
            var aulas = await _cursoRepository.ObterAulasPorCursoId(message.CursoId);

            if (curso is null)
            {
                await _mediator.Publish(new DomainNotification(message.MessageType, "Curso não encontrado."), cancellationToken);
                return false;
            }

            if (aulas.Any())
            {
                await _mediator.Publish(new DomainNotification(message.MessageType, "Curso não pode ser removido pois existe aulas vinculadas a ele."), cancellationToken);
                return false;
            }

            _cursoRepository.Remover(curso);

            curso.AdicionarEvento(new CursoRemovidoEvent(curso.Id, curso.UsuarioId, curso.Nome, curso.ConteudoProgramatico.DescricaoConteudoProgramatico, curso.Valor));

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
