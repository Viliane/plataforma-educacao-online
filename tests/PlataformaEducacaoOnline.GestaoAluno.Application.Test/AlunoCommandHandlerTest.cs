using MediatR;
using Moq;
using Moq.AutoMock;
using PlataformaEducacaoOnline.GestaoAluno.Application.Commands;
using PlataformaEducacaoOnline.GestaoAluno.Domain;
using System.Net.Sockets;

namespace PlataformaEducacaoOnline.GestaoAluno.Application.Test
{
    public class AlunoCommandHandlerTest
    {
        private readonly AutoMocker _mocker;
        private readonly AlunoCommandHandler _handler;

        public AlunoCommandHandlerTest()
        {
            _mocker = new AutoMocker();
            _handler = _mocker.CreateInstance<AlunoCommandHandler>();
        }

        [Fact(DisplayName = "Adicionar Aluno com Sucesso")]
        [Trait("Categoria", "Gestão Aluno - Aluno Command Handler")]
        public async Task Handle_DeveAdicionarAluno_QuandoComandoValido()
        {
            // Arrange
            var command = new AdicionarAlunoCommand(Guid.NewGuid(), "Joao");

            _mocker.GetMock<IAlunoRepository>()
                .Setup(repo => repo.UnitOfWork.Commit()).Returns(Task.FromResult(true));

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result);
            _mocker.GetMock<IAlunoRepository>()
                .Verify(r => r.Adicionar(It.IsAny<Aluno>()), Times.Once);
            _mocker.GetMock<IAlunoRepository>().Verify(repo => repo.UnitOfWork.Commit(), Times.Once);
        }

        [Fact(DisplayName = "Retornar Erro ao Adicionar Aluno Inválido")]
        [Trait("Categoria", "Gestão Aluno - Aluno Command Handler")]
        public async Task Handle_DeveRetornarErro_QuandoComandoInvalido()
        {
            // Arrange
            var command = new AdicionarAlunoCommand(Guid.Empty, string.Empty);
            _mocker.GetMock<IAlunoRepository>()
                .Setup(r => r.Adicionar(It.IsAny<Aluno>()))
                .Throws(new Exception("Não deve adicionar aluno inválido"));

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            _mocker.GetMock<IAlunoRepository>()
                .Verify(r => r.Adicionar(It.IsAny<Aluno>()), Times.Never);            
        }

        [Fact(DisplayName = "Adicionar Matrícula com Sucesso")]
        [Trait("Categoria", "Gestão Aluno - Aluno Command Handler")]
        public async Task Handle_DeveAdicionarMatricula_QuandoComandoValido()
        {
            // Arrange
            var alunoId = Guid.NewGuid();
            var cursoId = Guid.NewGuid();
            var command = new AdicionarMatriculaCommand(alunoId, cursoId);
            _mocker.GetMock<IAlunoRepository>()
                .Setup(r => r.ObterPorId(alunoId))
                .ReturnsAsync(new Aluno(alunoId, "Aluno Teste"));
            _mocker.GetMock<IAlunoRepository>()
                .Setup(repo => repo.UnitOfWork.Commit()).Returns(Task.FromResult(true));

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            _mocker.GetMock<IAlunoRepository>()
                .Verify(r => r.Adicionar(It.IsAny<Matricula>()), Times.Once);
            _mocker.GetMock<IAlunoRepository>().Verify(repo => repo.UnitOfWork.Commit(), Times.Once);
        }

        [Fact(DisplayName = "Retornar Erro ao Adicionar Matrícula Inválida")]
        [Trait("Categoria", "Gestão Aluno - Aluno Command Handler")]
        public async Task Handle_DeveRetornarErro_QuandoAdicionarMatriculaComandoInvalido()
        {
            // Arrange
            var command = new AdicionarMatriculaCommand(Guid.Empty, Guid.Empty);
            _mocker.GetMock<IAlunoRepository>()
                .Setup(r => r.Adicionar(It.IsAny<Matricula>()))
                .Throws(new Exception("Não deve adicionar matrícula inválida"));

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            _mocker.GetMock<IAlunoRepository>()
                .Verify(r => r.Adicionar(It.IsAny<Matricula>()), Times.Never);
            Assert.False(result);
        }

