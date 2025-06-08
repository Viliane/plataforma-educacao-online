using PlataformaEducacaoOnline.Core.DomainObjects;

namespace PlataformaEducacaoOnline.PagamentoFaturamento.Business
{
    public class Transacao : Entity
    {   
        public Guid PagamentoId { get; set; }
        public Guid MatriculaId { get; set; }
        public Guid AlunoId { get; set; }
        public Guid CursoId { get; set; }
        public decimal Valor { get; set; }
        public StatusTransacao StatusTransacao { get; set; }

        // EF
        public Transacao() { }
        public Pagamento Pagamento { get; set; }
    }
}
