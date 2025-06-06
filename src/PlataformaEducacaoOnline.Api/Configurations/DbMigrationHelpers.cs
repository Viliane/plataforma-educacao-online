using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PlataformaEducacaoOnline.Api.Data;
using PlataformaEducacaoOnline.GestaoAluno.Data;
using PlataformaEducacaoOnline.GestaoConteudo.Data;
using PlataformaEducacaoOnline.GestaoConteudo.Domain;

namespace PlataformaEducacaoOnline.Api.Configurations
{
    public static class DbMigrationHelpers
    {
        public static void UseDbMigrationHelper(this WebApplication app)
        {
            EnsureSeedData(app).Wait();
        }

        public static async Task EnsureSeedData(WebApplication application)
        {
            var service = application.Services.CreateScope().ServiceProvider;
            await EnsureSeedData(service);
        }

        public static async Task EnsureSeedData(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var contextGestaoConteudo = scope.ServiceProvider.GetRequiredService<GestaoConteudoContext>();
            var contextGestaoAluno = scope.ServiceProvider.GetRequiredService<GestaoAlunoContext>();

            await context.Database.MigrateAsync();
            await contextGestaoConteudo.Database.MigrateAsync();
            await contextGestaoAluno.Database.MigrateAsync();

            await InserirDadosIniciais(context);
            await InserirCursoAulaMaterial(context, contextGestaoConteudo);
        }

        private static async Task InserirDadosIniciais(AppDbContext context)
        {
            if (context.Users.Any()) return;

            #region Criação Administrador

            var UserAdminId = Guid.NewGuid();

            var adminIdentity = new IdentityUser()
            {
                Id = UserAdminId.ToString(),
                Email = "adminTeste@teste.com",
                EmailConfirmed = true,
                NormalizedEmail = "ADMINTESTE@TESTE.COM",
                UserName = "adminTeste@teste.com",
                AccessFailedCount = 0,
                PasswordHash = "AQAAAAIAAYagAAAAEEIsmFcI2MWZaRj7qTXrXoXatvLIeahf+yFJbb3pAI6SbmCFjHJtKz1Nxv0XOvhuQQ==",
                NormalizedUserName = "ADMINTESTE@TESTE.COM",
            };    

            await context.Users.AddAsync(adminIdentity);

            var RoleAdminId = "1";

            await context.Roles.AddAsync(new IdentityRole
            {
                Id = RoleAdminId,
                Name = "ADMIN",
                NormalizedName = "ADMIN",
                ConcurrencyStamp = null
            });

            await context.SaveChangesAsync();

            await context.UserRoles.AddAsync(new IdentityUserRole<string>
            {
                UserId = UserAdminId.ToString(),
                RoleId = RoleAdminId
            });

            await context.SaveChangesAsync();

            #endregion

            #region Criação Aluno

            var UserAlunoId = Guid.NewGuid().ToString();

            var alunoIdentity = new IdentityUser()
            {
                Id = UserAlunoId,
                Email = "alunoTeste@teste.com",
                EmailConfirmed = true,
                NormalizedEmail = "ALUNOTESTE@TESTE.COM",
                UserName = "alunoTeste@teste.com",
                AccessFailedCount = 0,
                PasswordHash = "AQAAAAIAAYagAAAAEEIsmFcI2MWZaRj7qTXrXoXatvLIeahf+yFJbb3pAI6SbmCFjHJtKz1Nxv0XOvhuQQ==",
                NormalizedUserName = "ALUNOTESTE@TESTE.COM",
            };

            await context.Users.AddAsync(alunoIdentity);

            var RoleAlunoId = "2";

            await context.Roles.AddAsync(new IdentityRole
            {
                Id = RoleAlunoId,
                Name = "ALUNO",
                NormalizedName = "ALUNO",
                ConcurrencyStamp = null
            });

            await context.SaveChangesAsync();

            await context.UserRoles.AddAsync(new IdentityUserRole<string>
            {
                UserId = UserAlunoId,
                RoleId = RoleAlunoId
            });

            await context.SaveChangesAsync();

            #endregion
        }

        private static async Task InserirCursoAulaMaterial(AppDbContext context, GestaoConteudoContext contextGestaoConteudo)
        {
            #region Criar Curso, Aula, Material

            if (contextGestaoConteudo.Set<Curso>().Any() || contextGestaoConteudo.Set<Aula>().Any())
                return;

            var userAdmin = await context.Users.FirstOrDefaultAsync(x => x.Email == "adminTeste@teste.com");

            var curso = new Curso("Curso de Teste", Guid.Parse(userAdmin.Id), new ConteudoProgramatico("Conteúdo Programático 1"), 200);

            contextGestaoConteudo.Cursos.Add(curso);

            await contextGestaoConteudo.SaveChangesAsync();

            var aula1 = new Aula("Aula 1", "Conteúdo da Aula 1", curso.Id);
            aula1.AdicionarMaterial(new Material("Aula 1 - Material 1"));
            aula1.AdicionarMaterial(new Material("Aula 1 - Material 2"));

            var aula2 = new Aula("Aula 2", "Conteúdo da Aula 2", curso.Id);
            aula2.AdicionarMaterial(new Material("Aula 2 - Material 1"));
            aula2.AdicionarMaterial(new Material("Aula 2 - Material 2"));

            contextGestaoConteudo.Aulas.AddRange(aula1, aula2);
            await contextGestaoConteudo.SaveChangesAsync();

            #endregion
        }
    }
}
