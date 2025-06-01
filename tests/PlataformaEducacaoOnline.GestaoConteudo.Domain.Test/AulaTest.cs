using PlataformaEducacaoOnline.Core.DomainObjects;

namespace PlataformaEducacaoOnline.GestaoConteudo.Domain.Test
{
    public class AulaTest
    {
        [Fact(DisplayName = "Adicionar Aula dados Inválidos")]
        [Trait("Categoria", "Aula Testes")]
        public void Validar_AdicionarAula_DeveRetornarDomainException()
        {
            // AAA
            Assert.Throws<DomainException>(() => new Aula(string.Empty, string.Empty, Guid.Empty));
        }

        [Fact(DisplayName = "Adicionar Aula vinculando ao material")]
        [Trait("Categoria", "Aula Testes")]
        public void AdicionarAula_NovaAula_DeveVincularMaterialAula()
        {
            // Arrange
            var aula = new Aula("Aula 1", "Descrição da Aula 1", Guid.NewGuid());
            var material = new Material("Material 1");

            // Act
            aula.AdicionarMaterial(material);

            // Assert
            Assert.Equal(aula.Id, material.AulaId);
        }

        [Fact(DisplayName = "Adicionar material existente a uma Aula")]
        [Trait("Categoria", "Curso Testes")]
        public void AdicionarMaterial_MaterialExistente_DeveRetornarDomainException()
        {
            var CursoId = Guid.NewGuid();

            // Arrange
            var aula = new Aula("Aula 1", "Descrição da Aula 1", CursoId);
            var material = new Material("Material 1");

            // Act
            aula.AdicionarMaterial(material);

            // Act && Assert
            Assert.Throws<DomainException>(() => aula.AdicionarMaterial(material));
        }
    }
}
