using PlataformaEducacaoOnline.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaEducacaoOnline.GestaoAluno.Domain
{
    public class Matricula : Entity
    {
        public Guid CursoId { get; private set; }
        public Guid AlunoId { get; private set; }

        public StatusMatricula StatusMatricula { get; private set; }

        public Matricula(Guid cursoId, Guid alunoId)
        {
            CursoId = cursoId;
            AlunoId = alunoId;

            Validar();
            InicioMatricula();
        }

        public void Validar()
        {
            if (CursoId == Guid.Empty)
                throw new DomainException("CursoId não pode ser vazio.");
            if (AlunoId == Guid.Empty)
                throw new DomainException("AlunoId não pode ser vazio.");
        }

        public void InicioMatricula()
        {
            StatusMatricula = StatusMatricula.Inicio;
        }

        public void AtivarMatricula()
        {
            StatusMatricula = StatusMatricula.Ativa;
        }

        public void PendentePagamento()
        {
            StatusMatricula = StatusMatricula.PendentePagamento;
        }
    }
}
