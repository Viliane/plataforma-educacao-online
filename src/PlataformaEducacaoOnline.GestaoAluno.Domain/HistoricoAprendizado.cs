using PlataformaEducacaoOnline.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaEducacaoOnline.GestaoAluno.Domain
{
    public class HistoricoAprendizado
    {
        public DateTime? DataRegistro { get; private set; }

        public string Descricao { get; private set; }

        public HistoricoAprendizado(string descricao)
        {
            Descricao = descricao;
            DataRegistro = DateTime.UtcNow;
        }
    }
}
