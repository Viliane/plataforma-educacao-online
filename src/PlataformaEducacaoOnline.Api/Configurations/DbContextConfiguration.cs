using Microsoft.EntityFrameworkCore;
using PlataformaEducacaoOnline.Api.Data;
using PlataformaEducacaoOnline.GestaoAluno.Data;
using PlataformaEducacaoOnline.GestaoConteudo.Data;
using PlataformaEducacaoOnline.PagamentoFaturamento.Data;
using System;

namespace PlataformaEducacaoOnline.Api.Configurations
{
    public static class DbContextConfiguration
    {
        public static WebApplicationBuilder AddDbContextConfiguration(this WebApplicationBuilder builder)
        {
            if (builder.Environment.IsProduction())
            {
                builder.Services.AddDbContext<AppDbContext>(opt =>
                {
                    opt.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
                });

                builder.Services.AddDbContext<GestaoConteudoContext>(opt =>
                {
                    opt.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
                });

                builder.Services.AddDbContext<GestaoAlunoContext>(opt =>
                {
                    opt.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
                });

                builder.Services.AddDbContext<PagamentoContext>(opt =>
                {
                    opt.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
                });
            }
            else
            {
                builder.Services.AddDbContext<AppDbContext>(opt =>
                {
                    opt.UseSqlite(builder.Configuration.GetConnectionString("Sqlite"));
                });

                builder.Services.AddDbContext<GestaoConteudoContext>(opt =>
                {
                    opt.UseSqlite(builder.Configuration.GetConnectionString("Sqlite"));
                });

                builder.Services.AddDbContext<GestaoAlunoContext>(opt =>
                {
                    opt.UseSqlite(builder.Configuration.GetConnectionString("Sqlite"));
                });

                builder.Services.AddDbContext<PagamentoContext>(opt =>
                {
                    opt.UseSqlite(builder.Configuration.GetConnectionString("Sqlite"));
                });
            }
            return builder;
        }
    }
}
