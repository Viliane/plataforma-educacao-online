using PlataformaEducacaoOnline.Api.DTO;
using PlataformaEducacaoOnline.Api.Tests.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaEducacaoOnline.Api.Tests
{
    [TestCaseOrderer("PlataformaEducacaoOnline.Api.Tests.Config.PriorityOrderer", "PlataformaEducacaoOnline.Api.Tests")]
    [Collection(nameof(IntegrationApiTestsFixtureCollection))]
    public class AlunoTests
    {
        private readonly IntegrationTestsFixture _testsfixture;

        public AlunoTests(IntegrationTestsFixture testsfixture)
        {
            _testsfixture = testsfixture;
        }

        [Fact(DisplayName = "Registrar Aluno com Sucesso"), TestPriority(5)]
        [Trait("Categoria", "Aluno")]
        public async Task Aluno_Registrar_DeveRetornarComSucesso()
        {
            // Arrange
            _testsfixture.GerarDadosUsuario();

            var dadosUsuario = new RegisterUserDto
            {
                Email = _testsfixture.usuarioEmail,
                Nome = _testsfixture.usuarioNome,
                Senha = _testsfixture.usuarioSenha,
                ConfimacaoSenha = _testsfixture.usuarioConfirmasenha
            };

            // Act
            var postResponse = await _testsfixture.Client.PostAsJsonAsync("api/conta/registrar/aluno", dadosUsuario);

            //Assert
            postResponse.EnsureSuccessStatusCode();

            _testsfixture.SalvarTokenUsuario(await postResponse.Content.ReadAsStringAsync());
        }

        [Fact(DisplayName = "Login aluno com Sucesso"), TestPriority(6)]
        [Trait("Categoria", "Aluno")]
        public async Task Aluno_Login_DeveRetornarComSucesso()
        {
            // Arrange  
            var loginUsuario = new LoginUserDto
            {
                Email = _testsfixture.usuarioEmail,
                Senha = _testsfixture.usuarioSenha
            };

            // Act
            var postResponse = await _testsfixture.Client.PostAsJsonAsync("api/conta/login", loginUsuario);

            //Assert
            postResponse.EnsureSuccessStatusCode();

            _testsfixture.SalvarTokenUsuario(await postResponse.Content.ReadAsStringAsync());
        }

        [Fact(DisplayName = "Matricular aluno com Sucesso"), TestPriority(7)]
        [Trait("Categoria", "Aluno")]
        public async Task Aluno_Matricular_DeveRetornarComSucesso()
        {
            _testsfixture.Client.AtribuirBearerToken(_testsfixture.TokenUsuario);

            // Arrange  
            var dadosMatricula = new MatriculaDto
            {
                CursoId = _testsfixture.CursoId,
                AlunoId = _testsfixture.UsuarioId
            };

            // Act
            var postResponse = await _testsfixture.Client.PostAsJsonAsync("api/matricula", dadosMatricula);

            //Assert
            postResponse.EnsureSuccessStatusCode();
        }

        [Fact(DisplayName = "Realizar pagamento do aluno com Sucesso"), TestPriority(8)]
        [Trait("Categoria", "Aluno")]
        public async Task Aluno_RealizarPagamento_DeveRetornarComSucesso()
        {
            _testsfixture.Client.AtribuirBearerToken(_testsfixture.TokenUsuario);
            _testsfixture.GerarDadosCartao(); 

            // Arrange  
            var dadosPagamento = new PagamentoMatriculaDto
            {
                CursoId = _testsfixture.CursoId,
                AlunoId = _testsfixture.UsuarioId,
                Valor = 1000,
                NomeCartao = _testsfixture.NomeCartao,
                NumeroCartao = _testsfixture.NumeroCartao,
                ExpiracaoCartao = _testsfixture.ExpiracaoCartao,
                CvvCartao = _testsfixture.CvvCartao
            };

            // Act
            var postResponse = await _testsfixture.Client.PostAsJsonAsync("api/matricula/realizar-pagamento", dadosPagamento);

            //Assert
            postResponse.EnsureSuccessStatusCode();
        }
    }
}
