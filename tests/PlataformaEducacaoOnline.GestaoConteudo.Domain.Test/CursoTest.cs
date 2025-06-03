using PlataformaEducacaoOnline.Core.DomainObjects;

namespace PlataformaEducacaoOnline.GestaoConteudo.Domain.Test
{
    public class CursoTest
    {
        [Fact(DisplayName = "Adicionar Novo Curso dados Inv�lidos")]
        [Trait("Categoria", "Curso Testes")]
        public void Validar_AdicionarNovoCurso_DeveRetornarDomainException()
        {
            // AAA
            Assert.Throws<DomainException>(() => new Curso(string.Empty, Guid.Empty, new ConteudoProgramatico(string.Empty), 0));
        }

        [Fact(DisplayName = "Atualizar Nome Curso dados Inv�lidos")]
        [Trait("Categoria", "Curso Testes")]
        public void AtualizarNome_AtualizarCurso_DeveRetornarDomainException()
        {
            // Arrange
            var curso = new Curso("Curso 1", Guid.NewGuid(), new ConteudoProgramatico("Conte�do Program�tico 1"), 100);

            // Act && Assert
            Assert.Equal("O nome do curso � obrigat�rio.", Assert.Throws<DomainException>(() => curso.AtualizarNome(string.Empty)).Message);
        }

        [Fact(DisplayName = "Atualizar Nome Curso dados V�lidos")]
        [Trait("Categoria", "Curso Testes")]
        public void AtualizarNome_AtualizarCurso_DeveRetornarNomeAtualizado()
        {
            // Arrange
            var curso = new Curso("Curso 1", Guid.NewGuid(), new ConteudoProgramatico("Conte�do Program�tico 1"), 100);

            //Act
            curso.AtualizarNome("Curso Atualizado 1");

            // Act && Assert
            Assert.Equal("Curso Atualizado 1", curso.Nome);
        }

        [Fact(DisplayName = "Atualizar Conteudo com dados Inv�lidos")]
        [Trait("Categoria", "Curso Testes")]
        public void AtualizarConteudoProgramatico_AtualizarCurso_DeveRetornarDomainException()
        {
            // Arrange
            var curso = new Curso("Curso 1", Guid.NewGuid(), new ConteudoProgramatico("Conte�do Program�tico 1"), 100);

            // Act && Assert
            Assert.Equal("O conte�do program�tico � obrigat�rio.", Assert.Throws<DomainException>(() => curso.AtualizarConteudoProgramatico(null)).Message);
        }

        [Fact(DisplayName = "Atualizar Conteudo com dados V�lidos")]
        [Trait("Categoria", "Curso Testes")]
        public void AtualizarConteudoProgramatico_AtualizarCurso_DeveRetornarConteudoAtualizado()
        {
            // Arrange
            var curso = new Curso("Curso 1", Guid.NewGuid(), new ConteudoProgramatico("Conte�do Program�tico 1"), 100);

            //Act
            curso.AtualizarConteudoProgramatico(new ConteudoProgramatico("Curso Atualizado 1"));

            // Assert
            Assert.Equal("Curso Atualizado 1", curso.ConteudoProgramatico.DescricaoConteudoProgramatico);
        }

        [Fact(DisplayName = "Atualizar Valor com dados Inv�lidos")]
        [Trait("Categoria", "Curso Testes")]
        public void AtualizarValor_AtualizarCurso_DeveRetornarDomainException()
        {
            // Arrange
            var curso = new Curso("Curso 1", Guid.NewGuid(), new ConteudoProgramatico("Conte�do Program�tico 1"), 100);

            // Act && Assert
            Assert.Equal("O valor do curso deve ser maior que zero.", Assert.Throws<DomainException>(() => curso.AtualizarValor(0)).Message);
        }

        [Fact(DisplayName = "Atualizar Valor com dados V�lidos")]
        [Trait("Categoria", "Curso Testes")]
        public void AtualizarValor_AtualizarCurso_DeveRetornarValorAtualizado()
        {
            // Arrange
            var curso = new Curso("Curso 1", Guid.NewGuid(), new ConteudoProgramatico("Conte�do Program�tico 1"), 100);

            //Act
            curso.AtualizarValor(150);

            // Assert
            Assert.Equal(150, curso.Valor);
        }
    }
}