namespace PlataformaEducacaoOnline.Api.DTO
{
    public class AulaDto
    {
        public string Titulo { get; set; }
        public string Conteudo { get; set; }
        public Guid CursoId { get; set; }
        public List<MaterialDto> Materiais { get; set; } = new List<MaterialDto>();
    }
}
