using PlataformaEducacaoOnline.GestaoConteudo.Application.Commands;

namespace PlataformaEducacaoOnline.GestaoConteudo.Application.Test
{
    public class AdicionarCursoCommandTest
    {
        [Fact(DisplayName = "Adicionar Curso Command V�lido")]
        [Trait("Categoria", "Gest�o Conteudo - Curso Commands")]
        public void AdicionarCursoCommand_CommnadoEstaValido_DevePassarNaValidacao()
        {
            // Arrange
            var cursoCommand = new AdicionarCursoCommand("Curso de Teste", Guid.NewGuid(), "Conte�do Program�tico de Teste");

            //Act
            var result = cursoCommand.EhValido();

            // Assert
            Assert.True(result);
        }
    }
}
