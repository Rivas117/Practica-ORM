using Microsoft.EntityFrameworkCore;
using Practica_ORM.Context;
using Practica_ORM.Entities;

namespace Practica_ORM.Services;

public class ClaseService
{
    private readonly EscuelaDbContext _context;

    public ClaseService(EscuelaDbContext context)
    {
        _context = context;
    }

    public void MostrarClasesConSusAlumnos()
    {
        /// Consulta de relación N:M → Clases con estudiantes inscritos
        var clases = _context.Clases
            .Include(c => c.EstudianteClases)         /// Cargamos la tabla puente
            .ThenInclude(ec => ec.Estudiante)         /// Desde la tabla puente, cargamos cada estudiante
            .ToList();

        foreach (var clase in clases)
        {
            Console.WriteLine("Clase:" + clase.NombreClase);
            Console.WriteLine("Estudiantes:");

            foreach (var ec in clase.EstudianteClases)
            {
                Console.WriteLine("   - " + ec.Estudiante.Nombre);

            }
            Console.WriteLine("----------------------------");
        }
    }
}