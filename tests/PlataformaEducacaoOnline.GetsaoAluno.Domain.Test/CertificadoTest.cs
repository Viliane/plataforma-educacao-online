using Moq;
using PlataformaEducacaoOnline.Core.DomainObjects;
using PlataformaEducacaoOnline.GestaoAluno.Domain;

namespace PlataformaEducacaoOnline.GetsaoAluno.Domain.Test
{
    public class CertificadoTest
    {
        [Fact(DisplayName = "Adicionar Novo Certificado dados Inválidos")]
        [Trait("Categoria", "Certificado Testes")]
        public void Validar_AdicionarNovoCertificado_DeveRetornarDomainException()
        {
            // AAA
            Assert.Throws<DomainException>(() => new Certificado(Guid.Empty, Guid.Empty));
        }

        [Fact(DisplayName = "Adicionar Novo Certificado dados Inválidos")]
        [Trait("Categoria", "Certificado Testes")]
        public void Validar_AdicionarNovoCertificado_DeveDadosComSucesso()
        {
            var certificado = new Certificado(Guid.NewGuid(), Guid.NewGuid());

            //Assert            
            Assert.NotNull(certificado);
        }

        [Fact(DisplayName = "Emitir Certificado com Sucesso")]
        [Trait("Categoria", "Certificado Testes")]
        public void EmitirCertificado_DeveEmitirComSucesso()
        {
            // Arrange
            var pdfGeneratorMock = new Mock<ICertificadoPdfGenerator>();
            var alunoId = Guid.NewGuid();
            var matriculaId = Guid.NewGuid();
            var certificado = new Certificado(matriculaId, alunoId);
            var expectedPdf = new byte[] { 1, 2, 3, 4 };

            pdfGeneratorMock.Setup(x => x.GerarPdf(certificado)).Returns(expectedPdf);

            // Act  
            var pdf = certificado.EmitirCertificado(pdfGeneratorMock.Object);

            // Assert  
            Assert.NotNull(pdf);
            Assert.Equal(expectedPdf, pdf);
            pdfGeneratorMock.Verify(x => x.GerarPdf(certificado), Times.Once);
        }
    }
}
