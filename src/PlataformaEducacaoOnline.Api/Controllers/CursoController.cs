using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlataformaEducacaoOnline.Api.Controllers.Base;
using PlataformaEducacaoOnline.Api.DTO;
using PlataformaEducacaoOnline.Core.DomainObjects;
using PlataformaEducacaoOnline.GestaoConteudo.Application.Commands;
using PlataformaEducacaoOnline.GestaoConteudo.Application.Queries;
using PlataformaEducacaoOnline.GestaoConteudo.Application.Queries.DTO;
using System.Net;

namespace PlataformaEducacaoOnline.Api.Controllers
{
    [Route("api/cursos")]
    public class CursoController(INotificationHandler<DomainNotification> notificacoes,
                                  IMediator mediator, ICursoQueries cursoQueries)
        : MainController(notificacoes, mediator)
    {
        private readonly IMediator _mediator = mediator;
        private readonly ICursoQueries _cursoQueries = cursoQueries;

        [Authorize(Roles = "ADMIN")]
        [HttpPost]
        public async Task<IActionResult> Adicionar([FromBody] CursoDto curso)
        {
            var command = new AdicionarCursoCommand(curso.Nome, UsuarioId, curso.Conteudo, curso.Valor);
            await _mediator.Send(command);

            return RetornoPadrao(HttpStatusCode.Created);
        }

        [Authorize(Roles = "ADMIN")]
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Atualizar(Guid id, [FromBody] CursoDto curso)
        {   
            var command = new AtualizarCursoCommand(curso.Nome, id, curso.Conteudo, curso.Valor);

            await _mediator.Send(command);
            return RetornoPadrao(HttpStatusCode.NoContent);
        }

        [Authorize(Roles = "ADMIN")]
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Deletar(Guid id)
        {
            var command = new RemoverCursoCommand(id);
            await _mediator.Send(command);
            return RetornoPadrao(HttpStatusCode.NoContent);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CursoQueryDto>>> ObterTodosCursos()
        {
            var cursos = await _cursoQueries.ObterTodosCursos();
            return RetornoPadrao(HttpStatusCode.OK, cursos);
        }

        [AllowAnonymous]
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<CursoQueryDto>> ObterCursoPorId(Guid id)
        {
            var curso = await _cursoQueries.ObterCursoPorId(id);
            return RetornoPadrao(HttpStatusCode.OK, curso);
        }
    }
}
