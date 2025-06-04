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
    public class AulaMapping : IEntityTypeConfiguration<Aula>
    {
        public void Configure(EntityTypeBuilder<Aula> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Titulo)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(c => c.Conteudo)
                .IsRequired()
                .HasColumnType("varchar(500)");

            builder.HasMany(c => c.Materiais)
                .WithOne(m => m.Aula)
                .HasForeignKey(m => m.AulaId);

            builder.ToTable("Aulas");
        }
    }
}
