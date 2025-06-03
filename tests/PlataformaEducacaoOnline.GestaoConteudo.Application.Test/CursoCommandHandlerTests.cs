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
            var cursoCommand = new AdicionarCursoCommand("Curso de Teste", usuarioId, "Conteúdo Programático de Teste", 200);

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

        [Fact(DisplayName = "Curso Não Encontrado na atualizar")]
        [Trait("Categoria", "Gestão Conteudo - Curso Command Handler")]
        public async Task AtualizarCurso_CursoNaoEncontrado_NaoDeveAlterarComSucesso()
        {
            // Arrange             
            var cursoCommand = new AtualizarCursoCommand("Curso de Teste 1", Guid.NewGuid(), "Conteúdo Programático de Teste 1", 300);

            var mocker = new AutoMocker();
            var cursoHandler = mocker.CreateInstance<CursoCommandHandler>();

            mocker.GetMock<ICursoRepositoty>()
                .Setup(repo => repo.ObterCursoPorId(cursoCommand.CursoId)).Returns(Task.FromResult<Curso?>(null));

            // Act  
            var result = await cursoHandler.Handle(cursoCommand, CancellationToken.None);

            // Assert  
            Assert.False(result);
            mocker.GetMock<ICursoRepositoty>().Verify(repo => repo.Atualizar(It.IsAny<Curso>()), Times.Never);
            mocker.GetMock<ICursoRepositoty>().Verify(repo => repo.UnitOfWork.Commit(), Times.Never);
        }

        [Fact(DisplayName = "Atualizar o curso com sucesso")]
        [Trait("Categoria", "Gestão Conteudo - Curso Command Handler")]
        public async Task AtualizarCurso_CursoEncontrado_DeveAlterarComSucesso()
        {
            var curso = new Curso("Curso de Teste ", Guid.NewGuid(), new ConteudoProgramatico("Conteúdo Programático 1"), 200);
            // Arrange             
            var cursoCommand = new AtualizarCursoCommand("Curso de Teste 1", curso.Id, "Conteúdo Programático de Teste 1", 300);

            var mocker = new AutoMocker();
            var cursoHandler = mocker.CreateInstance<CursoCommandHandler>();

            mocker.GetMock<ICursoRepositoty>()
                .Setup(repo => repo.ObterCursoPorId(cursoCommand.CursoId)).ReturnsAsync(curso);

            mocker.GetMock<ICursoRepositoty>()
                .Setup(repo => repo.UnitOfWork.Commit()).Returns(Task.FromResult(true));

            // Act  
            var result = await cursoHandler.Handle(cursoCommand, CancellationToken.None);

            // Assert  
            Assert.True(result);
            mocker.GetMock<ICursoRepositoty>().Verify(repo => repo.Atualizar(It.IsAny<Curso>()), Times.Once);
            mocker.GetMock<ICursoRepositoty>().Verify(repo => repo.UnitOfWork.Commit(), Times.Once);
        }

        [Fact(DisplayName = "Curso Não Encontrado na remoção")]
        [Trait("Categoria", "Gestão Conteudo - Curso Command Handler")]
        public async Task RemoverCurso_CursoNaoEncontrado_NaoDeveRemoverComSucesso()
        {
            // Arrange             
            var cursoCommand = new RemoverCursoCommand(Guid.NewGuid());

            var mocker = new AutoMocker();
            var cursoHandler = mocker.CreateInstance<CursoCommandHandler>();

            mocker.GetMock<ICursoRepositoty>()
                .Setup(repo => repo.ObterCursoPorId(cursoCommand.CursoId)).Returns(Task.FromResult<Curso?>(null));

            // Act  
            var result = await cursoHandler.Handle(cursoCommand, CancellationToken.None);

            // Assert  
            Assert.False(result);
            mocker.GetMock<ICursoRepositoty>().Verify(repo => repo.Remover(It.IsAny<Curso>()), Times.Never);
            mocker.GetMock<ICursoRepositoty>().Verify(repo => repo.UnitOfWork.Commit(), Times.Never);
        }

        [Fact(DisplayName = "Aula vinculada ao curso na remoção")]
        [Trait("Categoria", "Gestão Conteudo - Curso Command Handler")]
        public async Task RemoverCurso_AulaVinculadaAoCurso_NaoDeveRemoverComSucesso()
        {
            // Arrange
            var curso = new Curso("Curso de Teste ", Guid.NewGuid(), new ConteudoProgramatico("Conteúdo Programático 1"), 200);
            
            List<Aula> aulas = new List<Aula>
            {
                new Aula("Aula 1", "Descrição da Aula 1", curso.Id),
                new Aula("Aula 2", "Descrição da Aula 2", curso.Id)
            };
            
            var cursoCommand = new RemoverCursoCommand(curso.Id);

            var mocker = new AutoMocker();
            var cursoHandler = mocker.CreateInstance<CursoCommandHandler>();

            mocker.GetMock<ICursoRepositoty>()
                .Setup(repo => repo.ObterCursoPorId(cursoCommand.CursoId)).ReturnsAsync(curso);

            mocker.GetMock<ICursoRepositoty>()
                .Setup(repo => repo.ObterAulasPorCursoId(cursoCommand.CursoId)).ReturnsAsync(aulas);

            // Act  
            var result = await cursoHandler.Handle(cursoCommand, CancellationToken.None);

            // Assert  
            Assert.False(result);
            mocker.GetMock<ICursoRepositoty>().Verify(repo => repo.Remover(It.IsAny<Curso>()), Times.Never);
            mocker.GetMock<ICursoRepositoty>().Verify(repo => repo.UnitOfWork.Commit(), Times.Never);
        }

        [Fact(DisplayName = "Remover o curso com sucesso")]
        [Trait("Categoria", "Gestão Conteudo - Curso Command Handler")]
        public async Task RemoverCurso_CursoEncontrado_DeveRemoverComSucesso()
        {
            var curso = new Curso("Curso de Teste ", Guid.NewGuid(), new ConteudoProgramatico("Conteúdo Programático 1"), 200);
            // Arrange             
            var cursoCommand = new RemoverCursoCommand(curso.Id);

            var mocker = new AutoMocker();
            var cursoHandler = mocker.CreateInstance<CursoCommandHandler>();

            mocker.GetMock<ICursoRepositoty>()
                .Setup(repo => repo.ObterCursoPorId(cursoCommand.CursoId)).ReturnsAsync(curso);

            mocker.GetMock<ICursoRepositoty>()
                .Setup(repo => repo.UnitOfWork.Commit()).Returns(Task.FromResult(true));

            // Act  
            var result = await cursoHandler.Handle(cursoCommand, CancellationToken.None);

            // Assert  
            Assert.True(result);
            mocker.GetMock<ICursoRepositoty>().Verify(repo => repo.Remover(It.IsAny<Curso>()), Times.Once);
            mocker.GetMock<ICursoRepositoty>().Verify(repo => repo.UnitOfWork.Commit(), Times.Once);
        }
    }
}
