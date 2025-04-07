/// <summary>
/// Implementación del servicio de estudiantes que interactúa con la base de datos
/// usando Entity Framework Core para consultas y operaciones.
/// </summary>

using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
using Practica_ORM.Context;
using Practica_ORM.Entities;
using Practica_ORM.interfaces;

namespace Practica_ORM.Services;

public class EstudianteService : IEstudianteService
{
    private readonly EscuelaDbContext _context;

    public EstudianteService(EscuelaDbContext context)
    {
        _context = context;
    }

    public void MostrarEstudiantesConDocumento()
    {
        /// Consultamos los estudiantes con su documento relacionado (relación 1:1)
        var estudiantes = _context.Estudiantes
            .Include(e => e.Documento)   /// Esto le dice a EF que haga JOIN con la tabla Documento
            .ToList();                   /// Ejecuta la consulta y trae los resultados a memoria

        /// Recorremos la lista y mostramos los datos
        foreach (var estudiante in estudiantes)
        {
            Console.WriteLine("Nombre del estudiante: " + estudiante.Nombre);
            Console.WriteLine("Edad del estudiante: " + estudiante.Edad);
            Console.WriteLine("Número de documento: " + estudiante.Documento.NumeroDocumento);
            Console.WriteLine("-------------------------------");
        }
    }

    /// <summary>
    /// Muestra los estudiantes cuya edad es mayor a 20 años.
    /// Incluye el documento relacionado.
    /// </summary>
    public void MostrarEstudiantesMayoresDe20()
    {
        var mayores = _context.Estudiantes
            .Where(e => e.Edad > 20)
            .Include(e => e.Documento)
            .ToList();

        Console.WriteLine("Estudiantes mayores de 20 años:");

        foreach (var estudiante in mayores)
        {
            Console.WriteLine("Nombre Alumno: " + estudiante.Nombre);
            Console.WriteLine("Edad: " + estudiante.Edad);
            Console.WriteLine("Número de Documento: " + estudiante.Documento.NumeroDocumento);
            Console.WriteLine("-----------------------------");
        }
    }

    public void AgregarEstudiante(Estudiante estudiante)
    {
        _context.Estudiantes.Add(estudiante);
        _context.SaveChanges();

        Console.WriteLine("Estudiante agregado exitosamente.");
    }

    public Estudiante? ObtenerEstudiantePorId(int id)
    {
        return _context.Estudiantes
        .Include(e => e.Documento)
        .FirstOrDefault(e => e.Id == id);
    }

    public void ActualizarEstudiante(Estudiante estudiante)
    {
        var existente = _context.Estudiantes
        .Include(e => e.Documento)
        .FirstOrDefault(e => e.Id == estudiante.Id);

        if (existente != null)
        {
            // Modificamos los valores deseados
            existente.Nombre = estudiante.Nombre;
            existente.Edad = estudiante.Edad;
            existente.Documento.NumeroDocumento = estudiante.Documento.NumeroDocumento;

            _context.SaveChanges();

            Console.WriteLine("Estudiante actualizado correctamente.");
        }
        else
        {
            Console.WriteLine("Estudiante no encontrado para actualizar.");
        }
    }

    public void EliminarEstudiante(int id)
    {
        var estudiante = _context.Estudiantes
        .Include(e => e.Documento)
        .FirstOrDefault(e => e.Id == id);

        if (estudiante != null)
        {
            _context.Estudiantes.Remove(estudiante);
            _context.SaveChanges();

            Console.WriteLine($"Estudiante con ID {id} eliminado correctamente.");
        }
        else
        {
            Console.WriteLine($"No se encontró un estudiante con ID {id} para eliminar.");
        }
    }

    public IEnumerable<Estudiante> ObtenerTodosConDocumento()
    {
        return _context.Estudiantes
            .Include(e => e.Documento)
            .ToList();
    }

}