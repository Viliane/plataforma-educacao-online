using Microsoft.EntityFrameworkCore;
using PlataformaEducacaoOnline.Api.Data;
using PlataformaEducacaoOnline.GestaoConteudo.Data;
using System;

namespace PlataformaEducacaoOnline.Api.Configurations
{
    public static class DbContextConfiguration
    {
        public static WebApplicationBuilder AddDbContextConfiguration(this WebApplicationBuilder builder)
        {
            if (builder.Environment.IsDevelopment())
            {
                builder.Services.AddDbContext<AppDbContext>(opt =>
                {
                    opt.UseSqlite(builder.Configuration.GetConnectionString("Sqlite"));
                });

                builder.Services.AddDbContext<GestaoConteudoContext>(opt =>
                {
                    opt.UseSqlite(builder.Configuration.GetConnectionString("Sqlite"));
                });
            }
            else
            {
                builder.Services.AddDbContext<AppDbContext>(opt =>
                {
                    opt.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
                });

                builder.Services.AddDbContext<GestaoConteudoContext>(opt =>
                {
                    opt.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
                });
            }
            return builder;
        }
    }
}
