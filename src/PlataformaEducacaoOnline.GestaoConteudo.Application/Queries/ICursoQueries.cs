using PlataformaEducacaoOnline.GestaoConteudo.Application.Queries.DTO;
using PlataformaEducacaoOnline.GestaoConteudo.Domain;


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

    public class CursoQueries : ICursoQueries
    {
        private readonly ICursoRepositoty _cursoRepository;
        public CursoQueries(ICursoRepositoty cursoRepository)
        {
            _cursoRepository = cursoRepository;
        }

        public async Task<CursoQueryDto?> ObterCursoPorId(Guid cursoId)
        {
            var curso = await _cursoRepository.ObterCursoPorId(cursoId);
            return curso is null ? null : 
                new CursoQueryDto { 
                    Id = cursoId, 
                    Nome = curso.Nome, 
                    Conteudo = curso.ConteudoProgramatico.DescricaoConteudoProgramatico, 
                    DataAtualizacaoConteudo = curso.ConteudoProgramatico.DataAtualizacaoConteudoProgramatico,
                    UsuarioId = curso.UsuarioId,
                    DataCadastro = curso.DataCadastro,
                    DataAtualizacao = curso.DataAtualizacao,
                    Valor = curso.Valor 
                };
        }

        public async Task<IEnumerable<CursoQueryDto>> ObterTodosCursos()
        {
            var cursos = await _cursoRepository.ObterTodosCursos();
            return cursos.Select(c => new CursoQueryDto { 
                Id = c.Id,
                Nome = c.Nome,
                Conteudo = c.ConteudoProgramatico.DescricaoConteudoProgramatico,
                DataAtualizacaoConteudo = c.ConteudoProgramatico.DataAtualizacaoConteudoProgramatico,
                UsuarioId = c.UsuarioId,
                DataCadastro = c.DataCadastro,
                DataAtualizacao = c.DataAtualizacao,
                Valor = c.Valor
            });
        }

        //public async Task<AulaDto?> ObterAulaPorId(Guid aulaId)
        //{
        //    var aula = await _cursoRepository.ObterAulaPorId(aulaId);
        //    return aula == null ? null : new AulaDto { Titulo = aula.Titulo, Descricao = aula.Descricao, Duracao = aula.Duracao };
        //}
        //public async Task<IEnumerable<AulaDto>> ObterAulasPorCursoId(Guid cursoId)
        //{
        //    var aulas = await _cursoRepository.ObterAulasPorCursoId(cursoId);
        //    return aulas.Select(a => new AulaDto { Titulo = a.Titulo, Descricao = a.Descricao, Duracao = a.Duracao });
        //}
        //public async Task<IEnumerable<AulaDto>> ObterTodasAulas()
        //{
        //    var aulas = await _cursoRepository.ObterTodasAulas();
        //    return aulas.Select(a => new AulaDto { Titulo = a.Titulo, Descricao = a.Descricao, Duracao = a.Duracao });
        //}
    }
}
