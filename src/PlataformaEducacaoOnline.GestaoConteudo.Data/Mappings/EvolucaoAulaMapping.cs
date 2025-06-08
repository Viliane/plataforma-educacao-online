using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlataformaEducacaoOnline.GestaoConteudo.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaEducacaoOnline.GestaoConteudo.Data.Mappings
{
    public class EvolucaoAulaMapping : IEntityTypeConfiguration<EvolucaoAula>
    {
        public void Configure(EntityTypeBuilder<EvolucaoAula> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.AulaId)
                .IsRequired()
                .HasColumnName("AulaId");

            builder.Property(e => e.UsuarioId)
                .IsRequired()
                .HasColumnName("UsuarioId");

            builder.Property(e=> e.Status)
                .IsRequired()
                .HasConversion<int>()
                .HasColumnName("Status");

            builder.ToTable("EvolucaoAulas");
        }
    }
}
