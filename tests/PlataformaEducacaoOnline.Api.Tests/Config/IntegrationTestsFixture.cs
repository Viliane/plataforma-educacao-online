using Bogus;
using Bogus.DataSets;
using Microsoft.AspNetCore.Mvc.Testing;
using PlataformaEducacaoOnline.Api.DTO;
using PlataformaEducacaoOnline.Api.Tests.DTO;
using PlataformaEducacaoOnline.PagamentoFaturamento.Business;
using System.Text.Json;

namespace PlataformaEducacaoOnline.Api.Tests.Config
{
    [CollectionDefinition(nameof(IntegrationApiTestsFixtureCollection))]

    public class IntegrationApiTestsFixtureCollection : ICollectionFixture<IntegrationTestsFixture> { }

    public class IntegrationTestsFixture : IDisposable
    {
        public string usuarioEmail { get; set; } = string.Empty;
        public string usuarioSenha { get; set; } = string.Empty;
        public string usuarioNome { get; set; } = string.Empty;
        public string usuarioConfirmasenha { get; set; } = string.Empty;
        public string TokenUsuario { get; set; } = string.Empty;
        public Guid UsuarioId { get; set; } = Guid.Empty;
        public Guid CursoId { get; set; } = Guid.Empty;
        public Guid AulaId { get; set; } = Guid.Empty;
        public string NomeCartao { get; set; } = string.Empty;
        public string NumeroCartao { get; set; } = string.Empty;
        public string ExpiracaoCartao { get; set; } = string.Empty;
        public string CvvCartao { get; set; } = string.Empty;

        public readonly PlataformaAppFactory Factory;
        public HttpClient Client;

        public IntegrationTestsFixture()
        {
            var clientOptions = new WebApplicationFactoryClientOptions
            {
                BaseAddress = new Uri("http://localhost:5016")
            };

            Factory = new PlataformaAppFactory();
            Client = Factory.CreateClient(clientOptions);
        }

        public void GerarDadosUsuario()
        {
            var faker = new Faker("pt_BR");
            usuarioEmail = faker.Internet.Email().ToLower();
            usuarioNome = faker.Name.FirstName();
            usuarioSenha = faker.Internet.Password(8, false, "", "@1Ab_");
            usuarioConfirmasenha = usuarioSenha;
        }

        public void SalvarTokenUsuario(string token)
        {
            var response = JsonSerializer.Deserialize<ResponseDataDto>(token,
                 new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }) ?? new ResponseDataDto();
            TokenUsuario = response.Data.AccessToken;
            UsuarioId = Guid.Parse(response.Data.UserToken.Id);
            usuarioEmail = response.Data.UserToken.Email;
        }

        public void GerarDadosCartao()
        {
            var faker = new Faker("pt_BR");
            NomeCartao = faker.Name.FullName();
            NumeroCartao = new string(faker.Random.Replace("################"));
            ExpiracaoCartao = faker.Date.Future(1, DateTime.Now).ToString("MM/yy");
            CvvCartao = faker.Finance.CreditCardCvv();
        }

        public void Dispose()
        {
            Client.Dispose();
            Factory.Dispose();
        }
    }
}
