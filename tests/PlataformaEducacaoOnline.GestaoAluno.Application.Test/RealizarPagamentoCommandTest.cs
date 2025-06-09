using PlataformaEducacaoOnline.GestaoAluno.Application.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaEducacaoOnline.GestaoAluno.Application.Test
{
    public class RealizarPagamentoCommandTest
    {
        [Fact(DisplayName = "Realizar Pagamento Matricula Command Válido")]
        [Trait("Categoria", "Gestão Aluno - Aluno Commands")]
        public void RealizarPagamentoCommand_CommnadoEstaValido_DevePassarNaValidacao()
        {
            // Arrange
            var realizarPagamentoCommand = new RealizarPagamentoCommand(Guid.NewGuid(), Guid.NewGuid(),
                Guid.NewGuid(), 200, "Aluno Teste", "2569874586523458", "08/29", "123");

            //Act
            var result = realizarPagamentoCommand.EhValido();

            // Assert
            Assert.True(result);
        }

        [Fact(DisplayName = "Realizar Pagamento Matricula Command dados inválido")]
        [Trait("Categoria", "Gestão Aluno - Aluno Commands")]
        public void RealizarPagamentoCommand_CommnadoEstaValido_NaoDevePassarNaValidacao()
        {
            // Arrange
            var realizarPagamentoCommand = new RealizarPagamentoCommand(Guid.Empty, Guid.Empty, Guid.Empty,
                0, string.Empty, string.Empty, string.Empty, string.Empty);

            //Act
            var result = realizarPagamentoCommand.EhValido();

            // Assert
            Assert.False(result);
        }
    }
}
