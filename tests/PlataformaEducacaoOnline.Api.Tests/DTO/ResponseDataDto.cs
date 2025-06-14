using PlataformaEducacaoOnline.Api.DTO;
using System.Security.Claims;

namespace PlataformaEducacaoOnline.Api.Tests.DTO
{
    public class ResponseDataDto
    {
        public bool Sucesso { get; set; }
        public LoginResponseDto Data { get; set; } = new();
    }
}
