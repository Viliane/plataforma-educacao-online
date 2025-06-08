using MediatR;
using PlataformaEducacaoOnline.Core.Bus;
using PlataformaEducacaoOnline.Core.DomainObjects;
using PlataformaEducacaoOnline.Core.Messages;
using PlataformaEducacaoOnline.Core.Messages.CommonMessagens.IntegrationEvent;
using PlataformaEducacaoOnline.GestaoAluno.Application.Events;
using PlataformaEducacaoOnline.GestaoAluno.Domain;

namespace PlataformaEducacaoOnline.GestaoAluno.Application.Commands
{
    public class AlunoCommandHandler : IRequestHandler<AdicionarAlunoCommand, bool>,
        IRequestHandler<AdicionarMatriculaCommand, bool>,
        IRequestHandler<RealizarPagamentoCommand, bool>,
        IRequestHandler<AtualizarMatriculaCommand, bool>,
        IRequestHandler<FinalizarCursoCommand, bool>
    {
        private readonly IAlunoRepository _alunoRepository;
        private readonly IMediator _mediator;
        private readonly IMediatrHandler _mediatorHandler;

        public AlunoCommandHandler(IAlunoRepository alunoRepository, IMediator mediator, IMediatrHandler mediatorHandler)
        {
            _alunoRepository = alunoRepository;
            _mediator = mediator;
            _mediatorHandler = mediatorHandler;
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

        public async Task<bool> Handle(AdicionarMatriculaCommand message, CancellationToken cancellationToken)
        {
            if (!ValidarComando(message))
                return false;

            var matricula = new Matricula(message.CursoId, message.AlunoId);
            _alunoRepository.Adicionar(matricula);

            return await _alunoRepository.UnitOfWork.Commit();
        }

        public async Task<bool> Handle(RealizarPagamentoCommand message, CancellationToken cancellationToken)
        {
            if (!ValidarComando(message))
                return false;

            await _mediatorHandler.PublicarEvento(new PedidoMatriculaConfirmadoEvent(
                message.AlunoId, 
                message.CursoId, 
                message.MatriculaId,
                message.Valor, 
                message.NomeCartao, 
                message.NumeroCartao, 
                message.ExpiracaoCartao, 
                message.CvvCartao));

            return true;
        }

        public async Task<bool> Handle(AtualizarMatriculaCommand message, CancellationToken cancellationToken)
        {
            if (!ValidarComando(message))
                return false;

            var matricula = await _alunoRepository.ObterMatriculaPorId(message.MatriculaId);

            if (matricula is null)
            {
                await _mediator.Publish(new DomainNotification(message.MessageType, "Matrícula não encontrada."));
                return false;
            }

            matricula.AtivarMatricula();
            _alunoRepository.Atualizar(matricula);

            return await _alunoRepository.UnitOfWork.Commit();
        }

        public async Task<bool> Handle(FinalizarCursoCommand message, CancellationToken cancellationToken)
        {
            if (!ValidarComando(message))
                return false;

            var matricula = await _alunoRepository.ObterMatriculaPorAlunoIdCursoId(message.AlunoId, message.CursoId);
            if (matricula is null)
            {
                await _mediator.Publish(new DomainNotification(message.MessageType, "Matrícula não encontrada."));
                return false;
            }

            matricula.ConcluirCurso();
            _alunoRepository.Atualizar(matricula);

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
