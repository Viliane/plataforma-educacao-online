﻿namespace PlataformaEducacaoOnline.Api.DTO
{
    public class LoginResponseDto
    {
        public string AccessToken { get; set; }
        public double ExpiresIn { get; set; }
        public UserTokenDto UserToken { get; set; }
    }
}
