using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlataformaEducacaoOnline.Api.Controllers.Base;
using PlataformaEducacaoOnline.Api.DTO;
using PlataformaEducacaoOnline.Core.DomainObjects;
using PlataformaEducacaoOnline.Core.DomainObjects.DTO;
using PlataformaEducacaoOnline.GestaoAluno.Application.Commands;
using PlataformaEducacaoOnline.GestaoAluno.Application.Queries;
using PlataformaEducacaoOnline.GestaoConteudo.Application.Commands;
using PlataformaEducacaoOnline.GestaoConteudo.Application.Queries;
using PlataformaEducacaoOnline.PagamentoFaturamento.Business;
using System.Net;

namespace PlataformaEducacaoOnline.Api.Controllers
{
    [Route("api/matricula")]
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
        [HttpPost()]
        public async Task<IActionResult> Adicionar([FromBody] MatriculaDto matricula)
        {
            var command = new AdicionarMatriculaCommand(UsuarioId, matricula.CursoId);
            await _mediator.Send(command);

            var aulaCurso = await _cursoQueries.ObterAulasPorCursoId(matricula.CursoId);

            var evolucaoAulaCommand = new EvoluirAulaCommand(UsuarioId, matricula.CursoId, aulaCurso);
            await _mediator.Send(evolucaoAulaCommand);

            return RetornoPadrao(HttpStatusCode.Created);
        }

        [Authorize(Roles = "ALUNO")]
        [HttpPost("resumo-matricula")]
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
        [HttpPost("realizar-pagamento")]
        public async Task<IActionResult> RealizarPagamento([FromBody] PagamentoMatriculaDto pagamentoDto)
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

            var pagamento = await _pagamentoRepository.ObterPagamentoPorAlunoIdCursoIdMatriculaId(UsuarioId, curso.Id, dadosMatricula.Id);

            if (pagamento != null)
            {
                NotificarErro("Pagamento", "Você não tem permissão para realizar outro pagamento para a mesma matricula.");
                return RetornoPadrao();
            }

            var RealizarPagamentoCommand = new RealizarPagamentoCommand(pagamentoDto.AlunoId, pagamentoDto.CursoId, dadosMatricula.Id, pagamentoDto.Valor,
                                                       pagamentoDto.NomeCartao, pagamentoDto.NumeroCartao,
                                                       pagamentoDto.ExpiracaoCartao, pagamentoDto.CvvCartao);

            await _mediator.Send(RealizarPagamentoCommand);

            var transacao = await _pagamentoRepository.ObterTransacaoPorAlunoIdCursoIdMatricula(UsuarioId, curso.Id, dadosMatricula.Id);

            if ((int)transacao.StatusTransacao == (int)StatusTransacao.Pago)
            {
                var AtualizarMatriculaCommand = new AtualizarMatriculaCommand(dadosMatricula.Id);
                await _mediator.Send(AtualizarMatriculaCommand);
            }

            return RetornoPadrao(HttpStatusCode.Created);
        }

        [Authorize(Roles = "ALUNO")]
        [HttpPost("finalizar-curso")]
        public async Task<IActionResult> FinalizarCurso([FromBody] MatriculaDto matricula)
        {  
            var evolucaoAula = await _cursoQueries.ObterEvolucaoAulaPorUsuarioIdCursoId(UsuarioId, matricula.CursoId);            
            var todasAulasConcluidas = evolucaoAula != null && evolucaoAula.All(a => a.Status == (int)StatusAula.Concluida);

            if (!todasAulasConcluidas)
            {
                NotificarErro("FinalizarCurso", "Nem todas as aulas foram concluídas.");
                return RetornoPadrao();
            }

            var finalizarCursoCommand = new FinalizarCursoCommand(UsuarioId, matricula.CursoId);
            await _mediator.Send(finalizarCursoCommand);

            return RetornoPadrao(HttpStatusCode.OK);
        }

        [Authorize(Roles = "ALUNO")]
        [HttpGet("gerar-certificado/{matriculaId:guid}")]
        public async Task<IActionResult> GerarCertificado(Guid matriculaId)
        {
            var certificado = await _alunoQueries.ObterCertificado(matriculaId, UsuarioId);

            if (certificado == null)
            {
                return NoContent();
            }

            return File(certificado, "application/pdf", "certificado.pdf");
        }
    }
}
