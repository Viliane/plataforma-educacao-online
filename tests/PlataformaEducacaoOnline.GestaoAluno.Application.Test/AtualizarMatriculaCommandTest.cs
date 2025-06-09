using PlataformaEducacaoOnline.GestaoAluno.Application.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaEducacaoOnline.GestaoAluno.Application.Test
{
    public class AtualizarMatriculaCommandTest
    {
        [Fact(DisplayName = "Atualizar Matricula Command Válido")]
        [Trait("Categoria", "Gestão Aluno - Aluno Commands")]
        public void AtualizarMatriculaCommand_CommnadoEstaValido_DevePassarNaValidacao()
        {
            // Arrange
            var matriculaCommand = new AtualizarMatriculaCommand(Guid.NewGuid());

            //Act
            var result = matriculaCommand.EhValido();

            // Assert
            Assert.True(result);
        }

        [Fact(DisplayName = "Atualizar Matricula Command dados inválido")]
        [Trait("Categoria", "Gestão Aluno - Aluno Commands")]
        public void AtualizarMatriculaCommand_CommnadoEstaValido_NaoDevePassarNaValidacao()
        {
            // Arrange
            var matriculaCommand = new AtualizarMatriculaCommand(Guid.Empty);

            //Act
            var result = matriculaCommand.EhValido();

            // Assert
            Assert.False(result);
        }
    }
}
