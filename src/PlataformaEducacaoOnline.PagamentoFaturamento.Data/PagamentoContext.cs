using MediatR;
using Microsoft.EntityFrameworkCore;
using PlataformaEducacaoOnline.Core.Data;
using PlataformaEducacaoOnline.Core.Data.Extension;
using PlataformaEducacaoOnline.Core.DomainObjects;
using PlataformaEducacaoOnline.Core.Messages;
using PlataformaEducacaoOnline.PagamentoFaturamento.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaEducacaoOnline.PagamentoFaturamento.Data
{
    public class PagamentoContext : DbContext, IUnitOfWork
    {
        private readonly IMediator _mediator;

        public PagamentoContext(DbContextOptions<PagamentoContext> options, 
            IMediator mediator) : base(options)
        {
            _mediator = mediator;
        }

        public DbSet<Pagamento> Pagamentos { get; set; }
        public DbSet<Transacao> Transacoes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                     .SelectMany(e => e.GetProperties()
                         .Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(200)");

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PagamentoContext).Assembly);

            modelBuilder.Ignore<Event>();

            base.OnModelCreating(modelBuilder);
        }

        public async Task<bool> Commit()
        {
            var sucesso = await base.SaveChangesAsync() > 0;
            if (sucesso) await _mediator.PublicarEventos(this);

            return sucesso;
        }
    }    
}
