using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;

namespace PlataformaEducacaoOnline.Api.Tests.Config
{
    public static class TestsExtensions
    {
        public static void AtribuirJsonMediaType(this HttpClient client)
        {
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public static void AtribuirBearerToken(this HttpClient client, string token)
        {
            if (string.IsNullOrEmpty(token)) return;
            client.AtribuirJsonMediaType();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
    }
}
