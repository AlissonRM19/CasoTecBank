using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Habilita los controladores
builder.Services.AddControllers();

// Habilita CORS para permitir conexiones desde la app móvil y web
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Aquí podés agregar servicios personalizados como:
// builder.Services.AddSingleton<ClienteService>();
// builder.Services.AddSingleton<JsonStorageService>();

var app = builder.Build();

app.UseCors("AllowAll");

app.UseRouting();

// Activar endpoints de controladores
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
