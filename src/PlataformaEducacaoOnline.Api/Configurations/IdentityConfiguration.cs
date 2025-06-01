using Microsoft.AspNetCore.Identity;
using PlataformaEducacaoOnline.Api.Data;
using PlataformaEducacaoOnline.Api.Extensions;
using System;

namespace PlataformaEducacaoOnline.Api.Configurations
{
    public static class IdentityConfiguration
    {
        public static WebApplicationBuilder AddIdentityConfiguration(this WebApplicationBuilder builder)
        {
            builder.Services.AddIdentity<IdentityUser, IdentityRole>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddErrorDescriber<IdentityMensagensPortugues>()
                .AddDefaultTokenProviders();

            return builder;
        }
    }
}
