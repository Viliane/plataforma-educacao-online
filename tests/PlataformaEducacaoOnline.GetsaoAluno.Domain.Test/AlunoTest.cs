using PlataformaEducacaoOnline.Core.DomainObjects;
using PlataformaEducacaoOnline.GestaoAluno.Domain;

namespace PlataformaEducacaoOnline.GetsaoAluno.Domain.Test
{
    public class AlunoTest
    {
        [Fact(DisplayName = "Adicionar Novo Aluno dados Inválidos")]
        [Trait("Categoria", "Aluno Testes")]
        public void Validar_AdicionarNovoAluno_DeveRetornarDomainException()
        {
            // AAA
            Assert.Throws<DomainException>(() => new Aluno(string.Empty, Guid.Empty, new HistoricoAprendizado(string.Empty)));
        }

        [Fact(DisplayName = "Adicionar Novo Aluno dados Válidos")]
        [Trait("Categoria", "Aluno Testes")]
        public void Validar_AdicionarNovoAluno_DeveDadosComSucesso()
        {
            //Arrange && Act
            var curso = new Aluno("Aluno Teste", Guid.NewGuid(), new HistoricoAprendizado("Teste Aluno"));

            //Assert            
            Assert.NotNull(curso);
        }
    }
}