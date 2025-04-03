using Microsoft.EntityFrameworkCore;
using Practica_ORM.Context; // Aquí tienes tu clase EscuelaDbContext

/// <summary>
/// Minimal Hosting Model
/// Esta sola línea es como decirle a .NET: 
/// "quiero crear y configurar una aplicación web
/// (o de consola con servicios), y necesito una herramienta para construirla paso a paso."
/// </summary>
var builder = WebApplication.CreateBuilder(args);

// 👉 Aquí defines la cadena de conexión a tu base de datos MySQL
var connectionString = "server=localhost;port=3306;database=Practica_ORM;user=root;password=";

// 👉 Registramos el DbContext con EF Core usando Pomelo
builder.Services.AddDbContext<EscuelaDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

/// <summary>
/// Una vez que has configurado lo indicado con el builder, haces .Build()
/// para armar toda la aplicación con los servicios listos para usar.
/// </summary>
var app = builder.Build();

/// <summary>
/// Finalmente, ejecutas la aplicación. 
/// En una API, aquí empieza a escuchar peticiones HTTP
/// </summary>
app.Run();
