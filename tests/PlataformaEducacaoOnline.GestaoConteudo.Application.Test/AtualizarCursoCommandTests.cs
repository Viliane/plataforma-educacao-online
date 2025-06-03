using PlataformaEducacaoOnline.GestaoConteudo.Application.Commands;

namespace PlataformaEducacaoOnline.GestaoConteudo.Application.Test
{   
    public class AtualizarCursoCommandTests
    {
        [Fact(DisplayName = "Atualizar Curso Command Válido")]
        [Trait("Categoria", "Gestão Conteudo - Curso Commands")]
        public void AtualizarCursoCommand_CommnadoEstaValido_DevePassarNaValidacao()
        {
            // Arrange
            var cursoCommand = new AtualizarCursoCommand("Curso de Teste", Guid.NewGuid(), "Conteúdo Programático de Teste", 200);

            //Act
            var result = cursoCommand.EhValido();

            // Assert
            Assert.True(result);
        }

        [Fact(DisplayName = "Atualizar Curso Command dados inválido")]
        [Trait("Categoria", "Gestão Conteudo - Curso Commands")]
        public void AtualizarCursoCommand_CommnadoEstaValido_NaoDevePassarNaValidacao()
        {
            // Arrange
            var cursoCommand = new AtualizarCursoCommand(string.Empty, Guid.Empty, "Conteúdo Programático de Teste", 0);

            //Act
            var result = cursoCommand.EhValido();

            // Assert
            Assert.False(result);
        }
    }
}
