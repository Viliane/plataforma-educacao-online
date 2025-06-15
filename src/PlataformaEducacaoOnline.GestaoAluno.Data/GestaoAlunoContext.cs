using MediatR;
using Microsoft.EntityFrameworkCore;
using PlataformaEducacaoOnline.Core.Data;
using PlataformaEducacaoOnline.Core.Data.Extension;
using PlataformaEducacaoOnline.Core.Messages;
using PlataformaEducacaoOnline.GestaoAluno.Domain;

namespace PlataformaEducacaoOnline.GestaoAluno.Data
{
    public class GestaoAlunoContext : DbContext, IUnitOfWork
    {
        private readonly IMediator _mediator;

        public GestaoAlunoContext(DbContextOptions<GestaoAlunoContext> options, IMediator mediator) : base(options)
        {
            _mediator = mediator;
        }

        public DbSet<Aluno> Alunos { get; set; }

        public DbSet<Matricula> Matriculas { get; set; }

        public DbSet<Certificado> Certificados { get; set; }

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

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(GestaoAlunoContext).Assembly);

            modelBuilder.Ignore<Event>();

            base.OnModelCreating(modelBuilder);
        }
    }
}
