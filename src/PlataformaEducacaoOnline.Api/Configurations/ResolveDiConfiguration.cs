using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using PlataformaEducacaoOnline.Api.Extensions;
using PlataformaEducacaoOnline.Api.Interfaces;
using PlataformaEducacaoOnline.Core.DomainObjects;
using PlataformaEducacaoOnline.GestaoAluno.Application.Commands;
using PlataformaEducacaoOnline.GestaoAluno.Data;
using PlataformaEducacaoOnline.GestaoAluno.Data.Repository;
using PlataformaEducacaoOnline.GestaoAluno.Domain;
using PlataformaEducacaoOnline.GestaoConteudo.Application.Commands;
using PlataformaEducacaoOnline.GestaoConteudo.Application.Events;
using PlataformaEducacaoOnline.GestaoConteudo.Application.Queries;
using PlataformaEducacaoOnline.GestaoConteudo.Data;
using PlataformaEducacaoOnline.GestaoConteudo.Data.Repository;
using PlataformaEducacaoOnline.GestaoConteudo.Domain;

namespace PlataformaEducacaoOnline.Api.Configurations
{
    public static class ResolveDiConfiguration
    {
        public static WebApplicationBuilder AddResolveDependencie(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IAppIdentityUser, AppIdentityUser>();
            builder.Services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();
            //builder.Services.AddScoped<INotificationHandler<CursoAdicionadoEvent>, CursoAdicionadoEventHandler>();

            // Gestão de Conteúdos
            builder.Services.AddScoped<ICursoRepositoty, CursoRepository>();
            builder.Services.AddScoped<ICursoQueries, CursoQueries>();
            builder.Services.AddScoped<GestaoConteudoContext>();

            //Gestão de Alunos
            builder.Services.AddScoped<IAlunoRepository, AlunoRepository>();
            builder.Services.AddScoped<GestaoAlunoContext>();
            

            //Mediator
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(
            typeof(Program).Assembly,
            typeof(CursoCommandHandler).Assembly,
            typeof(AdicionarCursoCommand).Assembly,
            typeof(AtualizarCursoCommand).Assembly,
            typeof(RemoverCursoCommand).Assembly,
            typeof(AlunoCommandHandler).Assembly,
            typeof(AdicionarAlunoCommand).Assembly));

            return builder;
        }
    }
}
