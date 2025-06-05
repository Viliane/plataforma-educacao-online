using PlataformaEducacaoOnline.GestaoConteudo.Application.Commands;

namespace PlataformaEducacaoOnline.GestaoConteudo.Application.Test
{
    public class AdicionarAulaCommandTest
    {
        [Fact(DisplayName = "Adicionar Aula Command Válido")]
        [Trait("Categoria", "Gestão Conteudo - Aula Commands")]
        public void AdicionarAulaCommand_CommnadoEstaValido_DevePassarNaValidacao()
        {
            // Arrange
            var aulaCommand = new AdicionarAulaCommand("Aula de Teste", "Conteúdo Programático de Teste", Guid.NewGuid(), 
                   null);

            //Act
            var result = aulaCommand.EhValido();

            // Assert
            Assert.True(result);
        }

        [Fact(DisplayName = "Adicionar Aula Command dados inválido")]
        [Trait("Categoria", "Gestão Conteudo - Aula Commands")]
        public void AdicionarAulaCommand_CommnadoEstaValido_NaoDevePassarNaValidacao()
        {
            // Arrange
            var aulaCommand = new AdicionarAulaCommand(string.Empty, string.Empty, Guid.Empty, null);

            //Act
            var result = aulaCommand.EhValido();

            // Assert
            Assert.False(result);
        }
    }
}
