using PlataformaEducacaoOnline.GestaoConteudo.Application.Queries.DTO;
using PlataformaEducacaoOnline.GestaoConteudo.Domain;


namespace PlataformaEducacaoOnline.GestaoConteudo.Application.Queries
{
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

        public async Task<AulaQueryDto?> ObterAulaPorId(Guid aulaId)
        {
            var aula = await _cursoRepository.ObterAulaPorId(aulaId);
            if (aula is null) return null;

            var materiais = await _cursoRepository.ObterMateriaisPorAulaId(aulaId);
            return new AulaQueryDto
            {
                Id = aula.Id,
                Titulo = aula.Titulo,
                Conteudo = aula.Conteudo,
                CursoId = aula.CursoId,
                Materiais = materiais.Select(m => new Material(m.Id, m.Nome, m.AulaId)).ToList()
            };
        }

        public Task<IEnumerable<AulaQueryDto>> ObterTodasAulas()
        {
            var aulas = _cursoRepository.ObterTodasAulas();
            return aulas.ContinueWith(task => task.Result.Select(aula => new AulaQueryDto
            {
                Id = aula.Id,
                Titulo = aula.Titulo,
                Conteudo = aula.Conteudo,
                CursoId = aula.CursoId,
                Materiais = _cursoRepository.ObterMateriaisPorAulaId(aula.Id).Result.Select(m => new Material(m.Id, m.Nome, m.AulaId)).ToList()
            }));
        }

        public Task<IEnumerable<AulaQueryDto>> ObterAulasPorCursoId(Guid cursoId)
        {
            var aulas = _cursoRepository.ObterAulasPorCursoId(cursoId);
            return aulas.ContinueWith(task => task.Result.Select(aula => new AulaQueryDto
            {
                Id = aula.Id,
                Titulo = aula.Titulo,
                Conteudo = aula.Conteudo,
                CursoId = aula.CursoId,
                Materiais = _cursoRepository.ObterMateriaisPorAulaId(aula.Id).Result.Select(m => new Material(m.Id, m.Nome, m.AulaId)).ToList()
            }));
        }

        public async Task<IEnumerable<EvolucaoAulaQueryDto>> ObterEvolucaoAulaPorUsuarioIdCursoId(Guid usuarioId, Guid cursoId)
        {
            var evolucoes = await _cursoRepository.ObterEvolucaoAulaPorUsuarioIdCursoId(usuarioId, cursoId);
            return evolucoes.Select(e => new EvolucaoAulaQueryDto
            {
                Id = e.Id,
                AulaId = e.AulaId,
                UsuarioId = e.UsuarioId,
                CursoId = e.CursoId,
                Status = (int)e.Status
            });
        }
    }
}
