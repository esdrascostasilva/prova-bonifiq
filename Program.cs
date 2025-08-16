using Microsoft.EntityFrameworkCore;
using ProvaPub.Repository;
using ProvaPub.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// builder.Services.AddSingleton<RandomService>();
// Alterado o ciclo do objeto para scoped para que a cada requisicao ele gerasse uma instancia diferente do objeto, com isso conseguir
// gerar os numeros aleatorios para salvar no banco
builder.Services.AddScoped<RandomService>();

// Registrando os sevicos de DI
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<CustomerService>();

builder.Services.AddDbContext<TestDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("ctx")));
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
