using PlataformaEducacaoOnline.GestaoAluno.Application.Commands;

namespace PlataformaEducacaoOnline.GestaoAluno.Application.Test
{
    public class AdicionarAlunoCommandTest
    {
        [Fact(DisplayName = "Adicionar Aluno Command Válido")]
        [Trait("Categoria", "Gestão Aluno - Aluno Commands")]
        public void AdicionarAlunoCommand_CommnadoEstaValido_DevePassarNaValidacao()
        {
            // Arrange
            var alunoCommand = new AdicionarAlunoCommand(Guid.NewGuid(), "João da Silva");   

            //Act
            var result = alunoCommand.EhValido();

            // Assert
            Assert.True(result);
        }

        [Fact(DisplayName = "Adicionar Aluno Command dados inválido")]
        [Trait("Categoria", "Gestão Aluno - Aluno Commands")]
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