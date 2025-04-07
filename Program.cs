using Microsoft.EntityFrameworkCore;
using Practica_ORM.Context; // Aquí tienes tu clase EscuelaDbContext
using Practica_ORM.Entities;
using Practica_ORM.interfaces;
using Practica_ORM.Seeders;
using Practica_ORM.Services;
using Practica_ORM.Execution;

/// <summary>
/// Minimal Hosting Model
/// Esta sola línea es como decirle a .NET: 
/// "quiero crear y configurar una aplicación web
/// (o de consola con servicios), y necesito una herramienta para construirla paso a paso."
/// </summary>
var builder = WebApplication.CreateBuilder(args);

// Aquí defines la cadena de conexión a tu base de datos MySQL
var connectionString = "server=localhost;port=3306;database=Practica_ORM;user=root;password=";

// Registramos el DbContext con EF Core usando Pomelo
builder.Services.AddDbContext<EscuelaDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));


/// <summary>
/// Creamos Scope de servicio
/// = Es como decir Dame permiso a la cocina del sistema para ver los 
/// Servicios registrados hasta ahora
/// 
/// Haciedo que tambien mediante Scope Haciendo que viva cuanto tiempo se necesite
/// Haciendo que despues de usarse se limpie en memoria
/// 
/// DataSeeder.Seed sive para agregar informacion a la bd
/// y verifica si hay datos o no
/// </summary>

builder.Services.AddScoped<IEstudianteService, EstudianteService>();
builder.Services.AddScoped<IProfesorService, ProfesorService>();
builder.Services.AddScoped<IClaseService, ClaseService>();
builder.Services.AddControllers();

/// <summary>
/// Una vez que has configurado lo indicado con el builder, haces .Build()
/// para armar toda la aplicación con los servicios listos para usar.
/// </summary>
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<EscuelaDbContext>();
    DataSeeder.Seed(context);

    var executor = new AppExecutor(scope.ServiceProvider); // NUEVO
    executor.Ejecutar(); // Aquí vive la lógica de prueba
}

app.MapControllers();
/// <summary>
/// Finalmente, ejecutas la aplicación. 
/// En una API, aquí empieza a escuchar peticiones HTTP
/// </summary>
app.Run();