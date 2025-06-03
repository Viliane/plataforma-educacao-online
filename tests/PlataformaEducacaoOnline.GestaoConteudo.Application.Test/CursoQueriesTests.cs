using Moq;
using Moq.AutoMock;
using PlataformaEducacaoOnline.GestaoConteudo.Application.Queries;
using PlataformaEducacaoOnline.GestaoConteudo.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaEducacaoOnline.GestaoConteudo.Application.Test
{
    public class CursoQueriesTests
    {
        [Fact(DisplayName = "Obter curso por Id retornando dados")]
        [Trait("Categoria", "Gestão Conteudo - Curso Queries")]
        public async Task ObterCursoPorId_CursoExistente_DeveRetornarCursoComDados()
        {
            // Arrange
            var cursoId = Guid.NewGuid();
            var mocker = new AutoMocker();
            var cursoQueries = mocker.CreateInstance<CursoQueries>();
            mocker.GetMock<ICursoRepositoty>()
                .Setup(repo => repo.ObterCursoPorId(cursoId))
                .ReturnsAsync(new Curso("Curso de Teste", cursoId, new ConteudoProgramatico("Conteúdo Programático"), 100));
            // Act
            var result = await cursoQueries.ObterCursoPorId(cursoId);
            // Assert
            Assert.NotNull(result);
            Assert.Equal(cursoId, result.Id);
            Assert.Equal("Curso de Teste", result.Nome);
            Assert.Equal("Conteúdo Programático", result.Conteudo);
        }

        [Fact(DisplayName = "Obter curso por Id não retornando dados")]
        [Trait("Categoria", "Gestão Conteudo - Curso Queries")]
        public async Task ObterCursoPorId_CursoNaoExistente_DeveRetornarNulo()
        {
            // Arrange
            var cursoId = Guid.NewGuid();
            var mocker = new AutoMocker();
            var cursoQueries = mocker.CreateInstance<CursoQueries>();
            mocker.GetMock<ICursoRepositoty>()
                .Setup(repo => repo.ObterCursoPorId(cursoId))
                .ReturnsAsync((Curso?)null);
            // Act
            var result = await cursoQueries.ObterCursoPorId(cursoId);
            // Assert
            Assert.Null(result);
        }

        [Fact(DisplayName = "Obter todos os cursos retornando dados")]
        [Trait("Categoria", "Gestão Conteudo - Curso Queries")]
        public async Task ObterTodosCursos_DeveRetornarListaDeCursosComDados()
        {
            // Arrange
            var mocker = new AutoMocker();
            var cursoQueries = mocker.CreateInstance<CursoQueries>();
            var cursos = new List<Curso>
            {
                new Curso("Curso 1", Guid.NewGuid(), new ConteudoProgramatico("Conteúdo 1"), 100),
                new Curso("Curso 2", Guid.NewGuid(), new ConteudoProgramatico("Conteúdo 2"), 200)
            };
            mocker.GetMock<ICursoRepositoty>()
                .Setup(repo => repo.ObterTodosCursos())
                .ReturnsAsync(cursos);
            // Act
            var result = await cursoQueries.ObterTodosCursos();
            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }
    }
}
