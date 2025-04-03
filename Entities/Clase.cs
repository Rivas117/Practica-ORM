/// <summary>
/// Clase tiene una relacion de 1:m con el profe
/// y una relacion de m:m con estudiantes que culmina en EstudianteClases
/// </summary>

namespace Practica_ORM.Entities {
    
    public class Clase
{
    public int Id { get; set; }
    public string NombreClase { get; set; }

    public int ProfesorId { get; set; }// foranea de profe
    public Profesor Profesor { get; set; }// 1:m

    public List<EstudianteClase> EstudianteClases { get; set; } 
}
    }