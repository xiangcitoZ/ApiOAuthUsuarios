using ApiOAuthUsuarios;
using ApiOAuthUsuarios.Data;
using ApiOAuthUsuarios.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddSingleton<HelperOAuthToken>();

HelperOAuthToken helper = new HelperOAuthToken(builder.Configuration);

builder.Services.AddAuthentication(helper.GetAuthenticationOptions())
    .AddJwtBearer(helper.GetJwtOptions());

// Add services to the container.

string conectionString =
 builder.Configuration.GetConnectionString("SqlAzure");
builder.Services.AddTransient<RepositoryUsuarios>();
builder.Services.AddDbContext<UsuariosContext>
 (options => options.UseSqlServer(conectionString));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API TUTORIAL ",
        Version = "v1",
        Description = "TUTORIAL DE API SEGURIDAD"

    });
});
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json"
        , "API TUTORIAL");
    options.RoutePrefix = "";
});

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
