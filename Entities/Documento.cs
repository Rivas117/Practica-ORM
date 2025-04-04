/// <summary>
/// Documento tiene una relacion con estudiante
/// </summary>

namespace Practica_ORM.Entities
{
    public class Documento
    {
        public int Id { get; set; }
        public string NumeroDocumento { get; set; }

        public int EstudianteId { get; set; } //Llave foranea del estudiante
        public Estudiante Estudiante { get; set; }// 1:1
    }
}