        [Fact(DisplayName = "Atualizar Matrícula com Sucesso")]
        [Trait("Categoria", "Gestão Aluno - Aluno Command Handler")]
        public async Task Handle_DeveAtualizarMatricula_QuandoComandoValido()
        {
            // Arrange
            var matriculaId = Guid.NewGuid();
            var command = new AtualizarMatriculaCommand(matriculaId);
            _mocker.GetMock<IAlunoRepository>()
                .Setup(r => r.ObterMatriculaPorId(matriculaId))
                .ReturnsAsync(new Matricula(Guid.NewGuid(), Guid.NewGuid()));
            _mocker.GetMock<IAlunoRepository>()
                .Setup(repo => repo.UnitOfWork.Commit()).Returns(Task.FromResult(true));

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            _mocker.GetMock<IAlunoRepository>()
                .Verify(r => r.Atualizar(It.IsAny<Matricula>()), Times.Once);
            _mocker.GetMock<IAlunoRepository>().Verify(repo => repo.UnitOfWork.Commit(), Times.Once);
        }

        [Fact(DisplayName = "Retornar Erro ao Atualizar Matrícula Inválida")]
        [Trait("Categoria", "Gestão Aluno - Aluno Command Handler")]
        public async Task Handle_DeveRetornarErro_QuandoAtualizarMatriculaComandoInvalido()
        {
            // Arrange
            var command = new AtualizarMatriculaCommand(Guid.Empty);
            _mocker.GetMock<IAlunoRepository>()
                .Setup(r => r.Atualizar(It.IsAny<Matricula>()))
                .Throws(new Exception("Não deve atualizar matrícula inválida"));

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            _mocker.GetMock<IAlunoRepository>()
                .Verify(r => r.Atualizar(It.IsAny<Matricula>()), Times.Never);
            Assert.False(result);
        }

        [Fact(DisplayName = "Finalizar Curso com Sucesso")]
        [Trait("Categoria", "Gestão Aluno - Aluno Command Handler")]
        public async Task Handle_DeveFinalizarCurso_QuandoComandoValido()
        {
            // Arrange
            var alunoId = Guid.NewGuid();
            var cursoId = Guid.NewGuid();
            var command = new FinalizarCursoCommand(alunoId, cursoId);

            var matricula = new Matricula(cursoId, alunoId);
            _mocker.GetMock<IAlunoRepository>()
                .Setup(r => r.ObterMatriculaPorAlunoIdCursoId(alunoId, cursoId))
                .ReturnsAsync(matricula);

            _mocker.GetMock<IAlunoRepository>()
                .Setup(repo => repo.UnitOfWork.Commit()).Returns(Task.FromResult(true));

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result);
            _mocker.GetMock<IAlunoRepository>().Verify(repo => repo.UnitOfWork.Commit(), Times.Once);
        }

        [Fact(DisplayName = "Retornar Erro ao Finalizar Curso Inválido")]
        [Trait("Categoria", "Gestão Aluno - Aluno Command Handler")]
        public async Task Handle_DeveRetornarErro_QuandoFinalizarCursoComandoInvalido()
        {
            // Arrange
            var command = new FinalizarCursoCommand(Guid.Empty, Guid.Empty);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result);
        }

        [Fact(DisplayName = "Realizar Pagamento com Sucesso")]
        [Trait("Categoria", "Gestão Aluno - Aluno Command Handler")]
        public async Task Handle_DeveRealizarPagamento_QuandoComandoValido()
        {
            // Arrange
            var alunoId = Guid.NewGuid();
            var cursoId = Guid.NewGuid();
            var matriculaId = Guid.NewGuid();
            var command = new RealizarPagamentoCommand(alunoId, cursoId, matriculaId, 100, "Nome", "1234567890123456", "12/30", "123");
            _mocker.GetMock<IAlunoRepository>()
                .Setup(r => r.ObterMatriculaPorId(matriculaId))
                .ReturnsAsync(new Matricula(alunoId, cursoId));

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result);
        }

        [Fact(DisplayName = "Retornar Erro ao Realizar Pagamento Inválido")]
        [Trait("Categoria", "Gestão Aluno - Aluno Command Handler")]
        public async Task Handle_DeveRetornarErro_QuandoRealizarPagamentoComandoInvalido()
        {
            // Arrange
            var command = new RealizarPagamentoCommand(Guid.Empty, Guid.Empty, Guid.Empty, 0, "", "", "", "");
            _mocker.GetMock<IAlunoRepository>()
                .Setup(r => r.ObterMatriculaPorId(It.IsAny<Guid>()))
                .ReturnsAsync((Matricula?)null);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result);
        }
    }
}
