using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlataformaEducacaoOnline.Api.Controllers.Base;
using PlataformaEducacaoOnline.Api.DTO;
using PlataformaEducacaoOnline.Core.DomainObjects;
using PlataformaEducacaoOnline.GestaoConteudo.Application.Commands;
using System.Net;

namespace PlataformaEducacaoOnline.Api.Controllers
{
    [Route("api/cursos")]
    public class CursoController(INotificationHandler<DomainNotification> notificacoes,
                                  IMediator mediator)
        : MainController(notificacoes, mediator)
    {
        private readonly IMediator _mediator = mediator;

        [Authorize(Roles = "ADMIN")]
        [HttpPost]
        public async Task<IActionResult> Adicionar([FromBody] CursoDto curso)
        {
            var command = new AdicionarCursoCommand(curso.Nome, UsuarioId, curso.Conteudo);
            await _mediator.Send(command);

            return RetornoPadrao(HttpStatusCode.Created);
        }
    }
}
