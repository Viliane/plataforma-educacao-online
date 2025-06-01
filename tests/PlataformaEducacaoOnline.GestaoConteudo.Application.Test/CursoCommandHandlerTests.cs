using MediatR;
using Moq;
using Moq.AutoMock;
using PlataformaEducacaoOnline.GestaoConteudo.Application.Commands;
using PlataformaEducacaoOnline.GestaoConteudo.Domain;

namespace PlataformaEducacaoOnline.GestaoConteudo.Application.Test
{
    public class CursoCommandHandlerTests
    {
        [Fact(DisplayName = "Adicionar Curso com Sucesso")]
        [Trait("Categoria", "Gestão Conteudo - Curso Command Handler")]
        public async Task AdicionarCurso_NovoCurso_DeveAdicionarCursoComSucesso()
        {
            // Arrange
            var usuarioId = Guid.NewGuid();
            var cursoCommand = new AdicionarCursoCommand("Curso de Teste", usuarioId, "Conteúdo Programático de Teste");

            var mocker = new AutoMocker();
            var cursoHandler = mocker.CreateInstance<CursoCommandHandler>();

            mocker.GetMock<ICursoRepositoty>()
                .Setup(repo => repo.UnitOfWork.Commit()).Returns(Task.FromResult(true));

            // Act
            var result = await cursoHandler.Handle(cursoCommand, CancellationToken.None);

            // Assert
            Assert.True(result);
            mocker.GetMock<ICursoRepositoty>().Verify(repo => repo.Adicionar(It.IsAny<Curso>()), Times.Once);
            //mocker.GetMock<IMediator>().Verify(repo => repo.Publish(It.IsAny<INotification>(), It.IsAny<CancellationToken>()), Times.Once);
            mocker.GetMock<ICursoRepositoty>().Verify(repo => repo.UnitOfWork.Commit(), Times.Once);
        }
    }
}
