using PlataformaEducacaoOnline.GestaoAluno.Application.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaEducacaoOnline.GestaoAluno.Application.Test
{
    public class FinalizarCursoCommandTest
    {
        [Fact(DisplayName = "Finalizar Matricula Command Válido")]
        [Trait("Categoria", "Gestão Aluno - Aluno Commands")]
        public void FinalizarCursoCommand_CommnadoEstaValido_DevePassarNaValidacao()
        {
            // Arrange
            var finalizarCommand = new FinalizarCursoCommand(Guid.NewGuid(), Guid.NewGuid());

            //Act
            var result = finalizarCommand.EhValido();

            // Assert
            Assert.True(result);
        }

        [Fact(DisplayName = "Finalizar Matricula Command dados inválido")]
        [Trait("Categoria", "Gestão Aluno - Aluno Commands")]
        public void FinalizarCursoCommand_CommnadoEstaValido_NaoDevePassarNaValidacao()
        {
            // Arrange
            var matriculaCommand = new FinalizarCursoCommand(Guid.Empty, Guid.Empty);

            //Act
            var result = matriculaCommand.EhValido();

            // Assert
            Assert.False(result);
        }
    }
}
