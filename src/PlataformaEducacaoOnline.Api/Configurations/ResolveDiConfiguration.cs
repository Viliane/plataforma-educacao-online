using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using PlataformaEducacaoOnline.Api.Extensions;
using PlataformaEducacaoOnline.Api.Interfaces;
using PlataformaEducacaoOnline.Core.DomainObjects;

namespace PlataformaEducacaoOnline.Api.Configurations
{
    public static class ResolveDiConfiguration
    {
        public static WebApplicationBuilder AddResolveDependencie(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IAppIdentityUser, AppIdentityUser>();
            builder.Services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

            return builder;
        }
    }
}
