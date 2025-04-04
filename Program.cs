using Microsoft.EntityFrameworkCore;
using Practica_ORM.Context; // Aquí tienes tu clase EscuelaDbContext
using Practica_ORM.Entities;
using Practica_ORM.Seeders;
using Practica_ORM.Services;

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

    /// Llamamos a nuestro nuevo servicio Estudiantes con sus documentos
    var estudianteService = new EstudianteService(context);
    estudianteService.MostrarEstudiantesConDocumento();

    /// Llamamos a nuestro nuevo servicio Profesor con sus clases
    var profesorService = new ProfesorService(context);
    profesorService.MostrarProfesorConSusClases();

    /// Llamamos a nuestro nuevo Servicio Clase con sus alumnos
    var claseService = new ClaseService(context);
    claseService.MostrarClasesConSusAlumnos();

    



    var mayoresde20 = context.Estudiantes
    .Where(e => e.Edad > 20)             // Filtro
    .Include(e => e.Documento)           // Debe ser Documento con D mayúscula
    .ToList();

    Console.WriteLine("Estudiantes mayores de 20 años:");

    foreach (var estudiante in mayoresde20)
    {
        Console.WriteLine("Nombre Alumno: " + estudiante.Nombre);
        Console.WriteLine("Edad: " + estudiante.Edad);
        Console.WriteLine("Número de Documento: " + estudiante.Documento.NumeroDocumento); /// También con D mayúscula
        Console.WriteLine("-----------------------------");
    }

    var clasesHistoria = context.Clases
    .Where(c => c.NombreClase == "Historia")
    .Include(c => c.Profesor)
    .ToList();

    foreach (var clase in clasesHistoria)
    {
        Console.WriteLine("Nombre De la clase:" + clase.NombreClase);
        Console.WriteLine("Que profesor la imaparte:"+ clase.Profesor.NombreProfesor);
        Console.WriteLine("------------------------------");
    }
    }

/// <summary>
/// Finalmente, ejecutas la aplicación. 
/// En una API, aquí empieza a escuchar peticiones HTTP
/// </summary>
app.Run();