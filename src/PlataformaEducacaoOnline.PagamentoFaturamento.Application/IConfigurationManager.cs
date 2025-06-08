using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaEducacaoOnline.PagamentoFaturamento.AntiCorruption
{
    public interface IConfigurationManager
    {
        string GetValue(string node);
    }
}
