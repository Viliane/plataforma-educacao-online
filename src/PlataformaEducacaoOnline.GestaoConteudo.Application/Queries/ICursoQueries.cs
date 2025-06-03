using PlataformaEducacaoOnline.GestaoConteudo.Application.Queries.DTO;


namespace PlataformaEducacaoOnline.GestaoConteudo.Application.Queries
{
    public interface ICursoQueries
    {
        Task<CursoQueryDto?> ObterCursoPorId(Guid cursoId);
        Task<IEnumerable<CursoQueryDto>> ObterTodosCursos();
        //Task<AulaDto?> ObterAulaPorId(Guid aulaId);
        //Task<IEnumerable<AulaDto>> ObterAulasPorCursoId(Guid cursoId);
        //Task<IEnumerable<AulaDto>> ObterTodasAulas();
    }
}
