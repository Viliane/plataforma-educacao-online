using PlataformaEducacaoOnline.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaEducacaoOnline.GestaoConteudo.Domain
{
    public class ConteudoProgramatico
    {
        public string DescricaoConteudoProgramatico { get; private set; }
        public DateTime DataAtualizacaoConteudoProgramatico { get; private set; }

        public ConteudoProgramatico(string descricaoConteudoProgramatico)
        {
            DescricaoConteudoProgramatico = descricaoConteudoProgramatico;
            DataAtualizacaoConteudoProgramatico = DateTime.Now;

            Validar();
        }

        public void Validar()
        {
            if (string.IsNullOrEmpty(DescricaoConteudoProgramatico))
                throw new DomainException("A descrição do conteúdo programático é obrigatória.");
        }
    }
}
