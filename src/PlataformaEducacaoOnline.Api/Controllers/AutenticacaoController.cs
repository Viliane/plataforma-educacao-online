using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PlataformaEducacaoOnline.Api.Controllers.Base;
using PlataformaEducacaoOnline.Api.DTO;
using PlataformaEducacaoOnline.Api.Jwt;
using PlataformaEducacaoOnline.Core.DomainObjects;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace PlataformaEducacaoOnline.Api.Controllers
{
    [Route("api/conta")]
    public class AutenticacaoController(SignInManager<IdentityUser> signInManager,
                                        UserManager<IdentityUser> userManager,
                                        IMediator mediator,
                                        INotificationHandler<DomainNotification> notificacoes,
                                        IOptions<JwtSettings> jwtSettings) : MainController(notificacoes, mediator)
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost("registrar/admin")]
        public async Task<ActionResult> RegistrarAdmin(RegisterUserDto registerUser)
        {
            if (!ModelState.IsValid)
            {
                NotificarErro(ModelState);
                return RetornoPadrao();
            }

            var result = await RegistrarUsuario(registerUser, "ADMIN");

            if (!result.IdentityResult.Succeeded)
            {
                NotificarErro(result.IdentityResult);
                return RetornoPadrao();
            }

            if (!OperacaoValida())
                return RetornoPadrao();

            var token = await GenerateTokenAsync(registerUser.Email!);
            return RetornoPadrao(HttpStatusCode.Created, token);
        }

        [HttpPost("registrar/aluno")]
        public async Task<ActionResult> RegistrarAluno(RegisterUserDto registerUser)
        {
            if (!ModelState.IsValid)
            {
                NotificarErro(ModelState);
                return RetornoPadrao();
            }

            var result = await RegistrarUsuario(registerUser, "ALUNO");

            if (!result.IdentityResult.Succeeded)
            {
                NotificarErro(result.IdentityResult);
                return RetornoPadrao();
            }

            if (!OperacaoValida())
                return RetornoPadrao();

            var token = await GenerateTokenAsync(registerUser.Email!);
            return RetornoPadrao(HttpStatusCode.Created, token);
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginUserDto loginUser)
        {
            if (!ModelState.IsValid)
            {
                NotificarErro(ModelState);
                return RetornoPadrao();
            }

            var result = await signInManager.PasswordSignInAsync(loginUser.Email!, loginUser.Senha!, false, true);

            if (result.Succeeded)
            {
                var loginResponse = await GenerateTokenAsync(loginUser.Email!);
                return RetornoPadrao(HttpStatusCode.Created, loginResponse);
            }

            if (result.IsLockedOut)
            {
                NotificarErro("Identity", "Usuário bloqueado temporariamente. Tente mais tarde novamente.");
                return RetornoPadrao();
            }

            NotificarErro("Identity", "Usuário ou Senha incorretos");
            return RetornoPadrao();
        }

        private async Task<LoginResponseDto> GenerateTokenAsync(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            var roles = await userManager.GetRolesAsync(user);
            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Id)
            };

            claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));

            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(jwtSettings.Value.Segredo!);

            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Issuer = jwtSettings.Value.Emissor,
                Audience = jwtSettings.Value.Audiencia,
                Expires = DateTime.UtcNow.AddHours(jwtSettings.Value.ExpiracaoHoras),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });

            var encodedToken = tokenHandler.WriteToken(token);

            var response = new LoginResponseDto
            {
                AccessToken = encodedToken,
                ExpiresIn = TimeSpan.FromHours(jwtSettings.Value.ExpiracaoHoras).TotalSeconds,
                UserToken = new UserTokenDto
                {
                    Id = user.Id,
                    Email = user.Email,
                    Claims = claims.Select(c => new ClaimDto { Type = c.Type, Value = c.Value })
                }
            };

            return response;
        }

        private async Task<(IdentityResult IdentityResult, string UsuarioId)> RegistrarUsuario(RegisterUserDto registerUser, string role)
        {
            var novoUsuario = new IdentityUser
            {
                UserName = registerUser.Email,
                Email = registerUser.Email,
                EmailConfirmed = true,
            };

            
            var resultadoCriacao = await userManager.CreateAsync(novoUsuario, registerUser.Senha!);
                        
            if (resultadoCriacao.Succeeded)
            {
                await userManager.AddToRoleAsync(novoUsuario, role);
            }
            
            return (resultadoCriacao, resultadoCriacao.Succeeded ? novoUsuario.Id : string.Empty);
        }
    }
}
