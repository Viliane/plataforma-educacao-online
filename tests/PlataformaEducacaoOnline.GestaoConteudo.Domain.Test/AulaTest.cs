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
        [Trait("Categoria", "Aula Testes")]
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

        [Fact(DisplayName = "Atualizar titulo com dados validos")]
        [Trait("Categoria", "Aula Testes")]
        public void AtualizarTitulo_AtulizarAula_DeveRetornarTituloAtualizado()
        {
            var aula = new Aula("Titulo Aula 1", "Conteudo da Aula 1", Guid.NewGuid());

            aula.AtualizarTitulo("Titulo Aula 2");

            Assert.Equal("Titulo Aula 2", aula.Titulo);
        }

        [Fact(DisplayName = "Atualizar titulo com dados invalidos")]
        [Trait("Categoria", "Aula Testes")]
        public void AtualizarTitulo_AtulizarAula_DeveRetornarDomainException()
        {
            // Arrange
            var aula = new Aula("Titulo Aula 1", "Conteudo da Aula 1", Guid.NewGuid());
            // Act && Assert
            Assert.Throws<DomainException>(() => aula.AtualizarTitulo(string.Empty));
        }

        [Fact(DisplayName = "Atualizar conteudo com dados validos")]
        [Trait("Categoria", "Aula Testes")]
        public void AtualizarConteudo_AtulizarAula_DeveRetornarTituloAtualizado()
        {
            var aula = new Aula("Titulo Aula 1", "Conteudo da Aula 1", Guid.NewGuid());

            aula.AtualizarConteudo("Titulo Aula 2");

            Assert.Equal("Titulo Aula 2", aula.Conteudo);
        }

        [Fact(DisplayName = "Atualizar conteudo com dados invalidos")]
        [Trait("Categoria", "Aula Testes")]
        public void AtualizarConteudo_AtulizarAula_DeveRetornarDomainException()
        {
            // Arrange
            var aula = new Aula("Titulo Aula 1", "Conteudo da Aula 1", Guid.NewGuid());
            // Act && Assert
            Assert.Throws<DomainException>(() => aula.AtualizarConteudo(string.Empty));
        }
    }
}
