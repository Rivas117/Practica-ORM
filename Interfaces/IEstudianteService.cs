using Practica_ORM.Entities;

namespace Practica_ORM.interfaces;

/// <summary>
/// Interfaz para definir los métodos del servicio de estudiante.
/// Permite abstraer el comportamiento y facilita el testeo e inyección.
/// </summary>

public interface IEstudianteService
{

    /// <summary>
    /// Muestra todos los estudiantes con sus documentos relacionados (1:1).
    /// </summary>
    void MostrarEstudiantesConDocumento();

    /// <summary>
    /// Muestra los estudiantes mayores de 20 años.
    /// </summary>
    void MostrarEstudiantesMayoresDe20();

    void AgregarEstudiante(Estudiante estudiante);

    Estudiante? ObtenerEstudiantePorId(int id);

    void ActualizarEstudiante(Estudiante estudiante);

    void EliminarEstudiante(int id);

    IEnumerable<Estudiante> ObtenerTodosConDocumento();
}