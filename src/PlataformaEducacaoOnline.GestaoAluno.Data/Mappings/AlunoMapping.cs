using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlataformaEducacaoOnline.GestaoAluno.Domain;

namespace PlataformaEducacaoOnline.GestaoAluno.Data.Mappings
{
    public class AlunoMapping : IEntityTypeConfiguration<Aluno>
    {
        public void Configure(EntityTypeBuilder<Aluno> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Nome)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.OwnsOne(a => a.HistoricoAprendizado, a=>
            {
                a.Property(h => h.Descricao)
                    .IsRequired(false)
                    .HasColumnType("varchar(500)");
                a.Property(h => h.DataRegistro)
                    .IsRequired(false)
                    .HasColumnType("datetime");
            });

            // 1: N => Aluno : Matriculas
            builder.HasMany(a => a.Matriculas)
                .WithOne(m => m.Aluno)
                .HasForeignKey(m => m.AlunoId);

            builder.HasOne(a => a.Certificado)
                    .WithOne(c => c.Aluno)
                    .HasForeignKey<Certificado>(c => c.AlunoId);

            builder.ToTable("Alunos");
        }
    }
}
