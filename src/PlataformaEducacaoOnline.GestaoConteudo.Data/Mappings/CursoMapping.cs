using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlataformaEducacaoOnline.GestaoConteudo.Domain;
using System;

namespace PlataformaEducacaoOnline.GestaoConteudo.Data.Mappings
{
    public class CursoMapping : IEntityTypeConfiguration<Curso>
    {
        public void Configure(EntityTypeBuilder<Curso> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nome)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.OwnsOne(c=> c.ConteudoProgramatico, cp =>
            {
                cp.Property(c => c.DescricaoConteudoProgramatico)
                    .IsRequired()
                    .HasColumnType("varchar(500)");

                cp.Property(c => c.DataAtualizacaoConteudoProgramatico)
                    .IsRequired()
                    .HasColumnType("datetime");
            });

            // 1 : N =>  Curso : Aulas
            builder.HasMany(c => c.Aulas)
                .WithOne(a => a.Curso)
                .HasForeignKey(a => a.CursoId);

            builder.ToTable("Cursos");
        }
    }
}
