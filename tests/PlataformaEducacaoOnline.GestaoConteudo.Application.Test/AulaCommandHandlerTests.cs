using Moq;
using Moq.AutoMock;
using PlataformaEducacaoOnline.GestaoConteudo.Application.Commands;
using PlataformaEducacaoOnline.GestaoConteudo.Domain;

namespace PlataformaEducacaoOnline.GestaoConteudo.Application.Test
{
    public class AulaCommandHandlerTests
    {
        [Fact(DisplayName = "Adicionar Aula com Sucesso")]
        [Trait("Categoria", "Gestão Conteudo - Aula Command Handler")]
        public async Task AdicionarAula_NovaAula_DeveAdicionarAulaComSucesso()
        {
            // Arrange
            var materiais = new List<Material>
            {
                new Material("Material 1"),
                new Material("Material 2")
            };

            var curso = new Curso("Curso de Teste ", Guid.NewGuid(), new ConteudoProgramatico("Conteúdo Programático 1"), 200);
            var aulaCommand = new AdicionarAulaCommand("Aula de Teste", "Conteúdo Programático de Teste", Guid.NewGuid(), materiais);

            var mocker = new AutoMocker();
            var aulaHandler = mocker.CreateInstance<AulaCommandHandler>();

            mocker.GetMock<ICursoRepositoty>()
              .Setup(repo => repo.ObterCursoPorId(aulaCommand.CursoId)).ReturnsAsync(curso);

            mocker.GetMock<ICursoRepositoty>()
                .Setup(repo => repo.UnitOfWork.Commit()).Returns(Task.FromResult(true));

            // Act
            var result = await aulaHandler.Handle(aulaCommand, CancellationToken.None);

            // Assert
            Assert.True(result);
            mocker.GetMock<ICursoRepositoty>().Verify(repo => repo.Adicionar(It.IsAny<Aula>()), Times.Once);
            mocker.GetMock<ICursoRepositoty>().Verify(repo => repo.UnitOfWork.Commit(), Times.Once);
        }

        [Fact(DisplayName = "Aula Não Encontrado na atualizar")]
        [Trait("Categoria", "Gestão Conteudo - Aula Command Handler")]
        public async Task AtualizarAula_AulaNaoEncontrado_NaoDeveAlterarComSucesso()
        {
            // Arrange             
            var aulaCommand = new AtualizarAulaCommand(Guid.NewGuid(), "Titulo", "Conteudo");

            var mocker = new AutoMocker();
            var aulaHandler = mocker.CreateInstance<AulaCommandHandler>();

            mocker.GetMock<ICursoRepositoty>()
                .Setup(repo => repo.ObterAulaPorId(aulaCommand.AulaId)).Returns(Task.FromResult<Aula?>(null));

            // Act  
            var result = await aulaHandler.Handle(aulaCommand, CancellationToken.None);

            // Assert  
            Assert.False(result);
            mocker.GetMock<ICursoRepositoty>().Verify(repo => repo.Atualizar(It.IsAny<Curso>()), Times.Never);
            mocker.GetMock<ICursoRepositoty>().Verify(repo => repo.UnitOfWork.Commit(), Times.Never);
        }

        [Fact(DisplayName = "Atualizar o aula com sucesso")]
        [Trait("Categoria", "Gestão Conteudo - Aula Command Handler")]
        public async Task AtualizarAula_AulaEncontrada_DeveAlterarComSucesso()
        {
            // Arrange
            var aula = new Aula("Curso de Teste ", "Conteúdo Programático 1", Guid.NewGuid());
            var aulaCommand = new AtualizarAulaCommand(aula.Id, "Titulo", "Conteúdo");

            var mocker = new AutoMocker();
            var aulaHandler = mocker.CreateInstance<AulaCommandHandler>();

            mocker.GetMock<ICursoRepositoty>()
                .Setup(repo => repo.ObterAulaPorId(aulaCommand.AulaId)).ReturnsAsync(aula);

            mocker.GetMock<ICursoRepositoty>()
                .Setup(repo => repo.UnitOfWork.Commit()).Returns(Task.FromResult(true));

            // Act  
            var result = await aulaHandler.Handle(aulaCommand, CancellationToken.None);

            // Assert  
            Assert.True(result);
            mocker.GetMock<ICursoRepositoty>().Verify(repo => repo.Atualizar(It.IsAny<Aula>()), Times.Once);
            mocker.GetMock<ICursoRepositoty>().Verify(repo => repo.UnitOfWork.Commit(), Times.Once);
        }

        [Fact(DisplayName = "Aula Não Encontrado na remoção")]
        [Trait("Categoria", "Gestão Conteudo - Aula Command Handler")]
        public async Task RemoverAula_AulaNaoEncontrado_NaoDeveRemoverComSucesso()
        {
            // Arrange             
            var aulaCommand = new RemoverAulaCommand(Guid.NewGuid());

            var mocker = new AutoMocker();
            var aulaHandler = mocker.CreateInstance<AulaCommandHandler>();

            mocker.GetMock<ICursoRepositoty>()
                .Setup(repo => repo.ObterAulaPorId(aulaCommand.AulaId)).Returns(Task.FromResult<Aula?>(null));

            // Act  
            var result = await aulaHandler.Handle(aulaCommand, CancellationToken.None);

            // Assert  
            Assert.False(result);
            mocker.GetMock<ICursoRepositoty>().Verify(repo => repo.Remover(It.IsAny<Aula>()), Times.Never);
            mocker.GetMock<ICursoRepositoty>().Verify(repo => repo.UnitOfWork.Commit(), Times.Never);
        }

