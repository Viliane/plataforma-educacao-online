using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PlataformaEducacaoOnline.Api.Data;
using System;

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

            await context.Database.MigrateAsync();
            await InserirDadosIniciais(context);
        }

        private static async Task InserirDadosIniciais(AppDbContext context)
        {
            if (context.Users.Any()) return;

            #region Criação Administrador

            var UserAdminId = Guid.NewGuid().ToString();

            var adminIdentity = new IdentityUser()
            {
                Id = UserAdminId,
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
                UserId = UserAdminId,
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
    }
}
