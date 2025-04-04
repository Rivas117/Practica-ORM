using Microsoft.EntityFrameworkCore;
using Practica_ORM.Context;
using Practica_ORM.Entities;

namespace Practica_ORM.Services;

public class ProfesorService
{
    private readonly EscuelaDbContext _context;

    public ProfesorService(EscuelaDbContext context)
    {
        _context = context;
    }

    public void MostrarProfesorConSusClases()
    {
        /// Aqui hacemos la relacion1;m de los profesores
        /// la primera parte es igual a la primera de 1;1 pero ya 
        var profesores = _context.Profesores
        .Include(p => p.Clases)
        .ToList();

        foreach (var profesor in profesores)
        {
            Console.WriteLine("Nombre del profesor: " + profesor.NombreProfesor);
            Console.WriteLine("Clases:");

            /// aqui hacemos lo de 1:m para relacionar prosefor dando muchas clases
            foreach (var clase in profesor.Clases)
            {
                Console.WriteLine("   - " + clase.NombreClase);
            }

            Console.WriteLine("----------------------------");
        }
    }
}