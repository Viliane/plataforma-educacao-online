using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlataformaEducacaoOnline.Api.Controllers.Base;
using PlataformaEducacaoOnline.Api.DTO;
using PlataformaEducacaoOnline.Core.DomainObjects;
using PlataformaEducacaoOnline.Core.DomainObjects.DTO;
using PlataformaEducacaoOnline.GestaoAluno.Application.Commands;
using PlataformaEducacaoOnline.GestaoAluno.Application.Queries;
using PlataformaEducacaoOnline.GestaoAluno.Domain;
using PlataformaEducacaoOnline.GestaoConteudo.Application.Queries;
using PlataformaEducacaoOnline.PagamentoFaturamento.Business;
using System.Net;

namespace PlataformaEducacaoOnline.Api.Controllers
{
    [Route("api/aluno")]
    public class AlunoController(INotificationHandler<DomainNotification> notificacoes,
                                   IMediator mediator, IAlunoQueries alunoQueries, 
                                   ICursoQueries cursoQueries, 
                                   IPagamentoRepository pagamentoRepository)
        : MainController(notificacoes, mediator)
    {
        private readonly IMediator _mediator = mediator;
        private readonly IAlunoQueries _alunoQueries = alunoQueries;
        private readonly ICursoQueries _cursoQueries = cursoQueries;
        private readonly IPagamentoRepository _pagamentoRepository = pagamentoRepository;

        [Authorize(Roles = "ALUNO")]
        [HttpPost("matricula")]
        public async Task<IActionResult> Adicionar([FromBody] MatriculaDto matricula)
        {
            var command = new AdicionarMatriculaCommand(UsuarioId, matricula.CursoId);
            await _mediator.Send(command);
            return RetornoPadrao(HttpStatusCode.Created);
        }

        [Authorize(Roles = "ALUNO")]
        [HttpPost("resumoMatricula")]
        public async Task<ActionResult<ResumoMatriculaDto>> ObterResumoMatricula([FromBody] MatriculaDto matricula)
        {
            if (matricula.AlunoId != UsuarioId)
            {
                return Unauthorized(new { Sucesso = false, Erros = new[] { "Você não tem permissão para acessar esta matrícula." } });
            }

            var dadosMatricula = await _alunoQueries.ObterMatriculaAlunoIdCursoId(UsuarioId, matricula.CursoId);

            if (dadosMatricula == null)
            {
                return NotFound(new { Sucesso = false, Erros = new[] { "Matrícula não encontrada." } });
            }

            var curso = await _cursoQueries.ObterCursoPorId(matricula.CursoId);

            if (curso == null)
            {
                return NotFound(new { Sucesso = false, Erros = new[] { "Curso não encontrado." } });
            }

            var resumoMatricula = new ResumoMatriculaDto
            {
                Matricula = dadosMatricula.Id,
                NomeCurso = curso.Nome,
                StatusMatricula = ((StatusMatricula)dadosMatricula.Status).ToString(),
            };

            return RetornoPadrao(HttpStatusCode.OK, resumoMatricula);
        }

        [Authorize(Roles = "ALUNO")]
        [HttpPost("realizarPagamento")]
        public async Task<IActionResult> RealizarPagamento([FromBody] PagamentoMatricula pagamentoDto)
        {
            if (pagamentoDto.AlunoId != UsuarioId)
            {
                return Unauthorized(new { Sucesso = false, Erros = new[] { "Você não tem permissão para realizar o pagamento." } });
            }

            var dadosMatricula = await _alunoQueries.ObterMatriculaAlunoIdCursoId(UsuarioId, pagamentoDto.CursoId);

            if (dadosMatricula == null)
            {
                return NotFound(new { Sucesso = false, Erros = new[] { "Matrícula não encontrada." } });
            }

            var curso = await _cursoQueries.ObterCursoPorId(pagamentoDto.CursoId);

            if (curso == null)
            {
                return NotFound(new { Sucesso = false, Erros = new[] { "Curso não encontrado." } });
            }

            var RealizarPagamentoCommand = new RealizarPagamentoCommand(pagamentoDto.AlunoId, pagamentoDto.CursoId, dadosMatricula.Id, pagamentoDto.Valor, 
                                                       pagamentoDto.NomeCartao, pagamentoDto.NumeroCartao, 
                                                       pagamentoDto.ExpiracaoCartao, pagamentoDto.CvvCartao);

            await _mediator.Send(RealizarPagamentoCommand);

            var transacao = _pagamentoRepository.ObterTransacaoPorAlunoIdCursoIdMatricula(UsuarioId, curso.Id, dadosMatricula.Id);

            if (transacao == null)
            {
                return NotFound(new { Sucesso = false, Erros = new[] { "Transação de pagamento não encontrada." } });
            }

            if ((int)transacao.Status == (int)StatusTransacao.Pago)
            {
                var AtualizarMatriculaCommand = new AtualizarMatriculaCommand(dadosMatricula.Id);
                await _mediator.Send(AtualizarMatriculaCommand);
            }

            return RetornoPadrao(HttpStatusCode.Created);
        }
    }
}
