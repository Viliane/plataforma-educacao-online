using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlataformaEducacaoOnline.GestaoAluno.Domain;

namespace PlataformaEducacaoOnline.GestaoAluno.Data.Mappings
{
    public class MatriculaMapping : IEntityTypeConfiguration<Matricula>
    {
        public void Configure(EntityTypeBuilder<Matricula> builder)
        {
            builder.HasKey(m => m.Id);

            builder.Property(m => m.CursoId)
                .IsRequired();

            builder.Property(m => m.AlunoId)
                .IsRequired();

            builder.Property(m => m.DataMatricula)
                .IsRequired()
                .HasColumnType("datetime");

            builder.Property(m => m.StatusMatricula)
                .IsRequired()
                .HasConversion<int>();

            builder.HasOne(m => m.Certificado)
                    .WithOne(c => c.Matricula)
                    .HasForeignKey<Certificado>(c => c.MatriculaId);

            builder.ToTable("Matriculas");
        }
    }
}
