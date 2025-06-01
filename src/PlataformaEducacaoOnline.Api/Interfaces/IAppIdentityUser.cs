﻿namespace PlataformaEducacaoOnline.Api.Interfaces
{
    public interface IAppIdentityUser
    {
        string GetUserId();

        bool IsAutenticated();

        bool IsOwner(string? idIdentityUser);
    }
}
