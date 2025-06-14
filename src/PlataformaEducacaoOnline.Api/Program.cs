using PlataformaEducacaoOnline.Api.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.AddDbContextConfiguration()
       .AddResolveDependencie()
       .AddIdentityConfiguration()
       .AddSwaggerConfiguration()
       .AddApiConfiguration()
       .AddJwtBearerAutentication();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.UseDbMigrationHelper();

app.Run();

public partial class Program { }
