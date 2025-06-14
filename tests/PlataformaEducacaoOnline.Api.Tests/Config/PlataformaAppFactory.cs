using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using PlataformaEducacaoOnline.Api.Tests;

namespace PlataformaEducacaoOnline.Api.Tests.Config
{
    public class PlataformaAppFactory : WebApplicationFactory<Program> 
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {   
            builder.UseEnvironment("Testing");
            base.ConfigureWebHost(builder);
        }
    }
}
