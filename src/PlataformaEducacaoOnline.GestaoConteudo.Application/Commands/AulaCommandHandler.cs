using MediatR;
using PlataformaEducacaoOnline.Core.DomainObjects;
using PlataformaEducacaoOnline.Core.Messages;
using PlataformaEducacaoOnline.GestaoConteudo.Application.Events;
using PlataformaEducacaoOnline.GestaoConteudo.Domain;

namespace PlataformaEducacaoOnline.GestaoConteudo.Application.Commands
{
    public class AulaCommandHandler : IRequestHandler<AdicionarAulaCommand, bool>,
        IRequestHandler<AtualizarAulaCommand, bool>,
        IRequestHandler<RemoverAulaCommand, bool>,
        IRequestHandler<RemoverMaterialAulaCommand, bool>
    {
        private readonly ICursoRepositoty _cursoRepository;
        private readonly IMediator _mediator;

        public AulaCommandHandler(ICursoRepositoty cursoRepository, IMediator mediator)
        {
            _cursoRepository = cursoRepository;
            _mediator = mediator;
        }

        public async Task<bool> Handle(AdicionarAulaCommand message, CancellationToken cancellationToken)
        {
            if (!ValidarComando(message))
                return false;

            var curso = await _cursoRepository.ObterCursoPorId(message.CursoId);
            if (curso is null)
            {
                await _mediator.Publish(new DomainNotification(message.MessageType, "Curso não encontrado."), cancellationToken);
                return false;
            }

            var aula = new Aula(message.Titulo, message.Conteudo, message.CursoId);
            foreach (var material in message.Materiais)
            {
                aula.AdicionarMaterial(material);
            }

            _cursoRepository.Adicionar(aula);

            curso.AdicionarEvento(new AulaAdicionadaEvent(aula.Id, aula.CursoId, aula.Conteudo, aula.Titulo));

            return await _cursoRepository.UnitOfWork.Commit();
        }

        public async Task<bool> Handle(AtualizarAulaCommand message, CancellationToken cancellationToken)
        {
            if (!ValidarComando(message))
                return false;

            var aula = await _cursoRepository.ObterAulaPorId(message.AulaId);

            if (aula is null)
            {
                await _mediator.Publish(new DomainNotification(message.MessageType, "Aula não encontrado."), cancellationToken);
                return false;
            }

            if (message.Titulo.Trim() != aula.Titulo.Trim()) aula.AtualizarTitulo(message.Titulo);
            if (message.Conteudo != aula.Conteudo) aula.AtualizarConteudo(message.Conteudo);

            _cursoRepository.Atualizar(aula);

            aula.AdicionarEvento(new AulaAtualizadaEvent(aula.Id, aula.Titulo, aula.Conteudo));

            return await _cursoRepository.UnitOfWork.Commit();
        }

        public async Task<bool> Handle(RemoverAulaCommand message, CancellationToken cancellationToken)
        {
            if (!ValidarComando(message))
                return false;

            var aula = await _cursoRepository.ObterAulaPorId(message.AulaId);
            var materiais = await _cursoRepository.ObterMateriaisPorAulaId(message.AulaId);

            if (aula is null)
            {
                await _mediator.Publish(new DomainNotification(message.MessageType, "Aula não encontrado."), cancellationToken);
                return false;
            }

            if (materiais.Any())
            {
                await _mediator.Publish(new DomainNotification(message.MessageType, "Aula não pode ser removido pois existe materiais vinculadas a ele."), cancellationToken);
                return false;
            }

            _cursoRepository.Remover(aula);

            aula.AdicionarEvento(new AulaRemovidaEvent(aula.Id, aula.CursoId, aula.Conteudo, aula.Titulo));

            return await _cursoRepository.UnitOfWork.Commit();
        }

        public async Task<bool> Handle(RemoverMaterialAulaCommand message, CancellationToken cancellationToken)
        {
            if (!ValidarComando(message))
                return false;

            var material = await _cursoRepository.ObterMaterialPorId(message.MaterialId);
            

            if (material is null)
            {
                await _mediator.Publish(new DomainNotification(message.MessageType, "Material não encontrado."), cancellationToken);
                return false;
            }

            _cursoRepository.Remover(material);

            material.AdicionarEvento(new MaterialAulaRemovidaEvent(material.Id, material.Nome, material.AulaId));

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
