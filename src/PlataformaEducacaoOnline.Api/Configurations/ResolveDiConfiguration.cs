using MediatR;
using PlataformaEducacaoOnline.Api.Extensions;
using PlataformaEducacaoOnline.Api.Interfaces;
using PlataformaEducacaoOnline.Core.Bus;
using PlataformaEducacaoOnline.Core.DomainObjects;
using PlataformaEducacaoOnline.Core.Messages.CommonMessagens.IntegrationEvent;
using PlataformaEducacaoOnline.GestaoAluno.Application.Commands;
using PlataformaEducacaoOnline.GestaoAluno.Application.Queries;
using PlataformaEducacaoOnline.GestaoAluno.Data;
using PlataformaEducacaoOnline.GestaoAluno.Data.Repository;
using PlataformaEducacaoOnline.GestaoAluno.Domain;
using PlataformaEducacaoOnline.GestaoConteudo.Application.Commands;
using PlataformaEducacaoOnline.GestaoConteudo.Application.Queries;
using PlataformaEducacaoOnline.GestaoConteudo.Data;
using PlataformaEducacaoOnline.GestaoConteudo.Data.Repository;
using PlataformaEducacaoOnline.GestaoConteudo.Domain;
using PlataformaEducacaoOnline.PagamentoFaturamento.AntiCorruption;
using PlataformaEducacaoOnline.PagamentoFaturamento.Business;
using PlataformaEducacaoOnline.PagamentoFaturamento.Business.Events;
using PlataformaEducacaoOnline.PagamentoFaturamento.Data;
using PlataformaEducacaoOnline.PagamentoFaturamento.Data.Repository;

namespace PlataformaEducacaoOnline.Api.Configurations
{
    public static class ResolveDiConfiguration
    {
        public static WebApplicationBuilder AddResolveDependencie(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IAppIdentityUser, AppIdentityUser>();
            builder.Services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();
            builder.Services.AddScoped<IMediatrHandler, MediatrHandler>();
            //builder.Services.AddScoped<INotificationHandler<CursoAdicionadoEvent>, CursoAdicionadoEventHandler>();

            // Gestão de Conteúdos
            builder.Services.AddScoped<ICursoRepositoty, CursoRepository>();
            builder.Services.AddScoped<ICursoQueries, CursoQueries>();
            builder.Services.AddScoped<GestaoConteudoContext>();

            //Gestão de Alunos
            builder.Services.AddScoped<IAlunoRepository, AlunoRepository>();
            builder.Services.AddScoped<IAlunoQueries, AlunoQueries>();
            builder.Services.AddScoped<GestaoAlunoContext>();

            //Gestão de Pagamento
            builder.Services.AddScoped<IPagamentoRepository, PagamentoRepository>();
            builder.Services.AddScoped<IPagamentoServices, PagamentoService>();
            builder.Services.AddScoped<IPagamentoCartaoCreditoFacade, PagamentoCartaoCreditoFacade>();
            builder.Services.AddScoped<IPayPalGateway, PayPalGateway>();
            builder.Services.AddScoped<PagamentoFaturamento.AntiCorruption.IConfigurationManager, PagamentoFaturamento.AntiCorruption.ConfigurationManager>();
            builder.Services.AddScoped<PagamentoContext>();
            
            builder.Services.AddScoped<INotificationHandler<PedidoMatriculaConfirmadoEvent>, PagamentoEventHandler>();

            //Mediator
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(
            typeof(Program).Assembly,
            typeof(CursoCommandHandler).Assembly,
            typeof(AdicionarCursoCommand).Assembly,
            typeof(AtualizarCursoCommand).Assembly,
            typeof(RemoverCursoCommand).Assembly,
            typeof(AlunoCommandHandler).Assembly,
            typeof(AdicionarAlunoCommand).Assembly,
            typeof(AdicionarMatriculaCommand).Assembly));

            return builder;
        }
    }
}
