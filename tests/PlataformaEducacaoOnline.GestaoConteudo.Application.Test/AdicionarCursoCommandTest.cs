using PlataformaEducacaoOnline.GestaoConteudo.Application.Commands;

namespace PlataformaEducacaoOnline.GestaoConteudo.Application.Test
{
    public class AdicionarCursoCommandTest
    {
        [Fact(DisplayName = "Adicionar Curso Command Válido")]
        [Trait("Categoria", "Gestão Conteudo - Curso Commands")]
        public void AdicionarCursoCommand_CommnadoEstaValido_DevePassarNaValidacao()
        {
            // Arrange
            var cursoCommand = new AdicionarCursoCommand("Curso de Teste", Guid.NewGuid(), "Conteúdo Programático de Teste");

            //Act
            var result = cursoCommand.EhValido();

            // Assert
            Assert.True(result);
        }
    }
}
