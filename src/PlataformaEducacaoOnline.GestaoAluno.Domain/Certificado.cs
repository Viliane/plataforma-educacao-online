using PlataformaEducacaoOnline.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaEducacaoOnline.GestaoAluno.Domain
{
    public class Certificado : Entity
    {
        public Guid MatriculaId { get; private set; }
        public Guid AlunoId { get; private set; }
        public DateTime DataEmissao { get; private set; }

        //EF
        public Matricula Matricula { get; private set; }
        public Aluno Aluno { get; private set; }

        public Certificado(Guid matriculaId, Guid alunoId)
        {
            MatriculaId = matriculaId;
            AlunoId = alunoId;
            DataEmissao = DateTime.UtcNow;

            Validar();
        }
        public void Validar()
        {
            if (MatriculaId == Guid.Empty)
                throw new DomainException("MatriculaId não pode ser vazio.");
            if (AlunoId == Guid.Empty)
                throw new DomainException("AlunoId não pode ser vazio.");
        }

        public byte[] EmitirCertificado(ICertificadoPdfGenerator pdfGenerator)
        {
            if (pdfGenerator == null) throw new ArgumentNullException(nameof(pdfGenerator));
                return pdfGenerator.GerarPdf(this);
        }
    }
}
