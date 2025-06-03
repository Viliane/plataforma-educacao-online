using PlataformaEducacaoOnline.GestaoConteudo.Application.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaEducacaoOnline.GestaoConteudo.Application.Test
{
    public class RemoverCursoCommandTest
    {
        [Fact(DisplayName = "Remover Curso Command Válido")]
        [Trait("Categoria", "Gestão Conteudo - Curso Commands")]
        public void RemoverCursoCommand_CommnadoEstaValido_DevePassarNaValidacao()
        {
            // Arrange
            var cursoCommand = new RemoverCursoCommand(Guid.NewGuid());

            //Act
            var result = cursoCommand.EhValido();

            // Assert
            Assert.True(result);
        }

        [Fact(DisplayName = "Remover Curso Command dados inválido")]
        [Trait("Categoria", "Gestão Conteudo - Curso Commands")]
        public void RemoverCursoCommand_CommnadoEstaValido_NaoDevePassarNaValidacao()
        {
            // Arrange
            var cursoCommand = new RemoverCursoCommand(Guid.Empty);

            //Act
            var result = cursoCommand.EhValido();

            // Assert
            Assert.False(result);
        }
    }
}
