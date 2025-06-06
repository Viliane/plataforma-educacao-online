using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlataformaEducacaoOnline.GestaoAluno.Domain;
using System.Reflection.Emit;

namespace PlataformaEducacaoOnline.GestaoAluno.Data.Mappings
{
    public class CertificadoMapping : IEntityTypeConfiguration<Certificado>
    {
        public void Configure(EntityTypeBuilder<Certificado> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.DataEmissao).IsRequired();

            builder.ToTable("Certificados");
        }
    }
}
