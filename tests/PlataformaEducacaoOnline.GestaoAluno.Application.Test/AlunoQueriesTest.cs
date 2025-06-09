using Moq;
using Moq.AutoMock;
using PlataformaEducacaoOnline.GestaoAluno.Application.Queries;
using PlataformaEducacaoOnline.GestaoAluno.Application.Queries.DTO;
using PlataformaEducacaoOnline.GestaoAluno.Data.Repository;
using PlataformaEducacaoOnline.GestaoAluno.Domain;

namespace PlataformaEducacaoOnline.GestaoAluno.Application.Test
{
    public class AlunoQueriesTest
    {
        [Fact(DisplayName = "Obter Matricula por AlunoId e CursoId com dados")]
        [Trait("Categoria", "Gestão Conteudo - Aluno Queries")]
        public async Task ObterMatriculaAlunoIdCursoId_DeveRetornarMatriculaDto_QuandoExistirMatricula()
        {
            // Arrange
            var mocker = new AutoMocker();
            var alunoId = Guid.NewGuid();
            var cursoId = Guid.NewGuid();
            var matriculaEsperada = new Matricula(cursoId, alunoId);

            var mockRepository = mocker.GetMock<IAlunoRepository>();
            mockRepository
                .Setup(r => r.ObterMatriculaPorAlunoIdCursoId(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .ReturnsAsync(matriculaEsperada);

            var queries = mocker.CreateInstance<AlunoQueries>();

            // Act
            var resultado = await queries.ObterMatriculaAlunoIdCursoId(alunoId, cursoId);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(matriculaEsperada.AlunoId, resultado.AlunoId);
            Assert.Equal(matriculaEsperada.CursoId, resultado.CursoId);
        }

        [Fact(DisplayName = "Obter Matricula por AlunoId e CursoId sem dados")]
        [Trait("Categoria", "Gestão Conteudo - Aluno Queries")]
        public async Task ObterMatriculaAlunoIdCursoId_DeveRetornarNull_QuandoNaoExistirMatricula()
        {
            // Arrange
            var mocker = new AutoMocker();
            var alunoId = Guid.NewGuid();
            var cursoId = Guid.NewGuid();

            var mockRepository = mocker.GetMock<IAlunoRepository>();
            mockRepository
                .Setup(r => r.ObterMatriculaPorAlunoIdCursoId(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .ReturnsAsync((Matricula?)null);

            var queries = mocker.CreateInstance<AlunoQueries>();

            // Act
            var resultado = await queries.ObterMatriculaAlunoIdCursoId(alunoId, cursoId);

            // Assert
            Assert.Null(resultado);
        }

        [Fact(DisplayName = "Obter Certificado")]
        [Trait("Categoria", "Gestão Conteudo - Aluno Queries")]
        public async Task ObterCertificado_DeveRetornarCertificadoPdf_QuandoExistirCertificado()
        {
            // Arrange
            var mocker = new AutoMocker();
            var matriculaId = Guid.NewGuid();
            var usuarioId = Guid.NewGuid();
            var certificadoEsperado = new Certificado(matriculaId, usuarioId);
            var mockRepository = mocker.GetMock<IAlunoRepository>();
            mockRepository
                .Setup(r => r.ObterCertificadoPorId(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .ReturnsAsync(certificadoEsperado);
            var mockPdfGenerator = mocker.GetMock<ICertificadoPdfGenerator>();

            mockPdfGenerator.Setup(g => g.GerarPdf(It.IsAny<Certificado>()))
                .Returns(new byte[] { 1, 2, 3 });

            var queries = mocker.CreateInstance<AlunoQueries>();

            // Act
            var resultado = await queries.ObterCertificado(matriculaId, usuarioId);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(new byte[] { 1, 2, 3 }, resultado);
        }
    }
}
