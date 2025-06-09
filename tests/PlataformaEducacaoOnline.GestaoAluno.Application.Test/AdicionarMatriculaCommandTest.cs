using PlataformaEducacaoOnline.GestaoAluno.Application.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaEducacaoOnline.GestaoAluno.Application.Test
{
    public class AdicionarMatriculaCommandTest
    {
        [Fact(DisplayName = "Adicionar Matricula Command Válido")]
        [Trait("Categoria", "Gestão Aluno - Aluno Commands")]
        public void AdicionarMatriculaCommand_CommnadoEstaValido_DevePassarNaValidacao()
        {
            // Arrange
            var matriculaCommand = new AdicionarMatriculaCommand(Guid.NewGuid(), Guid.NewGuid());

            //Act
            var result = matriculaCommand.EhValido();

            // Assert
            Assert.True(result);
        }

        [Fact(DisplayName = "Adicionar Matricula Command dados inválido")]
        [Trait("Categoria", "Gestão Aluno - Aluno Commands")]
        public void AdicionarMatriculaCommand_CommnadoEstaValido_NaoDevePassarNaValidacao()
        {
            // Arrange
            var matriculaCommand = new AdicionarMatriculaCommand(Guid.Empty, Guid.Empty);

            //Act
            var result = matriculaCommand.EhValido();

            // Assert
            Assert.False(result);
        }
    }
}
