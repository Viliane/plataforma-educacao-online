using PlataformaEducacaoOnline.Core.DomainObjects;
using PlataformaEducacaoOnline.Core.DomainObjects.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaEducacaoOnline.GestaoConteudo.Domain.Test
{
    public class EvolucaoTest
    {
        [Fact(DisplayName = "Adicionar nova evolucao das aulas com dados Inválidos")]
        [Trait("Categoria", "EvolucaoAula Testes")]
        public void Validar_AdicionarNovoEvolucaoAula_DeveRetornarDomainException()
        {
            // AAA
            Assert.Throws<DomainException>(() => new EvolucaoAula(Guid.Empty, Guid.Empty, Guid.Empty));
        }

        [Fact(DisplayName = "Adicionar nova evolucao das aulas com dados Inválidos")]
        [Trait("Categoria", "EvolucaoAula Testes")]
        public void Validar_AdicionarNovoEvolucaoAula_DeveDadosComSucesso()
        {
            var evolucaoAula = new EvolucaoAula(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());

            //Assert            
            Assert.NotNull(evolucaoAula);
        }

        [Fact(DisplayName = "Alterar Status Evolucao da Aula em andamento")]
        [Trait("Categoria", "EvolucaoAula Testes")]
        public void AulaEmAndamento_AlterarStatusEvolucaoAula_DeveDadosComSucesso()
        {
            //Arrange && Act
            var evolucaoAula = new EvolucaoAula(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());
            evolucaoAula.AulaEmAndamento();

            //Assert            
            Assert.NotNull(evolucaoAula);
            Assert.Equal(StatusAula.EmAndamento, evolucaoAula.Status);
        }

        [Fact(DisplayName = "Alterar SStatus Evolucao da Aula Concluida")]
        [Trait("Categoria", "EvolucaoAula Testes")]
        public void ConcluirCurso_AlterarStatusMatricula_DeveDadosComSucesso()
        {
            //Arrange && Act
            var evolucaoAula = new EvolucaoAula(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());
            evolucaoAula.AulaConcluida();

            //Assert            
            Assert.NotNull(evolucaoAula);
            Assert.Equal(StatusAula.Concluida, evolucaoAula.Status);
        }
    }
}
