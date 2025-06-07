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
        public DateTime DataMatricula { get; private set; }
        public StatusMatricula StatusMatricula { get; private set; }

        //EF
        public Aluno Aluno { get; private set; } = null!;
        public Certificado Certificado { get; private set; } = null!;
        protected Matricula() { } // EF Constructor

        public Matricula(Guid cursoId, Guid alunoId)
        {
            CursoId = cursoId;
            AlunoId = alunoId;
            DataMatricula = DateTime.UtcNow;

            Validar();
            IniciarMatricula();
        }

        public void Validar()
        {
            if (CursoId == Guid.Empty)
                throw new DomainException("CursoId não pode ser vazio.");
            if (AlunoId == Guid.Empty)
                throw new DomainException("AlunoId não pode ser vazio.");
        }

        public void IniciarMatricula()
        {
            StatusMatricula = StatusMatricula.PendentePagamento;
        }

        public void AtivarMatricula()
        {
            StatusMatricula = StatusMatricula.Ativa;
        }

        public void CancelarMatricula()
        {
            if (StatusMatricula == StatusMatricula.Concluida)
                throw new DomainException("Não é possível cancelar uma matrícula já concluída.");
            StatusMatricula = StatusMatricula.Cancelada;
        }

        public void ConcluirCurso()
        {
            if (StatusMatricula != StatusMatricula.Ativa)
                throw new DomainException("A matrícula deve estar ativa para ser concluída.");
            StatusMatricula = StatusMatricula.Concluida;
        }
    }
}
