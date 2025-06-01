using MediatR;
using Microsoft.EntityFrameworkCore;
using PlataformaEducacaoOnline.Core.Data;
using PlataformaEducacaoOnline.Core.DomainObjects;
using PlataformaEducacaoOnline.Core.Messages;
using PlataformaEducacaoOnline.GestaoConteudo.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaEducacaoOnline.GestaoConteudo.Data
{
    public class GestaoConteudoContext : DbContext, IUnitOfWork
    {
        private readonly IMediator _mediator;

        public GestaoConteudoContext(DbContextOptions<GestaoConteudoContext> options, IMediator mediator) : base(options)
        {
            _mediator = mediator;
        }

        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Aula> Aulas { get; set; }

        public DbSet<Material> Materiais { get; set; }

        public async Task<bool> Commit()
        {
            var sucesso = await base.SaveChangesAsync() > 0;
            if (sucesso) await _mediator.PublicarEventos(this);

            return sucesso;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                     .SelectMany(e => e.GetProperties()
                         .Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(200)");
            
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(GestaoConteudoContext).Assembly);

            modelBuilder.Ignore<Event>();

            base.OnModelCreating(modelBuilder);
        }
    }

    public static class MediatorExtension
    {
        public static async Task PublicarEventos(this IMediator mediator, DbContext context)
        {
            var domainEntities = context.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.Notificacoes != null && x.Entity.Notificacoes.Any())
                .Select(x => x.Entity)
                .ToList();

            var domainEvents = domainEntities.SelectMany(x => x.Notificacoes).ToList();

            domainEntities.ForEach(entity => entity.LimparEventos());

            var tasks = domainEvents.Select(async (domainEvent) =>
            {
                await mediator.Publish(domainEvent);
            });

            await Task.WhenAll(tasks);
        }
    }
}
