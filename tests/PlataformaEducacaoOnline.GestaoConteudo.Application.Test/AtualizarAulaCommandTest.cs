using PlataformaEducacaoOnline.GestaoConteudo.Application.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaEducacaoOnline.GestaoConteudo.Application.Test
{
    public class AtualizarAulaCommandTest
    {
        [Fact(DisplayName = "Atualizar Aula Command Válido")]
        [Trait("Categoria", "Gestão Conteudo - AUla Commands")]
        public void AtualizarAulaCommand_CommnadoEstaValido_DevePassarNaValidacao()
        {
            // Arrange
            var aulaCommand = new AtualizarAulaCommand(Guid.NewGuid(), "Aula de Teste", "Conteúdo Programático de Teste");

            //Act
            var result = aulaCommand.EhValido();

            // Assert
            Assert.True(result);
        }

        [Fact(DisplayName = "Atualizar Aula Command dados inválido")]
        [Trait("Categoria", "Gestão Conteudo - Aula Commands")]
        public void AtualizarAulaCommand_CommnadoEstaValido_NaoDevePassarNaValidacao()
        {
            // Arrange
            var aulaCommand = new AtualizarAulaCommand(Guid.Empty, string.Empty, string.Empty);

            //Act
            var result = aulaCommand.EhValido();

            // Assert
            Assert.False(result);
        }
    }
}
