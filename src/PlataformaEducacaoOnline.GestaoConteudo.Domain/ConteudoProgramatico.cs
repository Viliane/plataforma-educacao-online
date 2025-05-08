using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaEducacaoOnline.GestaoConteudo.Domain
{
    public class ConteudoProgramatico
    {
        public int Codigo { get; private set; }
        public string Descricao { get; private set; }

        public ConteudoProgramatico(int codigo, string descricao)
        {
            Codigo = codigo;
            Descricao = descricao;
        }

        public override string ToString()
        {
            return $"{Descricao} - {Codigo}";
        }
    }
}
