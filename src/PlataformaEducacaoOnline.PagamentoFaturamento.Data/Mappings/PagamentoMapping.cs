using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlataformaEducacaoOnline.PagamentoFaturamento.Business;

namespace PlataformaEducacaoOnline.PagamentoFaturamento.Data.Mappings
{
    public class PagamentoMapping : IEntityTypeConfiguration<Pagamento>
    {
        public void Configure(EntityTypeBuilder<Pagamento> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.MatriculaId)
                .IsRequired();

            builder.Property(p => p.CursoId)
                .IsRequired();

            builder.Property(p => p.AlunoId)
                .IsRequired();

            builder.OwnsOne(p => p.DadosCartao, dc =>
            {
                dc.Property(d => d.NomeCartao)
                    .IsRequired()
                    .HasColumnType("varchar(250)");

                dc.Property(d => d.NumeroCartao)
                    .IsRequired()
                    .HasColumnType("varchar(16)");

                dc.Property(d => d.ExpiracaoCartao)
                    .IsRequired()
                    .HasColumnType("varchar(10)");

                dc.Property(d => d.CodigoSegurancaCartao)
                    .IsRequired()
                    .HasColumnType("varchar(4)");
            });

            // 1 : 1 => Pagamento : Transacao
            builder.HasOne(c => c.Transacao)
                .WithOne(c => c.Pagamento);

            builder.ToTable("Pagamentos");
        }
    }
}
