﻿namespace PlataformaEducacaoOnline.Api.DTO
{
    public class UserTokenDto
    {
        public string Id { get; set; }
        public string Email { get; set; }

        public IEnumerable<ClaimDto> Claims { get; set; }
    }
}
