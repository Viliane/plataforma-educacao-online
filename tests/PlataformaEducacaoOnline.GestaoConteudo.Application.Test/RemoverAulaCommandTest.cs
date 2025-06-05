using PlataformaEducacaoOnline.GestaoConteudo.Application.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaEducacaoOnline.GestaoConteudo.Application.Test
{
    public class RemoverAulaCommandTest
    {
        [Fact(DisplayName = "Remover Aula Command Válido")]
        [Trait("Categoria", "Gestão Conteudo - Aula Commands")]
        public void RemoverAulaCommand_CommnadoEstaValido_DevePassarNaValidacao()
        {
            // Arrange
            var aulaCommand = new RemoverAulaCommand(Guid.NewGuid());

            //Act
            var result = aulaCommand.EhValido();

            // Assert
            Assert.True(result);
        }

        [Fact(DisplayName = "Remover Aula Command dados inválido")]
        [Trait("Categoria", "Gestão Conteudo - Aula Commands")]
        public void RemoverAulaCommand_CommnadoEstaValido_NaoDevePassarNaValidacao()
        {
            // Arrange
            var aulaCommand = new RemoverCursoCommand(Guid.Empty);

            //Act
            var result = aulaCommand.EhValido();

            // Assert
            Assert.False(result);
        }
    }
}
