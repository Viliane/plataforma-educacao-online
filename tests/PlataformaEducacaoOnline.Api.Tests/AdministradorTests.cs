using PlataformaEducacaoOnline.Api.DTO;
using PlataformaEducacaoOnline.Api.Tests.Config;
using PlataformaEducacaoOnline.Api.Tests.DTO;
using System.Net.Http.Json;

namespace PlataformaEducacaoOnline.Api.Tests
{
    [TestCaseOrderer("PlataformaEducacaoOnline.Api.Tests.Config.PriorityOrderer", "PlataformaEducacaoOnline.Api.Tests")]
    [Collection(nameof(IntegrationApiTestsFixtureCollection))]
    public class AdministradorTests
    {
        private readonly IntegrationTestsFixture _testsfixture;

        public AdministradorTests(IntegrationTestsFixture testsfixture)
        {
            _testsfixture = testsfixture;
        }

        [Fact(DisplayName = "Registrar Administrador com Sucesso"), TestPriority(1)]
        [Trait("Categoria", "Admnistrador")]
        public async Task Administrador_Registrar_DeveRetornarComSucesso()
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
            var postResponse = await _testsfixture.Client.PostAsJsonAsync("api/conta/registrar/admin", dadosUsuario);

            //Assert
            postResponse.EnsureSuccessStatusCode();

            _testsfixture.SalvarTokenUsuario(await postResponse.Content.ReadAsStringAsync());
        }

        [Fact(DisplayName = "Login Administrador com Sucesso"), TestPriority(2)]
        [Trait("Categoria", "Admnistrador")]
        public async Task Administrador_Login_DeveRetornarComSucesso()
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

        [Fact(DisplayName = "Adicionar Curso com Sucesso"), TestPriority(3)]
        [Trait("Categoria", "Admnistrador")]
        public async Task Administrador_AdicionarCurso_DeveRetornarComSucesso()
        {
            // Arrange  
            var curso = new CursoDto
            {
                Nome = "Dominando os Testes de Software",
                Conteudo = "Teste",
                Valor = 1500
            };

            _testsfixture.Client.AtribuirBearerToken(_testsfixture.TokenUsuario);

            // Act
            var postResponse = await _testsfixture.Client.PostAsJsonAsync("api/cursos", curso);

            //Assert
            postResponse.EnsureSuccessStatusCode();            
        }

        [Fact(DisplayName = "Adicionar Aula com Sucesso"), TestPriority(4)]
        [Trait("Categoria", "Admnistrador")]
        public async Task Administrador_AdicionarAula_DeveRetornarComSucesso()
        {
            // Arrange  
            _testsfixture.Client.AtribuirBearerToken(_testsfixture.TokenUsuario);

            var response = await _testsfixture.Client.GetAsync("api/cursos");
            var cursos = await response.Content.ReadFromJsonAsync<CursoTestDto>();

            if (cursos?.data == null || !cursos.data.Any())
            {
                throw new InvalidOperationException("Nenhum curso encontrado.");
            }

            var primeiroCurso = cursos.data.First();

            var aulas = new AulaDto
            {
                Titulo = "Teste de Unidade",
                Conteudo = "Teste de Unidade",
                CursoId = primeiroCurso.id
            };

            // Act
            var postResponse = await _testsfixture.Client.PostAsJsonAsync("api/aulas", aulas);

            //Assert
            postResponse.EnsureSuccessStatusCode();

            _testsfixture.CursoId = primeiroCurso.id;
        }
    }
}
