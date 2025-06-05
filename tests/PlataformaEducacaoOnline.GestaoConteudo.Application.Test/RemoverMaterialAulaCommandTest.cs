using PlataformaEducacaoOnline.GestaoConteudo.Application.Commands;

namespace PlataformaEducacaoOnline.GestaoConteudo.Application.Test
{
    public class RemoverMaterialAulaCommandTest
    {
        [Fact(DisplayName = "Remover Material da Aula Command Válido")]
        [Trait("Categoria", "Gestão Conteudo - Aula Commands")]
        public void RemoverMaterialAulaCommand_CommnadoEstaValido_DevePassarNaValidacao()
        {
            // Arrange
            var materialAulaCommand = new RemoverMaterialAulaCommand(Guid.NewGuid());

            //Act
            var result = materialAulaCommand.EhValido();

            // Assert
            Assert.True(result);
        }

        [Fact(DisplayName = "Remover MAterial da Aula Command dados inválido")]
        [Trait("Categoria", "Gestão Conteudo - Aula Commands")]
        public void RemoverMaterialAulaCommand_CommnadoEstaValido_NaoDevePassarNaValidacao()
        {
            // Arrange
            var materialAulaCommand = new RemoverMaterialAulaCommand(Guid.Empty);

            //Act
            var result = materialAulaCommand.EhValido();

            // Assert
            Assert.False(result);
        }
    }
}
