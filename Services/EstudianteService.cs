using Microsoft.EntityFrameworkCore;
using Practica_ORM.Context;
using Practica_ORM.Entities;

namespace Practica_ORM.Services;

public class EstudianteService
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
}