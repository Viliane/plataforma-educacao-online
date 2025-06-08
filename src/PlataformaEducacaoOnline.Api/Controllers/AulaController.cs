using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlataformaEducacaoOnline.Api.Controllers.Base;
using PlataformaEducacaoOnline.Api.DTO;
using PlataformaEducacaoOnline.Core.DomainObjects;
using PlataformaEducacaoOnline.Core.DomainObjects.DTO;
using PlataformaEducacaoOnline.GestaoAluno.Application.Queries;
using PlataformaEducacaoOnline.GestaoConteudo.Application.Commands;
using PlataformaEducacaoOnline.GestaoConteudo.Application.Queries;
using PlataformaEducacaoOnline.GestaoConteudo.Application.Queries.DTO;
using PlataformaEducacaoOnline.GestaoConteudo.Domain;
using System.Net;

namespace PlataformaEducacaoOnline.Api.Controllers
{
    [Route("api/aulas")]
    public class AulaController(INotificationHandler<DomainNotification> notificacoes,
                                  IMediator mediator, ICursoQueries cursoQueries,
                                  IAlunoQueries alunoQueries)
        : MainController(notificacoes, mediator)
    {
        private readonly IMediator _mediator = mediator;
        private readonly ICursoQueries _cursoQueries = cursoQueries;
        private readonly IAlunoQueries _alunoQueries = alunoQueries;

        [Authorize(Roles = "ADMIN")]
        [HttpPost]
        public async Task<IActionResult> Adicionar([FromBody] AulaDto aula)
        {
            List<Material> materiaisDomain = new List<Material>();

            if (aula.Materiais.Any())
            {
                foreach (var material in aula.Materiais)
                {   
                    var materialDomain = new Material(material.Nome);
                    materiaisDomain.Add(materialDomain);
                }
            }

            var command = new AdicionarAulaCommand(aula.Titulo, aula.Conteudo, aula.CursoId, materiaisDomain);

            await _mediator.Send(command);

            return RetornoPadrao(HttpStatusCode.Created);
        }

        [Authorize(Roles = "ADMIN")]
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Atualizar(Guid id, [FromBody] AulaDto aula)
        {
            var command = new AtualizarAulaCommand(id, aula.Titulo, aula.Conteudo);

            await _mediator.Send(command);
            return RetornoPadrao(HttpStatusCode.NoContent);
        }

        [Authorize(Roles = "ADMIN")]
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Deletar(Guid id)
        {
            var command = new RemoverAulaCommand(id);
            await _mediator.Send(command);
            return RetornoPadrao(HttpStatusCode.NoContent);
        }

        [Authorize(Roles = "ADMIN")]
        [HttpDelete("material/{id:guid}")]
        public async Task<IActionResult> DeletarMaterial(Guid id)
        {
            var command = new RemoverMaterialAulaCommand(id);
            await _mediator.Send(command);
            return RetornoPadrao(HttpStatusCode.NoContent);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AulaQueryDto>>> ObterTodasAulas()
        {
            var cursos = await _cursoQueries.ObterTodasAulas();
            return RetornoPadrao(HttpStatusCode.OK, cursos);
        }

        [AllowAnonymous]
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<AulaQueryDto>> ObterAulaPorId(Guid id)
        {
            var curso = await _cursoQueries.ObterAulaPorId(id);
            return RetornoPadrao(HttpStatusCode.OK, curso);
        }

        [Authorize(Roles = "ALUNO")]
        [HttpPost("realizar-aula")]
        public async Task<IActionResult> RealizarAula([FromBody] EvolucaoAulaDto evolucaoAulaDto)
        {
            var dadosMatricula = await _alunoQueries.ObterMatriculaAlunoIdCursoId(UsuarioId, evolucaoAulaDto.CursoId);

            if (dadosMatricula == null)
            {
                return NotFound(new { Sucesso = false, Erros = new[] { "Matrícula não encontrada." } });
            }

            if ((int)dadosMatricula.Status != (int)StatusMatricula.Ativa)
            {
                NotificarErro("Realizar Aula", "Aula ainda não esta disponivel para realização.");
                return RetornoPadrao();
            }

            var command = new RealizarAulaCommand(evolucaoAulaDto.AulaId, UsuarioId);
            await _mediator.Send(command);

            return RetornoPadrao(HttpStatusCode.Created);
        }

        [Authorize(Roles = "ALUNO")]
        [HttpPost("concluir-aula")]
        public async Task<IActionResult> ConcluirAula([FromBody] EvolucaoAulaDto evolucaoAulaDto)
        {
            var dadosMatricula = await _alunoQueries.ObterMatriculaAlunoIdCursoId(UsuarioId, evolucaoAulaDto.CursoId);

            if (dadosMatricula == null)
            {
                return NotFound(new { Sucesso = false, Erros = new[] { "Matrícula não encontrada." } });
            }

            if ((int)dadosMatricula.Status != (int)StatusMatricula.Ativa)
            {
                NotificarErro("Concluir Aula", "Aula ainda não esta disponivel para conclusão.");
                return RetornoPadrao();
            }

            var command = new ConcluirAulaCommand(evolucaoAulaDto.AulaId, UsuarioId);
            await _mediator.Send(command);

            return RetornoPadrao(HttpStatusCode.Created);
        }
    }
}
