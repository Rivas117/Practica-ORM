using System.Text.Json.Serialization; // Esto es necesario para usar [JsonIgnore]
using Practica_ORM.Entities;

namespace Practica_ORM.Entities
{
    /// <summary>
    /// Documento tiene una relación con estudiante
    /// </summary>
    public class Documento
    {
        public int Id { get; set; }

        public string NumeroDocumento { get; set; }

        public int EstudianteId { get; set; } // Llave foránea del estudiante

        [JsonIgnore] // 👈 Esta línea evita el ciclo infinito al serializar JSON
        public Estudiante Estudiante { get; set; } // Relación 1:1 con Estudiante
    }
}