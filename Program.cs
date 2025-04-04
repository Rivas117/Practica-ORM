using Microsoft.EntityFrameworkCore;
using Practica_ORM.Context; // Aquí tienes tu clase EscuelaDbContext
using Practica_ORM.Seeders;

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
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<EscuelaDbContext>();
    DataSeeder.Seed(context);

    // Consultamos los estudiantes con su documento relacionado (relación 1:1)
    var estudiantes = context.Estudiantes
        .Include(e => e.Documento)   // Esto le dice a EF que haga JOIN con la tabla Documento
        .ToList();                   // Ejecuta la consulta y trae los resultados a memoria

    // Recorremos la lista y mostramos los datos
    foreach (var estudiante in estudiantes)
    {
        Console.WriteLine("Nombre del estudiante: " + estudiante.Nombre);
        Console.WriteLine("Edad del estudiante: " + estudiante.Edad);
        Console.WriteLine("Número de documento: " + estudiante.Documento.NumeroDocumento);
        Console.WriteLine("-------------------------------");
    }

    // Aqui hacemos la relacion1;m de los profesores
    // la primera parte es igual a la primera de 1;1 pero ya 
    var profesores = context.Profesores
    .Include(p => p.Clases)
    .ToList();

    foreach (var profesor in profesores)
    {
        Console.WriteLine("Nombre del profesor: " + profesor.NombreProfesor);
        Console.WriteLine("Clases:");

        // aqui hacemos lo de 1:m para relacionar prosefor dando muchas clases
        foreach (var clase in profesor.Clases)
        {
            Console.WriteLine("   - " + clase.NombreClase);
        }

        Console.WriteLine("----------------------------");
    }

    // 🧠 Consulta de relación N:M → Clases con estudiantes inscritos
    var clases = context.Clases
        .Include(c => c.EstudianteClases)         // ← Cargamos la tabla puente
        .ThenInclude(ec => ec.Estudiante)         // ← Desde la tabla puente, cargamos cada estudiante
        .ToList();

        foreach (var clase in clases)
        {
            Console.WriteLine("Clase:"+ clase.NombreClase);
            Console.WriteLine("Estudiantes:");

            foreach (var ec in clase.EstudianteClases)
            {
                Console.WriteLine("   - "+ ec.Estudiante.Nombre);
                
            }
            Console.WriteLine("----------------------------");
        }
}

/// <summary>
/// Finalmente, ejecutas la aplicación. 
/// En una API, aquí empieza a escuchar peticiones HTTP
/// </summary>
app.Run();