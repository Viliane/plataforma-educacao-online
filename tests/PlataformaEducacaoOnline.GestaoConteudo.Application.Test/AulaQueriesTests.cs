using Moq;
using Moq.AutoMock;
using PlataformaEducacaoOnline.GestaoConteudo.Application.Queries;
using PlataformaEducacaoOnline.GestaoConteudo.Domain;

namespace PlataformaEducacaoOnline.GestaoConteudo.Application.Test
{
    public class AulaQueriesTests
    {
        [Fact(DisplayName = "Obter aula por Id retornando dados")]
        [Trait("Categoria", "Gestão Conteudo - Aula Queries")]
        public async Task ObterAulaPorId_AulaExistente_DeveRetornarAulaComDados()
        {
            // Arrange
            var aula = new Aula("Curso de Teste", "Conteúdo Programático", Guid.NewGuid());

            var mocker = new AutoMocker();
            var aulaQueries = mocker.CreateInstance<CursoQueries>();

            mocker.GetMock<ICursoRepositoty>()
                .Setup(repo => repo.ObterAulaPorId(aula.Id))
                .ReturnsAsync(aula);

            // Act
            var result = await aulaQueries.ObterAulaPorId(aula.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(aula.Id, result.Id);
            Assert.Equal("Curso de Teste", result.Titulo);
            Assert.Equal("Conteúdo Programático", result.Conteudo);
        }

        [Fact(DisplayName = "Obter aula por Id não retornando dados")]
        [Trait("Categoria", "Gestão Conteudo - Aula Queries")]
        public async Task ObterAulaPorId_AulaNaoExistente_DeveRetornarNulo()
        {
            // Arrange
            var aulaId = Guid.NewGuid();
            var mocker = new AutoMocker();
            var cursoQueries = mocker.CreateInstance<CursoQueries>();
            mocker.GetMock<ICursoRepositoty>()
                .Setup(repo => repo.ObterAulaPorId(aulaId))
                .ReturnsAsync((Aula?)null);
            // Act
            var result = await cursoQueries.ObterAulaPorId(aulaId);
            // Assert
            Assert.Null(result);
        }

        [Fact(DisplayName = "Obter todos as aulas retornando dados")]
        [Trait("Categoria", "Gestão Conteudo - Aula Queries")]
        public async Task ObterTodasAulas_DeveRetornarListaDeAulasComDados()
        {
            // Arrange
            var mocker = new AutoMocker();
            var aulaQueries = mocker.CreateInstance<CursoQueries>();
            var aulas = new List<Aula>
            {
                new Aula("Aula 1", "Conteúdo 1", Guid.NewGuid()),
                new Aula("Aula 2", "Conteúdo 2", Guid.NewGuid())
            };

            List<Material> materias = new List<Material>
            {
                new Material("Material 1"),
                new Material("Material 2")
            };

            foreach (var item in aulas)
            {
                foreach (var material in materias)
                {
                    item.AdicionarMaterial(material);
                }
            }

            mocker.GetMock<ICursoRepositoty>()
                .Setup(repo => repo.ObterTodasAulas())
                .ReturnsAsync(aulas);

            foreach (var aula in aulas)
            {
                mocker.GetMock<ICursoRepositoty>()
                    .Setup(repo => repo.ObterMateriaisPorAulaId(aula.Id))
                    .ReturnsAsync(materias);
            }
            
            // Act
            var result = await aulaQueries.ObterTodasAulas();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }
    }
}
