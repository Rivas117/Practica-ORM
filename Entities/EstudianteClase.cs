/// <summary>
/// Aqui se hace la relacion de m:m de clase y estudiante
/// </summary>

namespace Practica_ORM.Entities{
    
    public class EstudianteClase
{
    public int EstudianteId { get; set; } //Llave foranea
    public required Estudiante Estudiante { get; set; }

    public int ClaseId { get; set; } //Llave foranea
    public required Clase Clase { get; set; }
}

    }