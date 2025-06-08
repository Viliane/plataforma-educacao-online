using MediatR;
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
        IRequestHandler<AtualizarMatriculaCommand, bool>
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

            var matricula = new Matricula(message.CursoId, message.AlunoId);

            matricula.AdicionarEvento(new PedidoMatriculaConfirmadoEvent(
                message.AlunoId, 
                message.CursoId, 
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
