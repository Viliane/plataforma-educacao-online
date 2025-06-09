using PlataformaEducacaoOnline.GestaoAluno.Application.Commands;

namespace PlataformaEducacaoOnline.GestaoAluno.Application.Test
{
    public class AdicionarAlunoCommandTest
    {
        [Fact(DisplayName = "Adicionar Aluno Command V�lido")]
        [Trait("Categoria", "Gest�o Aluno - Aluno Commands")]
        public void AdicionarAlunoCommand_CommnadoEstaValido_DevePassarNaValidacao()
        {
            // Arrange
            var alunoCommand = new AdicionarAlunoCommand(Guid.NewGuid(), "Jo�o da Silva");   

            //Act
            var result = alunoCommand.EhValido();

            // Assert
            Assert.True(result);
        }

        [Fact(DisplayName = "Adicionar Aluno Command dados inv�lido")]
        [Trait("Categoria", "Gest�o Aluno - Aluno Commands")]
        public void AdicionarAlunoCommand_CommnadoEstaValido_NaoDevePassarNaValidacao()
        {
            // Arrange
            var aulaCommand = new AdicionarAlunoCommand(Guid.Empty, string.Empty);

            //Act
            var result = aulaCommand.EhValido();

            // Assert
            Assert.False(result);
        }
    }
}