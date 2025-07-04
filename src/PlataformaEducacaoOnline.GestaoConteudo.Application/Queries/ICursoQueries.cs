﻿using PlataformaEducacaoOnline.GestaoConteudo.Application.Queries.DTO;


namespace PlataformaEducacaoOnline.GestaoConteudo.Application.Queries
{
    public interface ICursoQueries
    {
        Task<CursoQueryDto?> ObterCursoPorId(Guid cursoId);
        Task<IEnumerable<CursoQueryDto>> ObterTodosCursos();
        Task<AulaQueryDto?> ObterAulaPorId(Guid aulaId);        
        Task<IEnumerable<AulaQueryDto>> ObterTodasAulas();
        Task<IEnumerable<AulaQueryDto>> ObterAulasPorCursoId(Guid cursoId);
        Task<IEnumerable<EvolucaoAulaQueryDto>> ObterEvolucaoAulaPorUsuarioIdCursoId(Guid usuarioId, Guid cursoId);
    }
}
