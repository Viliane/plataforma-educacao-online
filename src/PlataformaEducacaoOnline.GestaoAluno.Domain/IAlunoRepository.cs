using PlataformaEducacaoOnline.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaEducacaoOnline.GestaoAluno.Domain
{
    public interface IAlunoRepository : IRepository<Aluno>
    {
        void Adicionar(Aluno aluno);
    }
}
