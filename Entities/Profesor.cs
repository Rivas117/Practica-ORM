/// <summary>
/// El profesor tiene una relacion de 1:M con Clase
/// </summary>

namespace Practica_ORM.Entities{

    public class Profesor
{
    public int id { get; set; }
    public required string NombreProfesor { get; set; }

    public required List<Clase> Clases { get; set; }
}
}