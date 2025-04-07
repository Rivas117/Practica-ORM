using Practica_ORM.Entities;
using Practica_ORM.interfaces;

namespace Practica_ORM.Execution;

public class AppExecutor
{
    private readonly IEstudianteService _estudianteService;
    private readonly IProfesorService _profesorService;
    private readonly IClaseService _claseService;

    public AppExecutor(IServiceProvider serviceProvider)
    {
        _estudianteService = serviceProvider.GetRequiredService<IEstudianteService>();
        _profesorService = serviceProvider.GetRequiredService<IProfesorService>();
        _claseService = serviceProvider.GetRequiredService<IClaseService>();
    }

    public void Ejecutar()
    {
        // Crear nuevo estudiante
        var nuevoEstudiante = new Estudiante
        {
            Nombre = "Juan",
            Edad = 23,
            Documento = new Documento
            {
                NumeroDocumento = "ID123456"
            },
            EstudianteClases = new List<EstudianteClase>()
        };

        _estudianteService.AgregarEstudiante(nuevoEstudiante);

        // Buscar por ID
        var encontrado = _estudianteService.ObtenerEstudiantePorId(1);

        if (encontrado != null)
        {
            Console.WriteLine("Estudiante encontrado:");
            Console.WriteLine("Nombre: " + encontrado.Nombre);
            Console.WriteLine("Edad: " + encontrado.Edad);
            Console.WriteLine("Documento: " + encontrado.Documento.NumeroDocumento);
        }
        else
        {
            Console.WriteLine("No se encontró el estudiante.");
        }

        var estudianteActualizado = new Estudiante
        {
            Id = 1,
            Nombre = "Luis Actulizado",
            Edad = 33,
            Documento = new Documento
            {
                NumeroDocumento = "ID+6"
            },
            EstudianteClases = new List<EstudianteClase>()
        };

        _estudianteService.ActualizarEstudiante(estudianteActualizado);

        // Eliminamos estudiante con ID 1 (por ejemplo)
        _estudianteService.EliminarEstudiante(1);


        //_profesorService.MostrarProfesorConSusClases();
        // _claseService.MostrarClasesConSusAlumnos();
    }
}