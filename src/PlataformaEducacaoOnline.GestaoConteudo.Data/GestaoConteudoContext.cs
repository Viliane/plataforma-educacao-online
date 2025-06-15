using MediatR;
using Microsoft.EntityFrameworkCore;
using PlataformaEducacaoOnline.Core.Data;
using PlataformaEducacaoOnline.Core.Data.Extension;
using PlataformaEducacaoOnline.Core.Messages;
using PlataformaEducacaoOnline.GestaoConteudo.Domain;

namespace PlataformaEducacaoOnline.GestaoConteudo.Data
{
    public class GestaoConteudoContext : DbContext, IUnitOfWork
    {
        private readonly IMediator _mediator;

        public GestaoConteudoContext(DbContextOptions<GestaoConteudoContext> options, 
            IMediator mediator) : base(options)
        {
            _mediator = mediator;
        }

        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Aula> Aulas { get; set; }

        public DbSet<EvolucaoAula> EvolucaoAulas { get; set; }

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
}
