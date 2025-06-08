namespace PlataformaEducacaoOnline.Api.DTO
{
    public class PagamentoMatriculaDto
    {
        public Guid CursoId { get; set; }
        public Guid AlunoId { get; set; }
        public decimal Valor { get; set; }
        public string NomeCartao { get; set; }
        public string NumeroCartao { get; set; }
        public string ExpiracaoCartao { get; set; }
        public string CvvCartao { get; set; }
    }
}
