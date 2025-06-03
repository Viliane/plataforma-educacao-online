using PlataformaEducacaoOnline.Core.DomainObjects;

namespace PlataformaEducacaoOnline.GestaoConteudo.Domain.Test
{
    public class CursoTest
    {
        [Fact(DisplayName = "Adicionar Novo Curso dados Inválidos")]
        [Trait("Categoria", "Curso Testes")]
        public void Validar_AdicionarNovoCurso_DeveRetornarDomainException()
        {
            // AAA
            Assert.Throws<DomainException>(() => new Curso(string.Empty, Guid.Empty, new ConteudoProgramatico(string.Empty), 0));
        }        
    }
}