using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PlataformaEducacaoOnline.Core.DomainObjects;
using System.Net;
using System.Security.Claims;

namespace PlataformaEducacaoOnline.Api.Controllers.Base
{
    [ApiController]
    public abstract class MainController(INotificationHandler<DomainNotification> notificacoes,
        IMediator mediator) : ControllerBase
    {
        private readonly DomainNotificationHandler _notificacoes = (DomainNotificationHandler)notificacoes;

        protected Guid UsuarioId => Guid.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty);

        protected ActionResult RetornoPadrao(HttpStatusCode statusCode = HttpStatusCode.OK, object? data = null)
        {
            if (OperacaoValida())
            {
                return new ObjectResult(new
                {
                    Sucesso = true,
                    Data = data,
                })
                {
                    StatusCode = (int)statusCode
                };
            }

            return BadRequest(new
            {
                Sucesso = false,
                Erros = _notificacoes.ObterNotificacoes().Select(n => n.Value)
            });
        }

        protected bool OperacaoValida()
        {
            return !_notificacoes.TemNotificacao();
        }

        protected void NotificarErro(string codigo, string mensagem)
        {
            mediator.Publish(new DomainNotification(codigo, mensagem));
        }

        protected void NotificarErro(ModelStateDictionary modelstate)
        {
            foreach (var msg in modelstate.Values
                         .SelectMany(e => e.Errors)
                         .Select(e => e.Exception?.Message ?? e.ErrorMessage))
                NotificarErro("ModelState", msg);
        }

        protected void NotificarErro(IdentityResult identityResult)
        {
            foreach (var erro in identityResult.Errors.Select(e => e.Description)) NotificarErro("Identity", erro);
        }
    }
}