        [Fact(DisplayName = "Material vinculada a aula na remoção")]
        [Trait("Categoria", "Gestão Conteudo - Curso Command Handler")]
        public async Task RemoverAula_MaterialVinculadaAula_NaoDeveRemoverComSucesso()
        {
            // Arrange
            var aula = new Aula("Titulo", "Conteúdo", Guid.NewGuid());

            List<Material> aulas = new List<Material>
            {
                new Material("Material 1"),
                new Material("Material 2")
            };

            var aulaCommand = new RemoverAulaCommand(aula.Id);

            var mocker = new AutoMocker();
            var aulaHandler = mocker.CreateInstance<AulaCommandHandler>();

            mocker.GetMock<ICursoRepositoty>()
                .Setup(repo => repo.ObterAulaPorId(aulaCommand.AulaId)).ReturnsAsync(aula);

            mocker.GetMock<ICursoRepositoty>()
                .Setup(repo => repo.ObterMateriaisPorAulaId(aulaCommand.AulaId)).ReturnsAsync(aulas);

            // Act  
            var result = await aulaHandler.Handle(aulaCommand, CancellationToken.None);

            // Assert  
            Assert.False(result);
            mocker.GetMock<ICursoRepositoty>().Verify(repo => repo.Remover(It.IsAny<Aula>()), Times.Never);
            mocker.GetMock<ICursoRepositoty>().Verify(repo => repo.UnitOfWork.Commit(), Times.Never);
        }

        [Fact(DisplayName = "Remover aula com sucesso")]
        [Trait("Categoria", "Gestão Conteudo - Aula Command Handler")]
        public async Task RemoverAula_AulaEncontrada_DeveRemoverComSucesso()
        {
            // Arrange
            var aula = new Aula("Titulo", "Conteúdo", Guid.NewGuid());
            var aulaCommand = new RemoverAulaCommand(aula.Id);

            var mocker = new AutoMocker();
            var aulaHandler = mocker.CreateInstance<AulaCommandHandler>();

            mocker.GetMock<ICursoRepositoty>()
                .Setup(repo => repo.ObterAulaPorId(aulaCommand.AulaId)).ReturnsAsync(aula);

            mocker.GetMock<ICursoRepositoty>()
                .Setup(repo => repo.UnitOfWork.Commit()).Returns(Task.FromResult(true));

            // Act  
            var result = await aulaHandler.Handle(aulaCommand, CancellationToken.None);

            // Assert  
            Assert.True(result);
            mocker.GetMock<ICursoRepositoty>().Verify(repo => repo.Remover(It.IsAny<Aula>()), Times.Once);
            mocker.GetMock<ICursoRepositoty>().Verify(repo => repo.UnitOfWork.Commit(), Times.Once);
        }

        [Fact(DisplayName = "Remover material da aula com sucesso")]
        [Trait("Categoria", "Gestão Conteudo - Aula Command Handler")]
        public async Task RemoverMaterialAula_MaterialEncontrada_DeveRemoverComSucesso()
        {
            // Arrange
            var material = new Material("Material 1");
            var materialCommand = new RemoverMaterialAulaCommand(material.Id);

            var mocker = new AutoMocker();
            var aulaHandler = mocker.CreateInstance<AulaCommandHandler>();

            mocker.GetMock<ICursoRepositoty>()
                .Setup(repo => repo.ObterMaterialPorId(materialCommand.MaterialId)).ReturnsAsync(material);

            mocker.GetMock<ICursoRepositoty>()
                .Setup(repo => repo.UnitOfWork.Commit()).Returns(Task.FromResult(true));

            // Act  
            var result = await aulaHandler.Handle(materialCommand, CancellationToken.None);

            // Assert  
            Assert.True(result);
            mocker.GetMock<ICursoRepositoty>().Verify(repo => repo.Remover(It.IsAny<Material>()), Times.Once);
            mocker.GetMock<ICursoRepositoty>().Verify(repo => repo.UnitOfWork.Commit(), Times.Once);
        }

        [Fact(DisplayName = "Material Não Encontrado na remoção")]
        [Trait("Categoria", "Gestão Conteudo - Aula Command Handler")]
        public async Task RemoverMaterialAula_MaterialNaoEncontrado_NaoDeveRemoverComSucesso()
        {
            // Arrange             
            var materialCommand = new RemoverMaterialAulaCommand(Guid.NewGuid());

            var mocker = new AutoMocker();
            var aulaHandler = mocker.CreateInstance<AulaCommandHandler>();

            mocker.GetMock<ICursoRepositoty>()
                .Setup(repo => repo.ObterMaterialPorId(materialCommand.MaterialId)).Returns(Task.FromResult<Material?>(null));

            // Act  
            var result = await aulaHandler.Handle(materialCommand, CancellationToken.None);

            // Assert  
            Assert.False(result);
            mocker.GetMock<ICursoRepositoty>().Verify(repo => repo.Remover(It.IsAny<Material>()), Times.Never);
            mocker.GetMock<ICursoRepositoty>().Verify(repo => repo.UnitOfWork.Commit(), Times.Never);
        }
    }
}
