using Abstracciones.Interfaces.Flujo;
using Flujo;
using DA;
using Abstracciones.Interfaces.DA;
using DA.Repositorios;
using Servicios;
using Abstracciones.Interfaces.Servicios;
using Abstracciones.Interfaces.Reglas;
using Reglas;
using Abstracciones.Interfaces.API;
using API.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();

builder.Services.AddScoped<IFavoritoFlujo, FavoritoFlujo>();
builder.Services.AddScoped<IFavoritoDA, FavoritoDA>();
builder.Services.AddScoped<IListaVisualizacionFlujo, ListaVisualizacionFlujo>();
builder.Services.AddScoped<IListaVisualizacionDA, ListaVisualizacionDA>();
builder.Services.AddScoped<IUsuarioFlujo, UsuarioFlujo>();
builder.Services.AddScoped<IUsuarioDA, UsuarioDA>();
builder.Services.AddScoped<IRepositorioDapper, RepositorioDapper>();
builder.Services.AddScoped<ITMDBServicio, TMDBServicio>();
builder.Services.AddScoped<ITMDBServicio, TMDBServicio>();
builder.Services.AddScoped<ITMDBReglas, TMDBReglas>();
builder.Services.AddScoped<ITMDBFlujo, TMDBFlujo>();
builder.Services.AddScoped<IValidacionPuntuacion, ValidacionPuntuacion>();
builder.Services.AddScoped<IConfiguracion, Configuracion>();

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
