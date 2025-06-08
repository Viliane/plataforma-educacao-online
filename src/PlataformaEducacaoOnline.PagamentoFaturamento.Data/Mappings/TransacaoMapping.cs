using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlataformaEducacaoOnline.PagamentoFaturamento.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaEducacaoOnline.PagamentoFaturamento.Data.Mappings
{
    public class TransacaoMapping : IEntityTypeConfiguration<Transacao>
    {
        public void Configure(EntityTypeBuilder<Transacao> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(p => p.MatriculaId)
                .IsRequired();

            builder.Property(p => p.CursoId)
                .IsRequired();

            builder.Property(p => p.AlunoId)
                .IsRequired();

            builder.ToTable("Transacoes");
        }
    }
}
