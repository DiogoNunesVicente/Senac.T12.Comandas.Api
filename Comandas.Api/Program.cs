using Microsoft.EntityFrameworkCore;
using SistemadeComandas.BancoDeDados;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var conexao = builder.Configuration.GetConnectionString("conexao");
builder.Services.AddDbContext<ComandaContexto>(opt =>
{
    opt.UseMySql(conexao,ServerVersion.Parse("10.4.24-MariaDB"));
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
