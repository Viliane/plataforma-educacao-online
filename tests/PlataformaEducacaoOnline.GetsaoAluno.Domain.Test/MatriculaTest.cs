using PlataformaEducacaoOnline.Core.DomainObjects;
using PlataformaEducacaoOnline.GestaoAluno.Domain;

namespace PlataformaEducacaoOnline.GetsaoAluno.Domain.Test
{
    public class MatriculaTest
    {
        [Fact(DisplayName = "Adicionar Nova Matricula dados Inválidos")]
        [Trait("Categoria", "Matricula Testes")]
        public void Validar_AdicionarNovoMatricula_DeveRetornarDomainException()
        {
            // AAA
            Assert.Throws<DomainException>(() => new Matricula(Guid.Empty, Guid.Empty));
        }

        [Fact(DisplayName = "Adicionar Nova Matricula dados válidos")]
        [Trait("Categoria", "Matricula Testes")]
        public void Validar_AdicionarNovoMatricula_DeveDadosComSucesso()
        {
            //Arrange && Act
            var matricula = new Matricula(Guid.NewGuid(), Guid.NewGuid());

            //Assert            
            Assert.NotNull(matricula);
            Assert.Equal(StatusMatricula.PendentePagamento, matricula.StatusMatricula);
        }

        [Fact(DisplayName = "Alterar Status Matricula Ativa")]
        [Trait("Categoria", "Matricula Testes")]
        public void AtivarMatricula_AlterarStatusMatricula_DeveDadosComSucesso()
        {
            //Arrange && Act
            var matricula = new Matricula(Guid.NewGuid(), Guid.NewGuid());
            matricula.AtivarMatricula();    

            //Assert            
            Assert.NotNull(matricula);
            Assert.Equal(StatusMatricula.Ativa, matricula.StatusMatricula);
        }

        [Fact(DisplayName = "Alterar Status Matricula Concluida")]
        [Trait("Categoria", "Matricula Testes")]
        public void ConcluirCurso_AlterarStatusMatricula_DeveDadosComSucesso()
        {
            //Arrange && Act
            var matricula = new Matricula(Guid.NewGuid(), Guid.NewGuid());
            matricula.ConcluirCurso();

            //Assert            
            Assert.NotNull(matricula);
            Assert.Equal(StatusMatricula.Concluida, matricula.StatusMatricula);
        }
    }
}
