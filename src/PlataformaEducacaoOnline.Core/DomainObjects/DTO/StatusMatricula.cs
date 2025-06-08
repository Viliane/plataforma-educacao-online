using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaEducacaoOnline.Core.DomainObjects.DTO
{
    public enum StatusMatricula
    {
        Inicio = 0,
        Ativa = 1,
        PendentePagamento = 2,
        Cancelada = 3,
        Concluida = 4
    }
}
