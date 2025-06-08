using PlataformaEducacaoOnline.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaEducacaoOnline.GestaoConteudo.Domain
{
    public interface ICursoRepositoty : IRepository<Curso>
    {
        Task<Curso?> ObterCursoPorId(Guid cursoId);
        Task<IEnumerable<Curso>> ObterTodosCursos();        
        Task<Aula?> ObterAulaPorId(Guid aulaId);
        Task<IEnumerable<Aula>> ObterAulasPorCursoId(Guid cursoId);
        Task<IEnumerable<Material>> ObterMateriaisPorAulaId(Guid aulaId);
        Task<Material?> ObterMaterialPorId(Guid materialId);
        Task<IEnumerable<Aula>> ObterTodasAulas();
        void Adicionar(Curso curso);
        void Atualizar(Curso curso);
        void Remover(Curso curso);
        void Adicionar(Aula aula);
        void Atualizar(Aula aula);
        void Remover(Aula aula);
        void Remover(Material material);
        void AdicionarEvolucaoAula(EvolucaoAula aulaEvolucao);
        Task<EvolucaoAula?> ObterEvolucaoAulaPorAulaIdUsuarioId(Guid aulaId, Guid usuarioId);
        void Atualizar(EvolucaoAula evolucaoAula);
        Task<IEnumerable<EvolucaoAula>> ObterEvolucaoAulaPorUsuarioIdCursoId(Guid usuarioId, Guid cursoId);
    }
}
