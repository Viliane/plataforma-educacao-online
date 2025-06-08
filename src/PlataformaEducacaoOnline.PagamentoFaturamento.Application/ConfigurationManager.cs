namespace PlataformaEducacaoOnline.PagamentoFaturamento.AntiCorruption
{
    public class ConfigurationManager : IConfigurationManager
    {
        public string GetValue(string node)
        {
            return new string(Enumerable.Repeat("ABCDEFGHIJKLMNOPQRSTUVXZ0123456789", 10).Select(s => s[new Random().Next(s.Length)]).ToArray());
        }
    }
}
