using MediatR;
using Moq;
using Moq.AutoMock;
using PlataformaEducacaoOnline.Core.DomainObjects;
using PlataformaEducacaoOnline.Core.DomainObjects.DTO;
using PlataformaEducacaoOnline.GestaoAluno.Application.Commands;
using PlataformaEducacaoOnline.PagamentoFaturamento.Business;

namespace PlataformaEducacao.Pagamento.Tests
{
    public class PagamentoServiceTests
    {
        private readonly AutoMocker _mocker;

        public PagamentoServiceTests()
        {
            _mocker = new AutoMocker();
        }

        [Fact(DisplayName = "Realizar Pagamento Command dados validos")]
        [Trait("Categoria", "Pagamentos - Realizar Pagamento")]
        public void RealizarPagamento_Command_DeveExecutarComSucesso()
        {
            // Arrange
            var command = new RealizarPagamentoCommand(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), 200, "Aluno Teste", "1234567891234567", "08/29", "123");

            // Act
            var result = command.EhValido();

            // Assert
            Assert.True(result);
        }

        [Fact(DisplayName = "Realizar Pagamento Command dados invalidos")]
        [Trait("Categoria", "Pagamentos - Realizar Pagamento")]
        public void RealizarPagamento_Command_NaoDeveExecutarComSucesso()
        {
            // Arrange
            var command = new RealizarPagamentoCommand(Guid.Empty, Guid.Empty, Guid.Empty, 0, string.Empty, string.Empty, string.Empty, string.Empty);

            // Act
            var result = command.EhValido();

            // Assert
            Assert.False(result);
        }

        [Fact(DisplayName = "Realizar Pagamento Matricula Sucesso")]
        [Trait("Categoria", "Pagamentos - RealizarPagamentoMatricula")]
        public async Task RealizarPagamentoMatricula_StatusTransacaoPaga_DeveSalvarComSucesso()
        {
            // Arrange
            var pagamentoCurso = new PagamentoMatricula
            {
                AlunoId = Guid.NewGuid(),
                CursoId = Guid.NewGuid(),
                NomeCartao = "Aluno Teste",
                NumeroCartao = "5502093788528294",
                ExpiracaoCartao = "08/29",
                CvvCartao = "123",
                Valor = 200.00m
            };

            var pagamentoService = _mocker.CreateInstance<PagamentoService>();

            var transacaoPaga = new PlataformaEducacaoOnline.PagamentoFaturamento.Business.Transacao
            {
                StatusTransacao = StatusTransacao.Pago
            };

            _mocker.GetMock<IPagamentoServices>()
                .Setup(p => p.RealizarPagamentoMatricula(It.IsAny<PagamentoMatricula>()))
                .ReturnsAsync(transacaoPaga);

            _mocker.GetMock<IPagamentoCartaoCreditoFacade>()
                .Setup(p => p.RealizarPagamento(It.IsAny<Pedido>(), It.IsAny<PlataformaEducacaoOnline.PagamentoFaturamento.Business.Pagamento>()))
                .Returns(transacaoPaga);

            _mocker.GetMock<IPagamentoRepository>().Setup(r => r.UnitOfWork.Commit()).ReturnsAsync(true);

            // Act
            var result = await pagamentoService.RealizarPagamentoMatricula(pagamentoCurso);

            // Assert
            Assert.True(result.StatusTransacao == StatusTransacao.Pago);
            _mocker.GetMock<IPagamentoRepository>().Verify(r => r.Adicionar(It.IsAny<PlataformaEducacaoOnline.PagamentoFaturamento.Business.Pagamento>()));
            _mocker.GetMock<IPagamentoRepository>().Verify(r => r.Adicionar(It.IsAny<PlataformaEducacaoOnline.PagamentoFaturamento.Business.Transacao>()));
            _mocker.GetMock<IPagamentoRepository>().Verify(r => r.UnitOfWork.Commit(), Times.Once);
        }

        [Fact(DisplayName = "Realizar Pagamento Matricula Recusado")]
        [Trait("Categoria", "Pagamentos - RealizarPagamentoMatricula")]
        public async Task RealizarPagamentoMatricula_StatusTransacaoREcusado_NaoDeveSalvarComSucesso()
        {
            // Arrange
            var pagamentoCurso = new PagamentoMatricula
            {
                AlunoId = Guid.NewGuid(),
                CursoId = Guid.NewGuid(),
                NomeCartao = "Aluno Teste",
                NumeroCartao = "5502093788528294",
                ExpiracaoCartao = "08/29",
                CvvCartao = "123",
                Valor = 200.00m
            };

            var pagamentoService = _mocker.CreateInstance<PagamentoService>();

            var transacaoRecusado = new PlataformaEducacaoOnline.PagamentoFaturamento.Business.Transacao
            {
                StatusTransacao = StatusTransacao.Recusado
            };

            _mocker.GetMock<IPagamentoServices>()
                .Setup(p => p.RealizarPagamentoMatricula(It.IsAny<PagamentoMatricula>()))
                .ReturnsAsync(transacaoRecusado);

            _mocker.GetMock<IPagamentoCartaoCreditoFacade>()
                .Setup(p => p.RealizarPagamento(It.IsAny<Pedido>(), It.IsAny<PlataformaEducacaoOnline.PagamentoFaturamento.Business.Pagamento>()))
                .Returns(transacaoRecusado);            

            // Act
            var result = await pagamentoService.RealizarPagamentoMatricula(pagamentoCurso);

            // Assert
            Assert.True(result.StatusTransacao == StatusTransacao.Recusado);
            _mocker.GetMock<IPagamentoRepository>().Verify(r => r.Adicionar(It.IsAny<PlataformaEducacaoOnline.PagamentoFaturamento.Business.Pagamento>()), Times.Never);
            _mocker.GetMock<IPagamentoRepository>().Verify(r => r.Adicionar(It.IsAny<PlataformaEducacaoOnline.PagamentoFaturamento.Business.Transacao>()), Times.Never);
            _mocker.GetMock<IPagamentoRepository>().Verify(r => r.UnitOfWork.Commit(), Times.Never);
        }
    }
}