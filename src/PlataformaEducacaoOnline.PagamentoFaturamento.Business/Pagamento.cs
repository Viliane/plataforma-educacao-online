using PlataformaEducacaoOnline.Core.DomainObjects;

namespace PlataformaEducacaoOnline.PagamentoFaturamento.Business
{
    public class Pagamento : Entity, IAggregateRoot
    {
        public Guid MatriculaId { get; set; }
        public Guid CursoId { get; set; }
        public Guid AlunoId { get; set; }
        public string Status { get; set; }
        public decimal Valor { get; set; }
        public DadosCartao DadosCartao { get; set; } = new DadosCartao();

        public Pagamento() { }
        // EF
        public Transacao Transacao { get; set; }
    }
}
